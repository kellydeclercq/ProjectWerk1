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
    public class StadsontwikkelingsInnovatiefWonenProject : Project, IStadsontwikkeling, IInnovatiefWonen
    {

        public StadsontwikkelingsInnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, StadsOntwikkeling stadsOntwikkeling, InnovatiefWonen innovatiefWonen) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            InnovatiefWonen = innovatiefWonen;
        }

        public StadsontwikkelingsInnovatiefWonenProject(int? id, string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, Gebruiker projectEigenaar, StadsOntwikkeling stadsOntwikkeling, InnovatiefWonen innovatiefWonen) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar)
        {
            StadsOntwikkeling = stadsOntwikkeling;
            InnovatiefWonen = innovatiefWonen;
        }


        private StadsOntwikkeling _stadsOntwikkeling;
        public StadsOntwikkeling StadsOntwikkeling {
            get { return _stadsOntwikkeling; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsInnovatiefWonenProject moet stadsOntwikkeling hebben.");
                _stadsOntwikkeling = value;
            }
        }

        private InnovatiefWonen _innovatiefWonen;
        public InnovatiefWonen InnovatiefWonen {
            get { return _innovatiefWonen; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingsInnovatiefWonenProject moet innovatiefWonen hebben.");
                _innovatiefWonen = value;
            }
        }
        
    }
}
