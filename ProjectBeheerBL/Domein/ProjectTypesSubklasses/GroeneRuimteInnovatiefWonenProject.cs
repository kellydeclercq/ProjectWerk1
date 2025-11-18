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
           string wijk, List<byte[]>? fotos, List<byte[]>? documenten, GroeneRuimte groeneRuimte, InnovatiefWonen innovatiefWonen )
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners)
        {
            GroeneRuimte = groeneRuimte;
            InnovatiefWonen = innovatiefWonen;
        }



        public GroeneRuimteInnovatiefWonenProject(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, GroeneRuimte groeneRuimte, InnovatiefWonen innovatiefWonen)
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners)
        {
            GroeneRuimte = groeneRuimte;
            InnovatiefWonen = innovatiefWonen;
        }

        public GroeneRuimte GroeneRuimte { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public InnovatiefWonen InnovatiefWonen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
