using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.projectType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein.ProjectTypesSubklasses
{
    public class StadsontwikkelinProject : Project, IStadsontwikkeling
    {
        public StadsontwikkelinProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk, List<byte[]>? fotos, List<byte[]>? documenten) : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
        }

        public StadsontwikkelinProject(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, string wijk, List<byte[]> fotos, List<byte[]> documenten) : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
        }

        public StadsOntwikkeling StadsOntwikkeling { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
