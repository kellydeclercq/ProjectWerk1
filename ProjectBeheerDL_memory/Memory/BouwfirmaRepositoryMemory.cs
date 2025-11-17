using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;

namespace ProjectBeheerDL.Memory
{
    public class BouwfirmaRepositoryMemory
    {
        private Dictionary<int, Bouwfirma> bouwfirmas = new();
        private int bouwfirmaId = 1;

        public BouwfirmaRepositoryMemory()
        {
            this.bouwfirmas = bouwfirmas.Add(bouwfirmaId, new Bouwfirma()); bouwfirmaId++;
        }
    }
}
