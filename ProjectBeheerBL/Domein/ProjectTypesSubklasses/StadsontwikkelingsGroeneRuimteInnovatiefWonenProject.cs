using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.projectType;
using ProjectBeheerBL.typeSoorten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein.ProjectTypesSubklasses
{
    public class StadsontwikkelingsGroeneRuimteInnovatiefWonenProject : Project, IStadsontwikkeling, IGroeneRuimte, IInnovatiefWonen
    {

        public StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten,List<Partner> partners, Gebruiker projectEigenaar, Adres adres, StadsOntwikkeling stadsOntwikkeling, 
            GroeneRuimte groeneRuimte, InnovatiefWonen innovatiefWonen) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar, adres)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            GroeneRuimte = groeneRuimte;
            InnovatiefWonen = innovatiefWonen;
        }

        public StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(int id, string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, Gebruiker projectEigenaar, Adres adres, StadsOntwikkeling stadsOntwikkeling, 
            GroeneRuimte groeneRuimte, InnovatiefWonen innovatiefWonen) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar, adres)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            GroeneRuimte = groeneRuimte;
            InnovatiefWonen = innovatiefWonen;
        }

        private StadsOntwikkeling _stadsOntwikkeling;
        public StadsOntwikkeling StadsOntwikkeling {
            get { return _stadsOntwikkeling; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteInnovatiefWonenProject: stadsOntwikkeling mag niet null zijn.");
                _stadsOntwikkeling = value;
            }
        }
        private GroeneRuimte _groeneRuimte;
        public GroeneRuimte GroeneRuimte {
            get { return _groeneRuimte; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteInnovatiefWonenProject: groeneRuimte mag niet null zijn.");
                _groeneRuimte = value;
            }
        }
        private InnovatiefWonen _innovatiefWonen;
        public InnovatiefWonen InnovatiefWonen {
            get { return _innovatiefWonen; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteInnovatiefWonenProject: innovatiefWonen mag niet null zijn.");
                _innovatiefWonen = value;
            }
        }
    }
}
