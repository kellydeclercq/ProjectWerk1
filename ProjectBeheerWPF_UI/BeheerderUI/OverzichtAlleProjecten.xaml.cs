using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using ProjectBeheerBL.Enumeraties;
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
        private Gebruiker ingelogdeGebruiker;

        private List<Project> projecten = new();
        private List<Project> Gefilterdeprojecten = new();

        private OpenFolderDialog folderDialog = new OpenFolderDialog();
        private string initFolderExport;


        public OverzichtAlleProjecten(ExportManager exportManager, GebruikersManager gebruikersManager,
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory, Gebruiker ingelogdeGebruiker)
        {
            InitializeComponent();

            projecten = projectManager.GeefAlleProjecten();
            ProjectOverzichtDatagrid.ItemsSource = projecten;
            initFolderExport = "@C:\\Downloads";

            this.exportManager = exportManager;
            this.gebruikersManager = gebruikersManager;
            this.projectManager = projectManager;
            this.beheerMemoryFactory = beheerMemoryFactory;
            this.ingelogdeGebruiker = ingelogdeGebruiker;

            Status.ItemsSource = Enum.GetNames(typeof(ProjectStatus));
        }

        //KNOPPEN ONDERAAN

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
                    ProjectFiche fiche = new ProjectFiche(project);
                    fiche.Show();

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

        private void ExporteerAlsCsv_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om te kunnen exporteren.",
                    "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<Project> geselecteerdeProjecten;
            //ipv List<Project> projecten = ProjectOverzichtDatagrid.SelectedItems as List<Project>;
            //want selectedItems is een Ilist en geen List => the Ilist is een collectie van objecten die niet weten welk type ze zijn
            //"as List<Project> zal dus altijd mislukken en null zijn
            //Cast<Project>().ToList(); itereert door elk item  in selectedItems en cast het naar Project, ToList() maakt hier dan een lijst van
            //alternatief: ProjectOverzichtDatagrid.SelectedItems.OfType<Project>().ToList(); als we willen dat de projecten die niet voldoen gewoon geskipt worden
            try
            {
                geselecteerdeProjecten = ProjectOverzichtDatagrid.SelectedItems
                    .Cast<Project>()
                    .ToList();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Er ging iets mis tijdens het verzamelen van de geselecteerde projecten.",
                    "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                //return beter bij innesting van de code
            }

            if (geselecteerdeProjecten == null || geselecteerdeProjecten.Count == 0)
            {
                MessageBox.Show("Er konden geen geldige projecten worden geselecteerd.",
                    "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // FolderDialog openen
            bool? result = folderDialog.ShowDialog();

            // result == true → gebruiker klikte op OK
            // FolderName bevat het pad
            if (result == true && !string.IsNullOrWhiteSpace(folderDialog.FolderName))
            {
                exportManager.ExporteerProjectenNaarCsv(
                    geselecteerdeProjecten,
                    folderDialog.FolderName
                );
            }
        }

        private void ExporteerAlsPdf_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om te kunnen exporteren.",
                    "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<Project> projecten;

            try
            {
                projecten = ProjectOverzichtDatagrid.SelectedItems
                    .Cast<Project>()
                    .ToList();
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("InvalidCast in ExporteerAlsCsv_Click");
                MessageBox.Show("De geselecteerde items zijn geen projecten.",
                    "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Controleer of er effectief Project-objecten zijn
            if (projecten == null || projecten.Count == 0)
            {
                MessageBox.Show("Er konden geen geldige projecten worden geselecteerd.",
                    "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Folderdialog openen
            bool? result = folderDialog.ShowDialog();

            if (result == true && !string.IsNullOrWhiteSpace(folderDialog.FolderName))
            {
                //TODO export als pdf
                // sla de exportfile hier op
                // exportManager.ExportNaarPdf(projecten, folderDialog.FolderName);
            }
        }

        //FILTERS

        #region FILTER apart
        //private void FilterCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (FilterCombobox.SelectedItem is ComboBoxItem item && item.Tag is not null)
        //    {
        //        string tag = item.Tag.ToString();

        //        bool isDatum = tag == "Datum";

        //        // Toon/verberg het datumpaneel
        //        DatumsFilterPanel.Visibility = isDatum ? Visibility.Visible : Visibility.Collapsed;

        //        // Pas filter alleen toe wanneer het geen datumselectie is
        //        if (!isDatum)
        //            FilterToepassen(tag);
        //    }
        //}
        #endregion

       

        #region vorige versie FILTER apart
        private void FilterToepassen(string tag)
        {
           
            //{
            //    //case moet tag naam zijn
            //    switch (tag)
            //    {
            //        case "Titel":
            //            if (!string.IsNullOrWhiteSpace(TitelTextBox.Text))
            //            {
            //                Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpTitel(TitelTextBox.Text);
            //            }
            //            else
            //            {
            //                Gefilterdeprojecten = projecten; //als de textbox leeg is toon altijd alle projecten
            //            }
            //            break;
            //        case "Type":
            //            if (!string.IsNullOrWhiteSpace(TypeTextBox.Text))
            //            {
            //                //Als de checkbox aangevinkt is → true
            //                //Als de checkbox niet aangevinkt is → false
            //                //Als de checkbox indeterminate(null) → false

            //                List<bool> checkedboxes = new List<bool>()
            //                {
            //                    //Deze notatie kan ook: IsStad.IsChecked.GetValueOrDefault(false);
            //                    IsStad.IsChecked ?? false,
            //                    IsGroeneRuimte.IsChecked ?? false,
            //                    IsInnovatief.IsChecked ?? false
            //                };

            //                Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpType(checkedboxes);
            //                //lijst:  1: IsStad 2: IsGroen 3: IsInnovatief
            //            }
            //            else
            //            {
            //                Gefilterdeprojecten = projecten;
            //            }
            //            break;
            //        case "Wijk":
            //            if (!string.IsNullOrWhiteSpace(WijkTextBox.Text))
            //            {

            //                Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpWijk(WijkTextBox.Text);
            //            }
            //            else
            //            {
            //                Gefilterdeprojecten = projecten;
            //            }
            //            break;
            //        case "Status":
            //            if (!string.IsNullOrWhiteSpace(StatusTextBox.Text))
            //            {
            //                Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpStatus(StatusTextBox.Text);
            //            }
            //            else
            //            {
            //                Gefilterdeprojecten = projecten;
            //            }
            //            break;
            //        case "Partners":
            //            if (!string.IsNullOrWhiteSpace(PartnersTextBox.Text))
            //            {
            //                Gefilterdeprojecten = projectManager.GeefProjectenGefilterdOpPartners(PartnersTextBox.Text);
            //            }
            //            else
            //            {
            //                Gefilterdeprojecten = projecten;
            //            }
            //            break;
            //        case "None":
            //        default:
            //            Gefilterdeprojecten = projecten;
            //            break;
            //    }

            //    ProjectOverzichtDatagrid.ItemsSource = Gefilterdeprojecten;
       

        }

        #endregion

        #region FILTER APART
        //private void Datepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // Controleer of beide datums geselecteerd zijn
        //    if (!StartDatePicker.SelectedDate.HasValue || !EndDatePicker.SelectedDate.HasValue)
        //        return;

        //    DateTime start = StartDatePicker.SelectedDate.Value;
        //    DateTime end = EndDatePicker.SelectedDate.Value;

        //    // Zorg dat start <= end
        //    if (start > end)
        //    {
        //        MessageBox.Show("De startdatum mag niet na de einddatum liggen.",
        //            "Ongeldige datum", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    
        //    //// Filter projecten waarvan de StartDatum tussen start en end ligt (inclusief grenzen)
        //    //Gefilterdeprojecten = projecten
        //    //    .Where(p => p.StartDatum >= start && p.StartDatum <= end)
        //    //    .ToList();

        //    //// Update de DataGrid
        //    //ProjectOverzichtDatagrid.ItemsSource = Gefilterdeprojecten;
        //   
        //}
        #endregion
       
        
        private void SorteerProjecten(string tag)
        {
            //als er een Gefilterdeprojecten bestaat => gesorteerdeprojecten krijgt die lijst binnen
            //als gefilterdeprojecten null is => gesorteerdeprojecten krijgt projecten binnen
            List<Project> gesorteerdeProjecten = Gefilterdeprojecten ?? projecten;
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


        private void Button_clickFilter(object sender, RoutedEventArgs e)
        {
            string projectnaam = Projectnaam.Text;
            string wijk = Wijk.Text;
            ProjectStatus status = Enum.Parse<ProjectStatus>(Status.Text);
            string eigenaar = Eigenaar.Text;
            List<bool> typeChecks = new List<bool>
            {
                stadsontwikkeling.IsChecked ?? false,
                groeneRuimte.IsChecked ?? false,
                innovatiefWonen.IsChecked ?? false
            };

            DateTime start = StartDatePicker.SelectedDate.Value;
            DateTime eind = EndDatePicker.SelectedDate.Value;

            if (start > eind && start != default && eind != default)
            {
                MessageBox.Show("De startdatum mag niet na de einddatum liggen.",
                    "Ongeldige datum", MessageBoxButton.OK, MessageBoxImage.Warning);              
            }

            //TODO methoden aanroepen die filters toepast
            ProjectOverzichtDatagrid.ItemsSource = projectManager.GeefProjectenGefilterd(projectnaam, wijk, status, eigenaar, typeChecks, start, eind);
        }
    }
}

