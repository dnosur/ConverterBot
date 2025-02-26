using ConverterBot.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using ConverterBot.Models;
using System.Runtime.Remoting.Messaging;
using ConverterBot.Utilities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConverterBot.Bot
{
    internal static class Events
    {
        public static async Task ChangeCurrencyFromId(
            Models.User user,
            SymbolsController symbols,
            UsersCurrenciesController usersCurrencies,
            ITelegramBotClient _bot,
            Message message,
            string newSymbol
        )
        {
            if (!symbols.Contains(newSymbol))
            {
                await _bot.SendTextMessageAsync(
                    chatId: message.Chat.Id, 
                    $"Не вдалося знайти валюту {newSymbol}!"
                    );
                return;
            }

            Symbols symbol = symbols.GetByTitle(newSymbol);
            await usersCurrencies.UpdateSymbolFrom(user.id, symbol.id);

            UsersCurrencies currencies = usersCurrencies.GetBy(user.id);

            await _bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                $"Конвертацію змінено на валюту {symbol.title}!\n{symbol.description}\n\n" +
                $"Ваші поточні параметри:\n\n" +
                $"З: {currencies.symbol_from.title}\n{currencies.symbol_from.description}\n\n" +
                $"На: {currencies.symbol_to.title}\n{currencies.symbol_to.description}"
                );
        }

        public static async Task ChangeCurrencyToId(
            Models.User user,
            SymbolsController symbols,
            UsersCurrenciesController usersCurrencies,
            ITelegramBotClient _bot,
            Message message,
            string newSymbol
        )
        {
            if (!symbols.Contains(newSymbol))
            {
                await _bot.SendTextMessageAsync(
                    chatId: message.Chat.Id, 
                    $"Не вдалося знайти валюту {newSymbol}!"
                    );
                return;
            }

            Symbols symbol = symbols.GetByTitle(newSymbol);
            await usersCurrencies.UpdateSymbolTo(user.id, symbol.id);

            UsersCurrencies currencies = usersCurrencies.GetBy(user.id);

            await _bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                $"Конвертацію змінено на валюту {symbol.title}!\n{symbol.description}\n\n" +
                $"Ваші поточні параметри:\n\n" +
                $"З: {currencies.symbol_from.title}\n{currencies.symbol_from.description}\n\n" +
                $"На: {currencies.symbol_to.title}\n{currencies.symbol_to.description}"
                );
        }
    }
}
