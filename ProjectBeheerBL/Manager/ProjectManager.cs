using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Interfaces.Repo;

namespace ProjectBeheerBL.Beheerder
{
    public class ProjectManager
    {
       private IProjectRepository _repo;
       public LijstService LijstService;

        public ProjectManager(IProjectRepository repo)
        {
            _repo = repo;
        }

        public void MaakGroeneruimteProjectAan()
        {
            _repo.MaakGroeneruimteProjectAan();
        }

        public void MaakStadsontwikkelingsProjectAan()
        {

            _repo.MaakStadsontwikkelingsProjectAan();
        }

        public void MaakInnovatiefWonenProjectAan()
        {
            _repo.MaakInnovatiefWonenProjectAan();
        }

        public void MaakStadsOntwikkelingInnovatiefWonenProjectAan()
        {
            _repo.MaakStadsOntwikkelingInnovatiefWonenProjectAan();
        }

        public void MaakStadsOntwikkelingGroeneRuimteProjectAan()
        {
            _repo.MaakStadsOntwikkelingGroeneRuimteProjectAan();
        }

        public void MaakGroeneRuimteInnovatiefWonenProjectAan()
        {
            _repo.MaakGroeneRuimteInnovatiefWonenProjectAan();
        }

        public void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject()
        {
            _repo.MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject();
        }


    }
}
