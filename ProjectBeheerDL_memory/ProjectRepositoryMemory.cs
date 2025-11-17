using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerDL_Memory
{
    public class ProjectRepositoryMemory
    {
        private Dictionary<int, Project> projecten = new();
        private List<byte[]> fotos = new List<byte[]>();
        private List<byte[]> documenten = new List<byte[]>();

        private int projectId = 1;

        public ProjectRepositoryMemory()
        {
            fotos = new List<byte[]>();
            documenten = new List<byte[]>();

            projecten.Add(projectId, new GroeneRuimteProject(1, "Kloosterhof", "beschrijving1", new DateTime(2025,12,18), 
                new ProjectStatus(), fotos[0], documenten[0]); projectId++;
            projecten.Add(projectId, new InnovatiefWonenProject(1, "Kloosterhof", "beschrijving1", new DateTime(2025, 12, 18), 
                new ProjectStatus(), fotos[0], documenten[0]); projectId++;
            projecten.Add(projectId, new StadsOntwikkelingsProject(1, "Kloosterhof", "beschrijving", new DateTime(2025, 12, 18), 
                new ProjectStatus(), fotos[0], documenten[0]); projectId++;
        
                }
    }
}
