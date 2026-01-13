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
        private string connectionstring = @"Data Source=MANALOLAPTOP\\SQLEXPRESS;Initial Catalog=ProjectBeheerStadGent;Integrated Security=True;Trust Server Certificate=True";

        public IGebruikerRepository GeefGebruikerRepo()
        {
            return new GebruikerRepository(connectionstring);
        }

        public IProjectRepository GeefProjectRepo()
        {
            return new ProjectRepositoryMemory(GeefGebruikerRepo());
        }
    }
}
