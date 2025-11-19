using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Interfaces.Repo;
using ProjectBeheerDL_Memory;

namespace ProjectBeheerUtils
{
    public class BeheerMemoryFactory
    {
        public IGebruikerRepository GeefGebruikerRepo()
        {
            return new GebruikerRepositoryMemory();
        }

        public IProjectRepository GeefProjectRepo()
        {
            return new ProjectRepositoryMemory(GeefGebruikerRepo());
        }
    }
}
