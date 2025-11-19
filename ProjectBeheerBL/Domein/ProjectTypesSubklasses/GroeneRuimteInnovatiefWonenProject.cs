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


        public GroeneRuimte GroeneRuimte { get; set; }
        public InnovatiefWonen InnovatiefWonen { get; set; }
    }
}
