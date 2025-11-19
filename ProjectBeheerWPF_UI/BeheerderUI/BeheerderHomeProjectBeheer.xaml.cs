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
using ProjectBeheerBL.Domein;
using ProjectBeheerUtils;
using ProjectBeheerWPF_UI.GebruikerUI;

namespace ProjectBeheerWPF_UI.BeheerderUI
{
    /// <summary>
    /// Interaction logic for BeheerderHomeProjectBeheer.xaml
    /// </summary>
    public partial class BeheerderHomeProjectBeheer : Window
    {

        private ExportManager exportManager;
        private GebruikersManager gebruikersManager;
        private ProjectManager projectManager;
        private BeheerMemoryFactory beheerMemoryFactory = new();
        private Gebruiker ingelogdeGebruiker;

        public BeheerderHomeProjectBeheer(ExportManager exportManager, GebruikersManager gebruikersManager, 
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory, Gebruiker ingelogdeGebruiker)
        {
            InitializeComponent();
            this.exportManager = exportManager;
            this.gebruikersManager = gebruikersManager;
            this.projectManager = projectManager;
            this.beheerMemoryFactory = beheerMemoryFactory;
        }

        private void MaakNieuwProject_CLick(object sender, RoutedEventArgs e)
        {
            NieuwProject nieuwProjectWindow = new(exportManager, gebruikersManager, projectManager, beheerMemoryFactory);
            nieuwProjectWindow.ShowDialog();
        }

        private void BekijkJouwProjecten_Click(object sender, RoutedEventArgs e)
        {
            OverzichtEigenProjecten overzichtEigenProjectenWindow = new(exportManager, gebruikersManager, projectManager, beheerMemoryFactory);
            overzichtEigenProjectenWindow.ShowDialog();
        }

        private void BekijkAlleProjecten_Click(object sender, RoutedEventArgs e)
        {
            OverzichtAlleProjecten overzichtAlleProjectenWindow = new(exportManager, gebruikersManager, projectManager, beheerMemoryFactory);
            overzichtAlleProjectenWindow.ShowDialog();
        }

        private void BekijkAlleGebruikers_Click(object sender, RoutedEventArgs e)
        {
            OverzichtAlleGebruikers overzichtAlleGebruikersWindow = new(exportManager, gebruikersManager, projectManager, beheerMemoryFactory);
            overzichtAlleGebruikersWindow.ShowDialog();
        }
    }
}
