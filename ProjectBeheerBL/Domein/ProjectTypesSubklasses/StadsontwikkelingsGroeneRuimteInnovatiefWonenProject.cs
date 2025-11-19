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
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten,List<Partner> partners, Gebruiker projectEigenaar, StadsOntwikkeling stadsOntwikkeling, 
            GroeneRuimte groeneRuimte, InnovatiefWonen innovatiefWonen) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            GroeneRuimte = groeneRuimte;
            InnovatiefWonen = innovatiefWonen;
        }

        public StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(int? id, string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, Gebruiker projectEigenaar, StadsOntwikkeling stadsOntwikkeling, 
            GroeneRuimte groeneRuimte, InnovatiefWonen innovatiefWonen) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            GroeneRuimte = groeneRuimte;
            InnovatiefWonen = innovatiefWonen;
        }

        private StadsOntwikkeling stadsOntwikkeling;
        public StadsOntwikkeling StadsOntwikkeling {
            get { return stadsOntwikkeling; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteInnovatiefWonenProject moet stadsOntwikkeling hebben.");
                stadsOntwikkeling = value;
            }
        }
        private GroeneRuimte groeneRuimte;
        public GroeneRuimte GroeneRuimte {
            get { return groeneRuimte; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteInnovatiefWonenProject moet groeneRuimte hebben.");
                groeneRuimte = value;
            }
        }
        private InnovatiefWonen innovatiefWonen;
        public InnovatiefWonen InnovatiefWonen {
            get { return innovatiefWonen; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsGroeneRuimteInnovatiefWonenProject moet innovatiefWonen hebben.");
                innovatiefWonen = value;
            }
        }
    }
}
