using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Interfaces.Repo;
using ProjectBeheerDL_Memory;
using ProjectBeheerDL_SQL;

namespace ProjectBeheerUtils
{
    public class ProjectSQLFactory
    {
        public IGebruikerRepository GeefGebruikerRepo()
        {
            return new GebruikerRepository();
        }

        public IProjectRepository GeefProjectRepo()
        {
            return new ProjectRepository();
        }
    }
}
