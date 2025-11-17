using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerDL;

namespace ProjectBeheerUtils.Database
{
    public class ProjectRepositoryFactory
    {
        public static IProjectRepository GetProjectRepository(string repoType)
        {
            switch (repoType)
            {
                case "memory": return new ProjectRepository();
                default: return null;
            }
        }
    }
}
