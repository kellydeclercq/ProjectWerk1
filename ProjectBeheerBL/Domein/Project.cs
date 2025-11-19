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
        const int MinLengteTitel = 5;
        const int MaxLengteTitel = 50;

        const int MinAantalTekens = 4;
        const int MaxAantalTekens = 10000;

        const int MinWijkLengte = 2;

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

        private int? _id;
        public int? Id
        {
            get => _id;
            set
            {
                if (value < 1) throw new ProjectException("Id moet minstens 1 zijn");
                _id = value;
            }
        }

        private string _projectTitel;
        public string ProjectTitel
        {
            get { return _projectTitel; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Titel mag niet leeg zijn");
                var trimmed = value.Trim();
                if (trimmed.Length < MinLengteTitel || trimmed.Length > MaxLengteTitel) throw new ProjectException($"Titel moet meer karakters hebben dan {MinLengteTitel} en max {MaxLengteTitel}.");
                _projectTitel = trimmed;
            }
        }
        private string _beschrijving;
        public string Beschrijving
        {
            get { return _beschrijving; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Beschrijving mag niet leeg zijn");
                var trimmed = value.Trim();
                if (trimmed.Length < MinAantalTekens || trimmed.Length > MaxAantalTekens) throw new ProjectException($"Beschrijving moet meer karakters hebben dan {MinAantalTekens} en max {MaxAantalTekens}.");
                _beschrijving = trimmed;
            }
        }
        public DateTime? StartDatum { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

        private string _wijk;


        public string Wijk
        {
            get { return _wijk; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Wijk mag niet leeg zijn");
                var trimmed = value.Trim();
                if (trimmed.Length < MinWijkLengte) throw new ProjectException($"Wijk moet meer dan {MinWijkLengte} karakters zijn");
                _wijk = trimmed;
            }
        }

        private List<byte[]> _fotos;

        public List<byte[]> Fotos
        {
            get => _fotos;

            set
            {
                if (_fotos == null) throw new ProjectException("Fotos mag niet NULL zijn.");
                _fotos = value;
            }
        }

        private List<byte[]> _documenten;
        public List<byte[]> Documenten
        {
            get => _documenten;
            set
            {
                if (_documenten == null) throw new ProjectException("Documenten mag niet NULL zijn.");
                _documenten = value;
            }
        }

        private List<Partner> _partners;
        public List<Partner> Partners
        {
            get => _partners;
            set
            {
                if (_partners == null) throw new ProjectException("Partners mag niet NULL zijn.");
                _partners = value;
            }
        }

        private Gebruiker _projectEigenaar;
        public Gebruiker ProjectEigenaar
        {
            get => _projectEigenaar;
            set
            {
                if (_projectEigenaar == null) throw new ProjectException("ProjectEigenaar mag niet NULL zijn.");
                _projectEigenaar = value;
            }
        }
        public override string? ToString()
        {
            throw new NotImplementedException();
            /*return $"{ProjectTitel}, {Type}, {GebruikersRol}";*/
        }
    }
}
