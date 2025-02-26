using ConverterBot.Controllers;
using ConverterBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ConverterBot.Db;
using System.Numerics;
using System.IO;
using Newtonsoft.Json;

using ConverterBot.Bot;

namespace ConverterBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bot.Bot bot = new Bot.Bot();
        }
    }
}
