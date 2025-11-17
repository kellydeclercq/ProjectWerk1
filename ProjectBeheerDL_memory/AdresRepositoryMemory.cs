using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;

namespace ProjectBeheerDL_Memory
{
    public class AdresRepositoryMemory
    {
        private Dictionary<int, Adres> adressen = new();
        private int adresId = 1;

        public AdresRepositoryMemory()
        {
            adressen.Add(adresId, new Adres("Rijksweg", "127", 9000, "Gent")); adresId++;
            adressen.Add(adresId, new Adres("Floraliënlaan", "88", 9000, "Gent")); adresId++;
            adressen.Add(adresId, new Adres("Kraanlei", "267B", 9000, "Gent")); adresId++;
            adressen.Add(adresId, new Adres("R4", "/", 9000, "Gent")); adresId++;
            adressen.Add(adresId, new Adres("Kastanjestraat", "67", 9000, "Gent")); adresId++;
        }
    }
}
