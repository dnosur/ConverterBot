using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using System.Threading;
using ConverterBot.Controllers;
using ConverterBot.Models;
using ConverterBot.Utilities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static ConverterBot.Bot.EventsController;

namespace ConverterBot.Bot
{
    internal class Bot
    {
        private ITelegramBotClient _bot;
        private ReceiverOptions _receiverOptions;

        private readonly string _token;

        private readonly UserController Users;
        private readonly SymbolsController symbols;
        private readonly UsersCurrenciesController UsersCurrencies;

        private readonly EventsController events;

        private readonly string ApiKey;

        private async Task StartBot()
        {
            _bot = new TelegramBotClient(_token);
            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery
                },
                ThrowPendingUpdates = true
            };

            var cts = new CancellationTokenSource();
            _bot.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);

            await _bot.GetMeAsync().ContinueWith((Task<Telegram.Bot.Types.User> task) =>
            {
                if (!task.IsCompleted)
                {
                    Console.WriteLine(task.Exception.ToString());
                    return;
                }

                Telegram.Bot.Types.User me = task.Result;
                Console.WriteLine($"{me.FirstName} started!");
            });

            await Task.Delay(-1);
        }

        private async Task UpdateHandler(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        await HandleMessage(update.Message);
                        break;
                    case UpdateType.CallbackQuery:
                        await HandleCallbackQuery(update.CallbackQuery);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            string ErrorMessage;
            switch (error)
            {
                case ApiRequestException apiRequestException:
                    ErrorMessage = $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}";
                    break;
                default:
                    ErrorMessage = error.ToString();
                    break;
            }

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandleMessage(Message message)
        {
            Telegram.Bot.Types.User user = message.From;
            Chat chat = message.Chat;

            Console.WriteLine($"{user.Id} {user.FirstName} {user.LastName}: {message.Text}");

            if (!Users.IsContains(user.Id.ToString()))
            {
                string telegram_id = user.Id.ToString();
                await Users.Add(new ConverterBot.Models.User()
                {
                    telegram_id = telegram_id
                });

                await UsersCurrencies.Add(Users[telegram_id].id);
                await _bot.SendTextMessageAsync(
                    chat.Id,
                    $"Привіт, {user.FirstName}!" +
                    $"\nЗавдяки мені ти можеш провести конвертацію валют! " +
                    $"Зараз у тебе встановлена конвертація з USD у EUR. " +
                    $"Просто, введи цікаву тобі суму, а я проведу для тебе конвертацію!",
                    replyMarkup: Keyboards.GetChangeSymbolKeyboard()
                );
                return;
            }

            EventsType eventType = events.GetEventType(message.Text);
            UsersCurrencies currency = UsersCurrencies.GetBy(Users[user.Id.ToString()].id);

            //Convert
            if (eventType == EventsType.Convert)
            {
                message.Text = message.Text.Replace(",", ".");

                ConvertedCurrency converted = Request<ConvertedCurrency>
                    .Get($"https://api.currencybeacon.com/v1/convert?from={currency.symbol_from.title}&to={currency.symbol_to.title}&amount={message.Text}&api_key={ApiKey}");
                await _bot.SendTextMessageAsync(
                    chat.Id,
                    $"{message.Text} {currency.symbol_from.title} = {converted.value} {currency.symbol_to.title}",
                    replyToMessageId: message.MessageId,
                    replyMarkup: Keyboards.GetChangeSymbolKeyboard()
                );
                return;
            }

            await _bot.SendTextMessageAsync(
                chat.Id,
                "Нічого не розумію... Можеш повторити?\n" +
                $"Введи мені цифру в {currency.symbol_from.title}, а я переконвертую її для тебе у {currency.symbol_to.title}",
                replyToMessageId: message.MessageId,
                replyMarkup: Keyboards.GetChangeSymbolKeyboard()
                );

            return;
        }

        private async Task HandleCallbackQuery(CallbackQuery callbackQuery)
        {
            if (callbackQuery.Data.StartsWith("change:"))
            {
                BotEventDelegate _event;
                if ((callbackQuery.Data.Split(':')[1] == "to"))
                {
                    _event = Events.ChangeCurrencyToId;
                }
                else
                {
                    _event = Events.ChangeCurrencyFromId;
                }

                await ChangeSymbol(
                    UsersCurrencies.GetBy(Users[callbackQuery.Message.Chat.Id.ToString()].id),
                    callbackQuery.Message.Chat.Id,
                    _event
                );
                return;
            }

            await _bot.DeleteMessageAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId);
            if (callbackQuery.Data.StartsWith("symbol:"))
            {
                string new_symbol = callbackQuery.Data.Split(':')[1];
                events.Execute(Users[callbackQuery.From.Id.ToString()], symbols, UsersCurrencies, _bot, callbackQuery.Message, new_symbol);
                return;
            }

            int page = Convert.ToInt32(callbackQuery.Data.Split(':')[1]);
            await SendPaginatedKeyboard(UsersCurrencies.GetBy(Users[callbackQuery.Message.Chat.Id.ToString()].id), callbackQuery.Message.Chat.Id, page);
        }

        private async Task SendPaginatedKeyboard(UsersCurrencies currency, long chatId, int page = 0, int itemsPerPage = 10)
        {
            await _bot.SendTextMessageAsync(chatId, $"Ваші поточні параметри:\n\n" +
                    $"З: {currency.symbol_from.title}\n" +
                    currency.symbol_from.description + "\n\n" +
                    $"На: {currency.symbol_to.title}\n" +
                    currency.symbol_to.description + "\n\n" + 
                    "Оберіть валюту:",
                    replyMarkup: Keyboards.GetPaginatedKeyboard(symbols, page, itemsPerPage));
        }

        private async Task ChangeSymbol(UsersCurrencies currency, long chatId, BotEventDelegate _event)
        {
            await SendPaginatedKeyboard(currency, chatId);
            events.AddEvent(chatId.ToString(), _event);
        } 

        public Bot()
        {
            ApiKey = ConfigurationManager.AppSettings["ApiKey"];
            _token = ConfigurationManager.AppSettings["token"];

            Users = new UserController();
            symbols = new SymbolsController();
            UsersCurrencies = new UsersCurrenciesController();

            events = new EventsController();

            StartBot().Wait();
        }
    }
}
