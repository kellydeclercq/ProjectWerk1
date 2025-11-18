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

        public StadsontwikkelinProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, StadsOntwikkeling stadsOntwikkeling) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
            StadsOntwikkeling = stadsOntwikkeling;
        }

        public StadsontwikkelinProject(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, StadsOntwikkeling stadsOntwikkeling) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
            StadsOntwikkeling = stadsOntwikkeling;
        }

        public StadsOntwikkeling StadsOntwikkeling { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
