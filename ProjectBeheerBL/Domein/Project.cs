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
            List<byte[]> documenten, List<Partner> partners, Gebruiker projectEigenaar)
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
            ProjectEigenaar = projectEigenaar;
        }

        public Project(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk, List<byte[]> fotos,
            List<byte[]> documenten, List<Partner> partners, Gebruiker projectEigenaar)
        {
            ProjectTitel = projectTitel;
            Beschrijving = beschrijving;
            StartDatum = startDatum;
            ProjectStatus = projectStatus;
            Wijk = wijk;
            Fotos = fotos;
            Documenten = documenten;
            Partners = partners;
            ProjectEigenaar = projectEigenaar;
        }

        public int? Id { get; set; }

        private string _projectTitel;
        public string ProjectTitel {
            get { return _projectTitel; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Titel mag niet leeg zijn");
                var trimmed = value.Trim();
                if (trimmed.Length < 5 || trimmed.Length > 50) throw new ProjectException("Titel moet meer karakters hebben dan 5 en max 50.");
                _projectTitel = trimmed;
            }
        }
        private string _beschrijving;
        public string Beschrijving {
            get { return _beschrijving; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Beschrijving mag niet leeg zijn");
                var trimmed = value.Trim();
                if (trimmed.Length < 4 || trimmed.Length > 10000) throw new ProjectException("Beschrijving moet meer karakters hebben dan 4 en max 10000.");
                _beschrijving = trimmed;
            }
        }
        public DateTime? StartDatum { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

        private string _wijk;
        public string Wijk {
            get { return _wijk; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Wijk mag niet leeg zijn");
                var trimmed = value.Trim();
                if (trimmed.Length < 2) throw new ProjectException("Wijk moet meer dan 2 karakters zijn");
                _wijk = trimmed;
            }
        }
        public List<byte[]> Fotos { get; set; }
        public List<byte[]> Documenten { get; set; }
        public List<Partner> Partners { get; set; }

        public Gebruiker ProjectEigenaar { get; set; }

        public override string? ToString()
        {
            throw new NotImplementedException();
            /*return $"{ProjectTitel}, {Type}, {GebruikersRol}";*/
        }
    }
}
