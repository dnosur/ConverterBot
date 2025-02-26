using ConverterBot.Controllers;
using ConverterBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConverterBot.Utilities
{
    internal static class Keyboards
    {
        private static readonly InlineKeyboardMarkup changeSymbolsKeys = new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("Змнітии з", "change:from"),
            InlineKeyboardButton.WithCallbackData("Змінити на", "change:to")
        });

        public static InlineKeyboardMarkup GetChangeSymbolKeyboard()
        {
            return changeSymbolsKeys;
        }

        public static InlineKeyboardMarkup GetPaginatedKeyboard(SymbolsController symbols, int page = 0, int itemsPerPage = 10)
        {
            int totalItems = symbols.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            List<List<InlineKeyboardButton>> rows = new List<List<InlineKeyboardButton>>();

            int start = page * itemsPerPage;
            int end = Math.Min(start + itemsPerPage, totalItems);

            for (int i = start; i < end; i++)
            {
                rows.Add(new List<InlineKeyboardButton> {
                    InlineKeyboardButton.WithCallbackData(symbols.GetByIndex(i).title, $"symbol:{symbols.GetByIndex(i).title}")
                });
            }

            List<InlineKeyboardButton> paginationRow = new List<InlineKeyboardButton>();

            if (page > 0)
            {
                paginationRow.Add(InlineKeyboardButton.WithCallbackData("\U000023EA ", $"pagination:{page - 1}"));
            }

            if (page < totalPages - 1)
            {
                paginationRow.Add(InlineKeyboardButton.WithCallbackData("\U000023E9 ", $"pagination:{page + 1}"));
            }

            rows.Add(paginationRow);

            return new InlineKeyboardMarkup(rows);
        }
    }
}
