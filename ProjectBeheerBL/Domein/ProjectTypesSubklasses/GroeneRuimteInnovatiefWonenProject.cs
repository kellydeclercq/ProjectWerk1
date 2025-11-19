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
    public class GroeneRuimteInnovatiefWonenProject : Project, IGroeneRuimte, IInnovatiefWonen
    {


        public GroeneRuimteInnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
           string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, GroeneRuimte groeneRuimte, 
           InnovatiefWonen innovatiefWonen )
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar)
        {
            GroeneRuimte = groeneRuimte;
            InnovatiefWonen = innovatiefWonen;
        }


        public GroeneRuimteInnovatiefWonenProject(int? id, string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, Gebruiker projectEigenaar, GroeneRuimte groeneRuimte, InnovatiefWonen innovatiefWonen)
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar)
        {
            GroeneRuimte = groeneRuimte;
            InnovatiefWonen = innovatiefWonen;
        }

        private GroeneRuimte groeneRuimte;
        public GroeneRuimte GroeneRuimte {
            get { return groeneRuimte; }
            set {
                if (value == null) throw new ProjectException("GroeneRuimteInnovatiefWonenProject moet groeneRuimte hebben.");
                groeneRuimte = value;
            }
     }

        private InnovatiefWonen innovatiefWonen;

        public InnovatiefWonen InnovatiefWonen {
            get { return innovatiefWonen; }
            set {
                if (value == null) throw new ProjectException("GroeneRuimteInnovatiefWonenProject moet innovatiefWonen hebben.");
                innovatiefWonen = value;
            }
        }
    }
}
