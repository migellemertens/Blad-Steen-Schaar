using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSS_v3
{
    public static class Wachtwoorden
    {
        public static string speler;

        public static Dictionary<string, string> geregistreerdeSpelers = new Dictionary<string, string>()
        {
            {"MiMe", "Migelle Mertens" },
            {"PaBr", "Patricia Briers" },
            {"DoPa", "Paul Dox" }
        };
    }
}
