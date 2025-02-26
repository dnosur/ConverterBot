using ConverterBot.Db;
using ConverterBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ConverterBot.Utilities;

namespace ConverterBot.Controllers
{
    internal class UserController
    {
        List<User> Users;

        private int GetIndexBy(string telegram_id)
        {
            return Users.FindIndex(item => item.telegram_id == telegram_id);
        }

        public UserController()
        {
            using (ConverterBot.Db.Db db = new ConverterBot.Db.Db())
            {
                Users = db.Users.ToList();
            }
        }

        public bool IsContains(ref User user)
        {
            return Users.IndexOf(user) != -1;
        }

        public bool IsContains(string telegram_id)
        {
            return Users.FindIndex(user => user.telegram_id == telegram_id) != -1;
        }

        public async Task Add(User user)
        {
            if (IsContains(ref user))
            {
                return;
            }

            using (var db = new ConverterBot.Db.Db())
            {
                await db.Users.AddAsync(user);
                Console.WriteLine($"User {user.telegram_id} add successful!");
                Users.Add(user);

                await ConverterBot.Utilities.Utilities.SaveDb(db);
            }
        }

        public User this[string telegram_id]
        {
            get
            {
                if (!IsContains(telegram_id))
                {
                    throw new ArgumentOutOfRangeException("Невірний telegram_id!");
                }
                return Users.Find(user => user.telegram_id == telegram_id);
            }
            set
            {
                int index = GetIndexBy(telegram_id);
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException("Невірний telegram_id!");
                }
                Users[index] = value;
            }
        }
    }
}
