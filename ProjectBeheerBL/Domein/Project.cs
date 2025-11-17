using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.Domein
{
    public class Project
    {
        public int? Id { get; set; }
        public string ProjectTitel { get; set; }
        public string Beschrijving { get; set; }
        public DateTime StartDatum { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        List<byte[]> Fotos { get; set; }
        List<byte[]> Documenten { get; set; }
    }
}
