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
    public class StadsontwikkelingsInnovatiefWonenProject : Project, IStadsontwikkeling, IInnovatiefWonen
    {

        public StadsontwikkelingsInnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, StadsOntwikkeling stadsOntwikkeling, InnovatiefWonen innovatiefWonen) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            InnovatiefWonen = innovatiefWonen;
        }

        public StadsontwikkelingsInnovatiefWonenProject(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, StadsOntwikkeling stadsOntwikkeling, InnovatiefWonen innovatiefWonen) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            InnovatiefWonen = innovatiefWonen;
        }

        public StadsOntwikkeling StadsOntwikkeling { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public InnovatiefWonen InnovatiefWonen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
