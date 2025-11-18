using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectBeheerBL.Beheerder;
using ProjectBeheerUtils;

namespace ProjectBeheerWPF_UI
{
    /// <summary>
    /// Interaction logic for HomeProjectBeheer.xaml
    /// </summary>
    public partial class HomeProjectBeheer : Window
    {
        private ExportManager exportManager;
        private GebruikersManager gebruikersManager;
        private ProjectManager projectManager;
        private BeheerMemoryFactory beheerMemoryFactory = new();

        public HomeProjectBeheer(ExportManager exportManager, GebruikersManager gebruikersManager, 
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory)
        {
            InitializeComponent();
            exportManager = exportManager;
        }

        private void MaakNieuwProjectButton_Click(object sender, RoutedEventArgs e)
        {
            NieuwProject nieuwProjectWindow = new(exportManager, gebruikersManager, projectManager, beheerMemoryFactory);
        }
    }
}
