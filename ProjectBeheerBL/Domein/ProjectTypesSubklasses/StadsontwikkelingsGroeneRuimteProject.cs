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
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, StadsOntwikkeling stadsOntwikkeling, GroeneRuimte groeneRuimte) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            GroeneRuimte = groeneRuimte;
        }

        public StadsontwikkelingsGroeneRuimteProject(int? id, string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, Gebruiker projectEigenaar, StadsOntwikkeling stadsOntwikkeling, GroeneRuimte groeneRuimte) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            GroeneRuimte = groeneRuimte;
        }

        private StadsOntwikkeling stadsOntwikkeling;
        public StadsOntwikkeling StadsOntwikkeling {
            get { return stadsOntwikkeling; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteProject moet stadsOntwikkeling hebben.");
                stadsOntwikkeling = value;
            }
        }
        private GroeneRuimte groeneRuimte;
        public GroeneRuimte GroeneRuimte {
            get { return groeneRuimte; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteProject moet groeneRuimte hebben.");
                groeneRuimte = value;
            }
        }
    }
}
