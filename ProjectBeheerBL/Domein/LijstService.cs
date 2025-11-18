using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class LijstService
    {
        public List<string> Faciliteiten;
        public List<string> Woonvormen;

        public LijstService()
        {
            Faciliteiten = new List<string>();
            Woonvormen = new List<string>();
        }
    }
}
