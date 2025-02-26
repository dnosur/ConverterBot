using ConverterBot.Controllers;
using ConverterBot.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConverterBot.Bot
{
    internal class EventsController
    {
        public delegate Task BotEventDelegate(
            Models.User user, 
            SymbolsController symbols,
            UsersCurrenciesController usersCurrencies,
            ITelegramBotClient _bot,
            Message message,
            string newSymbol
            );
        private readonly Dictionary<string, BotEventDelegate> events;

        public enum EventsType
        {
            Undefined = -1,
            Convert = 0,
            ChangeFrom = 1,
            ChangeTo = 2
        };

        public EventsController()
        {
            events = new Dictionary<string, BotEventDelegate>();
        }

        public void AddEvent(string telegram_id, BotEventDelegate botEvent)
        {
            events[telegram_id] = botEvent;
        }

        public void Execute(
            Models.User user, 
            SymbolsController symbols, 
            UsersCurrenciesController usersCurrencies,
            ITelegramBotClient _bot,
            Message message,
            string newSymbol
            )
        {
            events[user.telegram_id](user, symbols, usersCurrencies, _bot, message, newSymbol);
            events.Remove(user.telegram_id);
        }

        public EventsType GetEventType(string message)
        {
            if (message.ToLower() == "change from")
            {
                return EventsType.ChangeFrom;
            }

            if (message.ToLower() == "change to")
            {
                return EventsType.ChangeTo;
            }

            double num = 0;
            message = message.Replace(@".", ",");
            if (double.TryParse(message, out num))
            {
                return EventsType.Convert;
            }

            return EventsType.Undefined;
        }

        public bool Contains(string telegram_id)
        {
            foreach (string key in events.Keys)
            {
                if (key == telegram_id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
