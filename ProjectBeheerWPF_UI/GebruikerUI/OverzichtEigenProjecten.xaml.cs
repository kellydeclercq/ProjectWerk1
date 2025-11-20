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
        private List<Project> Gefilterdeprojecten = new();
        private OpenFolderDialog folderDialog = new OpenFolderDialog();
        private string initFolderExport;

        private ExportManager exportManager;
        private GebruikersManager gebruikersManager;
        private ProjectManager projectManager;
        private Gebruiker ingelogdeGebruiker;

        public OverzichtEigenProjecten(ExportManager exportManager, GebruikersManager gebruikersManager,
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory, Gebruiker ingelogdeGebruiker)
        {
            InitializeComponent();
            projecten = projectManager.GeefAlleProjecten();
            ProjectOverzichtDatagrid.ItemsSource = projecten;
            initFolderExport = "@C:\\Downloads";

            this.exportManager = exportManager;
            this.gebruikersManager = gebruikersManager;
            this.projectManager = projectManager;
            this.ingelogdeGebruiker = ingelogdeGebruiker;   
        }

        //Gebruiker kiest een optie in de combobox
        private void FilterCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = FilterCombobox.SelectedItem as ComboBoxItem;
            if (selectedItem is not null)
            {
                string tag = selectedItem.Tag.ToString();
                if (tag == "Datum")
                {
                    DatumsFilterPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    //als de optie datum geselecteerd is toon de datumvakjes, anders verberg ze
                    //filter toepassen adhv de tag die wel geselecteerd is
                    DatumsFilterPanel.Visibility = Visibility.Collapsed;
                    FilterToepassen(tag);
                }
            }
            
            
        }

        private void Datepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue) 
            {
                DateTime start = StartDatePicker.SelectedDate.Value;
                DateTime end = EndDatePicker.SelectedDate.Value;

                if (start <= end)
                {
                    //filter de projecten waar de start en einddatum van tussen of op de grenzen liggen
                    Gefilterdeprojecten = projecten
                                                    .Where(p => p.StartDatum >= start && p.StartDatum <= end)
                                                    .ToList();

                    ProjectOverzichtDatagrid.ItemsSource = Gefilterdeprojecten;
                }
            }
        }


        private void FilterToepassen(string tag) 
        {
            //case moet tag naam zijn
            switch (tag)
            {
                case "Titel":
                    if (!string.IsNullOrWhiteSpace(TitelTextBox.Text))
                    {
                        Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpTitel(TitelTextBox.Text);
                    }
                    else
                    {
                        Gefilterdeprojecten = projecten; //als de textbox leeg is toon altijd alle projecten
                    }
                    break;
                case "Type":
                    if (!string.IsNullOrWhiteSpace(TypeTextBox.Text))
                    {
                        Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpType();
                        //TODO: checkboxes; bools doorgeven1: Isgroen 2: Isinnovatief 3: Isstad
                    }
                    else
                    {
                        Gefilterdeprojecten = projecten;
                    }
                    break;
                case "Wijk":
                    if (!string.IsNullOrWhiteSpace(WijkTextBox.Text))
                    {
                        
                        Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpWijk(WijkTextBox.Text);
                    }
                    else
                    {
                        Gefilterdeprojecten = projecten;
                    }
                    break;
                case "Status":
                    if (!string.IsNullOrWhiteSpace(StatusTextBox.Text))
                    {
                        Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpStatus(StatusTextBox.Text);
                    }
                    else
                    {
                        Gefilterdeprojecten = projecten;
                    }
                    break;
                case "Partners":
                    if (!string.IsNullOrWhiteSpace(PartnersTextBox.Text))
                    {
                        Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpPartners(PartnersTextBox.Text);
                    }
                    else
                    {
                        Gefilterdeprojecten = projecten;
                    }
                    break;
                case "None":
                    default: Gefilterdeprojecten = projecten;
                    break;
            }


            //als er gefilterd is kunnen we direct gaan sorteren
            //als er geen sorteeroptie is aangeduid tonen we gewoon de gefilterde objecten
            ComboBoxItem selectedSorteerItem = SortComboBox.SelectedItem as ComboBoxItem;
            string sorteerTag = selectedSorteerItem.Tag.ToString();

            if (!string.IsNullOrWhiteSpace(sorteerTag) && sorteerTag != "None")
            {
                SorteerProjecten(sorteerTag);
            }
            else 
            {
                ProjectOverzichtDatagrid.ItemsSource = Gefilterdeprojecten;
            }
        }

        private void SorteerProjecten(string tag)
        {
            //als er een Gefilterdeprojecten bestaat => gesorteerdeprojecten krijgt die lijst binnen
            //als gefilterdeprojecten null is => gesorteerdeprojecten krijgt projecten binnen
            List <Project> gesorteerdeProjecten = Gefilterdeprojecten ?? projecten;
            //case moet tag naam zijn
            switch (tag)
            {
                case "Titel":
                    gesorteerdeProjecten = gesorteerdeProjecten.OrderBy(p => p.ProjectTitel).ToList();
                    break;
                case "Type":
                    //TODO: sorteren op type project
                    //gesorteerdeProjecten = gesorteerdeProjecten.OrderBy(p => p.).ToList();
                    break;
                case "Wijk":
                    gesorteerdeProjecten = gesorteerdeProjecten.OrderBy(p => p.Wijk).ToList();
                    break;
                case "Status":
                    gesorteerdeProjecten = gesorteerdeProjecten.OrderBy(p => p.ProjectStatus).ToList();
                    break;
                case "Partners":
                    gesorteerdeProjecten = gesorteerdeProjecten.OrderBy(p => p.Partners).ToList();
                    break;
                case "None":
                default:
                    gesorteerdeProjecten = projecten;
                    break;
            }

            ProjectOverzichtDatagrid.ItemsSource = gesorteerdeProjecten;
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
                        //TODO: sla de exportfile hier op 
                        exportManager.ExporteerProjectenNaarCsv(projecten, folderDialog.FolderName);
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
                        //exportManager.ExportNaarPdf(projecten);
                    }


                }
            }
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
                    var projectFicheWindow = new ProjectFiche(project);
                    projectFicheWindow.Show();
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

        
    }
}
