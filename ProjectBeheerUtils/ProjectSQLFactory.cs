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
    public static class ProjectSQLFactory
    {
        public static IGebruikerRepository GeefGebruikerRepo(string connectionString)
        {
            return new GebruikerRepository(connectionString);
        }

        public static IProjectRepository GeefProjectRepo(string connectionString)
        {
            return null;
            //return new ProjectRepository(connectionString);
        }
    }
}
