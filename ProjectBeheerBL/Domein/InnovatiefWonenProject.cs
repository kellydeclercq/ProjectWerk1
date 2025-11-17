using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class InnovatiefWonenProject
    {
        public int AantalWooneenheden { get; set; }
        public bool RondleidingMogelijk { get; set; }
        public int InnovatieScore { get; set; } //TODO gaan we dit in BL afdwingen of lijst van opties hebben hier?
    }
}
