using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterBot.Utilities
{
    internal static class Utilities
    {
        public static async Task SaveDb(ConverterBot.Db.Db db)
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save error: {ex.Message}");
            }
        }
    }
}
