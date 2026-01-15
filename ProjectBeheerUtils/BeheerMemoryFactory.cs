using ProjectBeheerBL.Interfaces.Repo;
using ProjectBeheerDL_Memory;
using ProjectBeheerDL_SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerUtils
{
    public class BeheerMemoryFactory
    {
        private readonly string _connectionString;

        public BeheerMemoryFactory(string connectionString)
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
