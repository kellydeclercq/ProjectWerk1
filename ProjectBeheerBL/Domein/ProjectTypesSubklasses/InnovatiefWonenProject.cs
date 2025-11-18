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
        public InnovatiefWonenProject(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, string wijk, List<byte[]> fotos, List<byte[]> documenten) : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
        }

        public InnovatiefWonen InnovatiefWonen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
