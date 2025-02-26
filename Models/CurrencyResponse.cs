using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterBot.Models
{
    internal class CurrencyResponse
    {
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, double> Rates { get; set; }
        public bool Success { get; set; }
        public int Timestamp { get; set; }
    }
}
