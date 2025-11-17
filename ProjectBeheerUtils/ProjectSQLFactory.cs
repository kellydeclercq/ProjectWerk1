using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Interfaces;
using ProjectBeheerDL_Memory;
using ProjectBeheerDL_SQL;

namespace ProjectBeheerUtils
{
    public class ProjectSQLFactory
    {
        public IGebruikerRepositoryMemory GeefGebruikerRepo()
        {
            return new GebruikerRepository();
        }

        public IProjectRepositoryMemory GeefProjectRepo()
        {
            return new ProjectRepository();
        }
    }
}
