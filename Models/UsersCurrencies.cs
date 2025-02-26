
using System.ComponentModel.DataAnnotations.Schema;

namespace ConverterBot.Models
{
    internal class UsersCurrencies
    {
        [Column("id")]
        public int id { get; set; }

        [Column("user_id")]
        public int user_id { get; set; }

        [Column("symbol_from_id")]
        public int symbol_from_id { get; set; }

        [Column("symbol_to_id")]
        public int symbol_to_id { get; set; }

        public User user { get; set; }
        public Symbols symbol_from { get; set; }
        public Symbols symbol_to { get; set; }
    }
}