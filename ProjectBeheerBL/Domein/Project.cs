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
        public String Wijk { get; set; }
        List<byte[]> Fotos { get; set; }
        List<byte[]> Documenten { get; set; }

        public Project(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, string wijk, List<byte[]> fotos, List<byte[]> documenten)
        {
            Id = id;
            ProjectTitel = projectTitel;
            Beschrijving = beschrijving;
            StartDatum = startDatum;
            ProjectStatus = projectStatus;
            Wijk = wijk;
            Fotos = fotos;
            Documenten = documenten;
        }
    }
}
