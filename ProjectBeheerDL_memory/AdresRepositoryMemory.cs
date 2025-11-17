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
            adressen = adressen.Add(adresId, new Adres()); adresId++;
        }
    }
}
