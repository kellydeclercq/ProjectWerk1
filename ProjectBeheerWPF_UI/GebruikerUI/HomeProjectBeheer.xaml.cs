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
using ProjectBeheerWPF_UI.BeheerderUI;

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
            this.exportManager = exportManager;
            this.gebruikersManager = gebruikersManager;
            this.projectManager = projectManager;
            this.beheerMemoryFactory = beheerMemoryFactory;
        }

        private void MaakNieuwProjectButton_Click(object sender, RoutedEventArgs e)
        {
            NieuwProject nieuwProjectWindow = new(exportManager, gebruikersManager, projectManager, beheerMemoryFactory);
            nieuwProjectWindow.ShowDialog();
        }

        private void OverzichtJouwProjectenButton(object sender, RoutedEventArgs e)
        {
            OverzichtAlleProjecten overzichtAlleProjectenWindow = new(exportManager, gebruikersManager, projectManager, beheerMemoryFactory);
            overzichtAlleProjectenWindow.ShowDialog();
        }
    }
}
