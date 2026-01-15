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
        private readonly string _connectionString;

        public ProjectSQLFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IGebruikerRepository GeefGebruikerRepo()
        {
            return new GebruikerRepository(_connectionString);
        }

        public IProjectRepository GeefProjectRepo()
        {
            return new ProjectRepository(_connectionString);
        }
    }

}
