using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;

namespace ProjectBeheerDL_Memory
{
    public class ProjectRepositoryMemory
    {
        private Dictionary<int, Project> projecten = new();
        private int projectId = 1;

        public ProjectRepositoryMemory()
        {
            projecten.Add(projectId, new Project(); projectId++;

        }
    }
}
