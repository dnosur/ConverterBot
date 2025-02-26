using ConverterBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ConverterBot.Controllers
{
    internal class UsersCurrenciesController : IEnumerable<UsersCurrencies>
    {
        List<UsersCurrencies> usersCurrencies;

        private UsersCurrencies Populate(UsersCurrencies usersCurrency)
        {
            using (ConverterBot.Db.Db db = new ConverterBot.Db.Db())
            {
                List<Symbols> symbols = db.Symbols.ToList();
                usersCurrency.symbol_from = symbols.Find(item => item.id == usersCurrency.symbol_from_id);
                usersCurrency.symbol_to = symbols.Find(item => item.id == usersCurrency.symbol_to_id);

                List<Models.User> users = db.Users.ToList();
                usersCurrency.user = users.Find(item => item.id == usersCurrency.user_id);
            }

            return usersCurrency;
        }

        private void UpdateByUserId(int user_id, UsersCurrencies currency)
        {
            int index = usersCurrencies.FindIndex(item => item.user_id == user_id);
            if (index < 0)
            {
                return;
            }
            usersCurrencies[index] = currency;
        }

        public UsersCurrenciesController()
        {
            using (ConverterBot.Db.Db db = new ConverterBot.Db.Db())
            {
                usersCurrencies = db.UsersCurrencies.ToList();
            }
        }

        //Default from USD to EUR
        public async Task Add(int user_id, int currency_from_id = 66, int currency_to_id = 159)
        {
            using (var db = new ConverterBot.Db.Db())
            {
                UsersCurrencies usersCurrency = new UsersCurrencies()
                {
                    user_id = user_id,
                    symbol_from_id = currency_from_id,
                    symbol_to_id = currency_to_id
                };

                await db.UsersCurrencies.AddAsync(usersCurrency);
                usersCurrencies.Add(usersCurrency);

                await ConverterBot.Utilities.Utilities.SaveDb(db);
            }
        }

        public UsersCurrencies GetBy(int user_id)
        {
            int index = usersCurrencies.FindIndex(item => item.user_id == user_id);
            if (index == -1)
            {
                throw new Exception("Wrong index!");
            }

            return Populate(usersCurrencies[index]);
        }

        public async Task UpdateSymbolFrom(int user_id, int new_symbol_from_id)
        {
            using (var db = new ConverterBot.Db.Db())
            {
                UsersCurrencies currency = await db.UsersCurrencies
                    .FirstOrDefaultAsync(item => item.user_id == user_id);

                if (currency == null)
                {
                    throw new Exception("User currency not found!");
                }

                currency.symbol_from_id = new_symbol_from_id;
                await db.SaveChangesAsync();
                UpdateByUserId(user_id, currency);
            }
        }

        public async Task UpdateSymbolTo(int user_id, int new_symbol_to_id)
        {
            using (var db = new ConverterBot.Db.Db())
            {
                UsersCurrencies currency = await db.UsersCurrencies
                    .FirstOrDefaultAsync(item => item.user_id == user_id);

                if (currency == null)
                {
                    throw new Exception("User currency not found!");
                }

                currency.symbol_to_id = new_symbol_to_id;
                await db.SaveChangesAsync();
                UpdateByUserId(user_id, currency);
            }
        }

        public IEnumerator<UsersCurrencies> GetEnumerator()
        {
            return usersCurrencies.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
