using ProjectBeheerBL.Beheerder;
using ProjectBeheerBL.Domein;
using ProjectBeheerUtils;

namespace ProjectBeheer.ExportTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new BeheerMemoryFactory();
            var projectRepo = factory.GeefProjectRepo();
            var projectManager = new ProjectManager(projectRepo);

            var exportManager = new ExportManager();

            List<Project> projecten = projectManager.GeefAlleProjecten();

            Console.WriteLine($"Aantal projecten gevonden: {projecten.Count}");

            string pad = @"C:\Hogent\ProjectWerk1\Export\projecten_export.csv";

            exportManager.ExporteerProjectenNaarCsv(projecten, pad);

            Console.WriteLine("Check map voor export");
            Console.WriteLine();
        }
    }
}
