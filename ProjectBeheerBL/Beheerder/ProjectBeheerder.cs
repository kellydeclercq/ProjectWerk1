using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Interfaces;

namespace ProjectBeheerBL.Beheerder
{
    public class ProjectBeheerder
    {
       private IProjectRepositoryMemory _repo;

        public ProjectBeheerder(IProjectRepositoryMemory repo)
        {
            _repo = repo;
        }
    }
}
