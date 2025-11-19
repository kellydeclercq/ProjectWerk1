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

namespace ProjectBeheerWPF_UI.BeheerderUI
{
    /// <summary>
    /// Interaction logic for OverzichtAlleProjecten.xaml
    /// </summary>
    public partial class OverzichtAlleProjecten : Window
    {
        private ExportManager exportManager;
        private GebruikersManager gebruikersManager;
        private ProjectManager projectManager;
        private BeheerMemoryFactory beheerMemoryFactory;

        public OverzichtAlleProjecten()
        {
            InitializeComponent();
        }

        public OverzichtAlleProjecten(ExportManager exportManager, GebruikersManager gebruikersManager, ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory)
        {
            this.exportManager = exportManager;
            this.gebruikersManager = gebruikersManager;
            this.projectManager = projectManager;
            this.beheerMemoryFactory = beheerMemoryFactory;
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om details te kunnen bekijken.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (ProjectOverzichtDatagrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Je kan geen meerdere projecten selecteren om details te bekijken", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Project project = ProjectOverzichtDatagrid.SelectedItem as Project;
                if (project != null)
                {
                    //navigeer naar DetailsProject
                    var detailsProject = new DetailsProject(project);
                    detailsProject.Show();
                }
            }
        }

        private void Bewerk_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om te kunnen bewerken.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (ProjectOverzichtDatagrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Je kan geen meerdere projecten selecteren om te bewerken.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Project project = ProjectOverzichtDatagrid.SelectedItem as Project;
                if (project != null)
                {
                    //navigeer naar BewerkProject
                    var bewerkProject = new BewerkProject(project);
                    bewerkProject.Show();
                }
            }
        }

        private void Exporteer_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om te kunnen exporteren.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Project project = ProjectOverzichtDatagrid.SelectedItem as Project;
                if (project != null)
                {
                    //navigeer naar DetailsProject
                    var exporteerWindow = new ExportWindow(project);
                    exporteerWindow.Show();
                }
            }
        }
    }
}
