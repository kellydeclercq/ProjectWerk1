using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;

namespace ProjectBeheerBL.Interfaces.Repo
{
    public interface IProjectRepository
    {
        void MaakGroeneRuimteInnovatiefWonenProjectAan();
        public void MaakGroeneruimteProjectAan();
        void MaakInnovatiefWonenProjectAan();
        void MaakStadsOntwikkelingGroeneRuimteProjectAan();
        void MaakStadsOntwikkelingInnovatiefWonenProjectAan();
        void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject();
        void MaakStadsontwikkelingsProjectAan();
    }
}
