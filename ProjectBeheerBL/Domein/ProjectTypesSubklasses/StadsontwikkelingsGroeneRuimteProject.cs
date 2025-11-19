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
    public class StadsontwikkelingsGroeneRuimteProject : Project, IStadsontwikkeling, IGroeneRuimte
    {

        public StadsontwikkelingsGroeneRuimteProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, Adres adres, StadsOntwikkeling stadsOntwikkeling, GroeneRuimte groeneRuimte) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar,adres)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            GroeneRuimte = groeneRuimte;
        }

        public StadsontwikkelingsGroeneRuimteProject(int? id, string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, Gebruiker projectEigenaar, Adres adres, StadsOntwikkeling stadsOntwikkeling, GroeneRuimte groeneRuimte) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar, adres)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            GroeneRuimte = groeneRuimte;
        }

        private StadsOntwikkeling _stadsOntwikkeling;
        public StadsOntwikkeling StadsOntwikkeling {
            get { return _stadsOntwikkeling; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteProject moet stadsOntwikkeling hebben.");
                _stadsOntwikkeling = value;
            }
        }
        private GroeneRuimte _groeneRuimte;
        public GroeneRuimte GroeneRuimte {
            get { return _groeneRuimte; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteProject moet groeneRuimte hebben.");
                _groeneRuimte = value;
            }
        }
    }
}
