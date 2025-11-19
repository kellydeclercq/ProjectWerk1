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
using Microsoft.Win32;
using ProjectBeheerBL.Beheerder;
using ProjectBeheerBL.Domein;
using ProjectBeheerUtils;

namespace ProjectBeheerWPF_UI.GebruikerUI
{
    /// <summary>
    /// Interaction logic for OverzichtEigenProjecten.xaml
    /// </summary>
    public partial class OverzichtEigenProjecten : Window
    {
        private List<Project> projecten = new();
        private OpenFolderDialog folderDialog = new OpenFolderDialog();
        private string initFolderExport;
        public OverzichtEigenProjecten(ExportManager exportManager, GebruikersManager gebruikersManager,
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory)
        {
            InitializeComponent();
            initFolderExport = "@C:\\Downloads";
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            if(ProjectOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om details te kunnen bekijken.","Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        //voor het exporteren:
        //open nieuwe window waarin je een folder kan kiezen waar je het bestand (of bestanden) opslaat
        private void ExporteerAlsCsv_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om te kunnen exporteren.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //ipv List<Project> projecten = ProjectOverzichtDatagrid.SelectedItems as List<Project>;
                //want selectedItems is een Ilist en geen List => the Ilist is een collectie van objecten die niet weten welk type ze zijn
                //"as List<Project> zal dus altijd mislukken en null zijn
                //Cast<Project>().ToList(); itereert door elk item  in selectedItems en cast het naar Project, ToList() maakt hier dan een lijst van
                //alternatief: ProjectOverzichtDatagrid.SelectedItems.OfType<Project>().ToList(); als we willen dat de projecten die niet voldoen gewoon geskipt worden
                try
                {
                    List<Project> projecten = ProjectOverzichtDatagrid.SelectedItems.Cast<Project>().ToList();
                }
                catch (InvalidCastException) 
                {
                    Console.WriteLine("Invalidcast in ExporteerAlsCsv_CLick");
                }
                if (projecten != null)
                {
                    //open verkenner window 
                    bool? result = folderDialog.ShowDialog();
                    //wordt venster gesloten en heb ik Folder geselecteerd?
                    if (result == true && !string.IsNullOrWhiteSpace(folderDialog.FolderName))
                    {
                        //sla de exportfile hier op 
                        
                    }


                }
            }
        }

        private void ExporteerAlsPdf_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om te kunnen exporteren.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //ipv List<Project> projecten = ProjectOverzichtDatagrid.SelectedItems as List<Project>;
                //want selectedItems is een Ilist en geen List => the Ilist is een collectie van objecten die niet weten welk type ze zijn
                //"as List<Project> zal dus altijd mislukken en null zijn
                //Cast<Project>().ToList(); itereert door elk item  in selectedItems en cast het naar Project, ToList() maakt hier dan een lijst van
                //alternatief: ProjectOverzichtDatagrid.SelectedItems.OfType<Project>().ToList(); als we willen dat de projecten die niet voldoen gewoon geskipt worden
                try
                {
                    List<Project> projecten = ProjectOverzichtDatagrid.SelectedItems.Cast<Project>().ToList();
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("Invalidcast in ExporteerAlsCsv_CLick");
                }
                if (projecten != null)
                {
                    //open verkenner window 
                    bool? result = folderDialog.ShowDialog();

                    //wordt venster gesloten en heb ik Folder geselecteerd?
                    if (result == true && !string.IsNullOrWhiteSpace(folderDialog.FolderName))
                    {
                        //sla de exportfile hier op 

                    }


                }
            }
    }
}
