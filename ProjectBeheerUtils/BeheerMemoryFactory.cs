using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Interfaces;
using ProjectBeheerDL_Memory;

namespace ProjectBeheerUtils
{
    public class BeheerMemoryFactory
    {
        public IGebruikerRepositoryMemory GeefGebruikerRepo()
        {
            return new GebruikerRepositoryMemory();
        }

        public IProjectRepositoryMemory GeefProjectRepo()
        {
            return new ProjectRepositoryMemory();
        }
    }
}
