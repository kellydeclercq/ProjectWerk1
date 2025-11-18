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
    }
}
