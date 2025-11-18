using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.projectType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein.ProjectTypesSubklasses
{
    public class InnovatiefWonenProject : Project, IInnovatiefWonen
    {

        public InnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, InnovatiefWonen innovatiefWonen) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
            InnovatiefWonen = innovatiefWonen;
        }

        public InnovatiefWonenProject(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, InnovatiefWonen innovatiefWonen) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
            InnovatiefWonen = innovatiefWonen;
        }

        public InnovatiefWonen InnovatiefWonen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
