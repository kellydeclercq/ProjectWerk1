using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.Domein
{
    public abstract class Project
    {
        public Project(int? id, string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk, List<byte[]> fotos,
            List<byte[]> documenten, List<Partner> partners)
        {
            Id = id;
            ProjectTitel = projectTitel;
            Beschrijving = beschrijving;
            StartDatum = startDatum;
            ProjectStatus = projectStatus;
            Wijk = wijk;
            Fotos = fotos;
            Documenten = documenten;
            Partners = partners;
        }

        public Project(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk, List<byte[]> fotos,
            List<byte[]> documenten, List<Partner> partners)
        {
            ProjectTitel = projectTitel;
            Beschrijving = beschrijving;
            StartDatum = startDatum;
            ProjectStatus = projectStatus;
            Wijk = wijk;
            Fotos = fotos;
            Documenten = documenten;
            Partners = partners;
        }

        public int? Id { get; private set; }

        private string _projectTitel;
        public string ProjectTitel { 
            get { return _projectTitel; } 
            set{
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Titel mag niet leeg zijn");
                var trimmed = value.Trim();
                if (trimmed.Length < 5 || trimmed.Length > 50) throw new ProjectException("Titel moet meer karakters hebben dan 5 en max 50.");
            }
        public string Beschrijving { get; set; }
        public DateTime? StartDatum { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public String Wijk { get; set; }
        List<byte[]> Fotos { get; set; }
        List<byte[]> Documenten { get; set; }
        public List<Partner> Partners { get; set; }


    }
}
