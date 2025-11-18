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
    public class GroeneRuimteProject : Project, IGroeneRuimte
    {
        public GroeneRuimteProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk, 
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, GroeneRuimte groeneRuimte) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners)
        {
            GroeneRuimte = groeneRuimte;
        }

        public GroeneRuimteProject(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, GroeneRuimte groeneRuimte) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners)
        {
            GroeneRuimte = groeneRuimte;
        }

        public GroeneRuimte GroeneRuimte { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
