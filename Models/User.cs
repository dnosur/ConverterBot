using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterBot.Models
{
    internal class User
    {
        public int id { get; set; }
        public string telegram_id { get; set; }

        public ICollection<UsersCurrencies> UsersCurrencies { get; set; }
    }
}
