using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;

namespace ProjectBeheerDL_Memory
{
    public class BouwfirmaRepositoryMemory
    {
        private Dictionary<int, BouwFirma> bouwfirmas = new();
        private int bouwfirmaId = 1;

        public BouwfirmaRepositoryMemory()
        {
            bouwfirmas = bouwfirmas.Add(bouwfirmaId, new BouwFirma()); bouwfirmaId++;
        }
    }
}
