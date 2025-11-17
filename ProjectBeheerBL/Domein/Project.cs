using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class Project
    {
        public int? Id { get; set; }
        public string ProjectTitel { get; set; }
        public string Beschrijving { get; set; }
        public DateTime MyProperty { get; set; }
    }
}
