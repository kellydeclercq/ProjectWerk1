using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;

namespace ProjectBeheerDL.Memory
{
    public class PartnerRepositoryMemory
    {
        private Dictionary<int, Partner> partners = new();
        private int partnerId = 1;

        public PartnerRepositoryMemory()
        {
            this.partners = partners.Add(partnerId, new Partner()); partnerId++;
        }
    }
}
