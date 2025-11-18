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
using ProjectBeheerBL.Domein;

namespace ProjectBeheerWPF_UI.GebruikerUI
{
    /// <summary>
    /// Interaction logic for OverzichtEigenProjecten.xaml
    /// </summary>
    public partial class OverzichtEigenProjecten : Window
    {
        private List<Project> projecten = new(); 
        public OverzichtEigenProjecten()
        {
            InitializeComponent();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Project project = ProjectOverzichtDatagrid.SelectedItem as Project;
            if (project != null) 
            { 
                //navigeer naar DetailsProject
                var detailsProject = new DetailsProject(project);
                detailsProject.Show();
            }
        }

        private void Bewerk_Click(object sender, RoutedEventArgs e)
        {
            Project project = ProjectOverzichtDatagrid.SelectedItem as Project;
            if (project != null)
            {
                //navigeer naar BewerkProject
                var bewerkProject = new BewerkProject(project);
                bewerkProject.Show();
            }
        }

        private void Exporteer_Click(object sender, RoutedEventArgs e)
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
