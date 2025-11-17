using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Interfaces;

namespace ProjectBeheerBL.Beheerder
{
    public class ProjectManager
    {
       private IProjectRepositoryMemory _repo;

        public ProjectManager(IProjectRepositoryMemory repo)
        {
            _repo = repo;
        }
    }
}
