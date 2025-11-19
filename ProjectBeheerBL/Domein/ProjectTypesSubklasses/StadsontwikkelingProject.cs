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
    public class StadsontwikkelingProject : Project, IStadsontwikkeling
    {

        public StadsontwikkelingProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, Adres adres StadsOntwikkeling stadsOntwikkeling) 
            : base(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar, adres)
        {
            StadsOntwikkeling = stadsOntwikkeling;
        }

        public StadsontwikkelingProject(int? id, string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, 
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, Gebruiker projectEigenaar, Adres adres, StadsOntwikkeling stadsOntwikkeling) 
            : base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar, adres)
        {
            StadsOntwikkeling = stadsOntwikkeling;
        }

        private StadsOntwikkeling _stadsOntwikkeling;
        public StadsOntwikkeling StadsOntwikkeling {
            get { return _stadsOntwikkeling; }
            set {
                if (value == null) throw new ProjectException("StadsontwikkelingProject moet stadsOntwikkeling hebben.");
                _stadsOntwikkeling = value;
            }
        }

    }
}
