using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;

namespace ProjectBeheerDL_Memory
{
    public class PartnerRepositoryMemory
    {
        private Dictionary<int, Partner> partners = new();
        private int partnerId = 1;

        public PartnerRepositoryMemory()
        {
            partners.Add(partnerId, new Partner()); partnerId++;
        }
    }
}
