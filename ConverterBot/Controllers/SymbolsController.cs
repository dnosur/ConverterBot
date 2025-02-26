using ConverterBot.Db;
using ConverterBot.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterBot.Controllers
{
    internal class SymbolsController : IEnumerable<Symbols>
    {
        List<Symbols> symbols;

        public SymbolsController()
        {
            using (Db.Db db = new Db.Db())
            {
                symbols = db.Symbols.ToList();
            }
        }

        public Symbols GetById(int id)
        {
            int index = symbols.FindIndex(item => item.id == id);
            if (index < 0)
            {
                throw new Exception("Index error!");
            }
            return symbols[index];
        }

        public Symbols GetByTitle(string title)
        {
            int index = symbols.FindIndex(item => item.title == title);
            if (index < 0)
            {
                throw new Exception("Index error!");
            }
            return symbols[index];
        }

        public bool Contains(string title)
        {
            return symbols.FindIndex(item => item.title == title) != -1;
        }

        public Symbols GetByIndex(int index)
        {
            if (index >= symbols.Count)
            {
                throw new Exception("Index exception");
            }
            return symbols[index];
        }

        public IEnumerator<Symbols> GetEnumerator()
        {
            return symbols.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
