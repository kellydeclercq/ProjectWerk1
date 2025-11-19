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
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerUtils;

namespace ProjectBeheerWPF_UI
{
    /// <summary>
    /// Interaction logic for NieuwProject.xaml
    /// </summary>
    public partial class NieuwProject : Window
    {
        private GebruikersManager _gebruikersManager;
        private ExportManager exportManager;
        private GebruikersManager gebruikersManager;
        private ProjectManager projectManager;
        private BeheerMemoryFactory beheerMemoryFactory;
        private FileDialog fileDialog;
        private List<string> projectStatussen = Enum.GetNames(typeof(ProjectStatus)).ToList();
        private List<string> vergunningsStatussen = Enum.GetNames(typeof(VergunningsStatus)).ToList();
        private List<string> toegankelijkheden = Enum.GetNames(typeof(Toegankelijkheid)).ToList();

        public List<Partner> partners;
        public List<string> bijlages;

        public NieuwProject(ExportManager exportManager, GebruikersManager gebruikersManager, 
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory)
        {
            InitializeComponent();
            this.exportManager = exportManager;
            this.gebruikersManager = gebruikersManager;
            this.projectManager = projectManager;
            this.beheerMemoryFactory = beheerMemoryFactory;

            StatusComboBox.ItemsSource = projectStatussen;
            VergunningsStatusComboBox.ItemsSource = vergunningsStatussen;
            ToegankelijkheidComboBox.ItemsSource = toegankelijkheden;
            
            
        }

        //hier staan algemene methodes voor nieuwProject window

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GaVerderButtonTab_Click(object sender, RoutedEventArgs e)
        {
            //per tab wordt hier individueel naar verwezen
            if (NieuwProjectTabs.SelectedIndex > NieuwProjectTabs.Items.Count - 1)
            {
                NieuwProjectTabs.SelectedIndex += 1;
            }
        }

        //hieronder alles ivm tab1: algemene info

        private void VoegDocumentenToeButton_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Selecteer een bestand";
            dialog.Filter = "Alle bestanden (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                string bestandsPad = dialog.FileName;
                MessageBox.Show("Geselecteerd bestand: " + bestandsPad);
            }
        }

        private void VoegFotosToeButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Selecteer een bestand";
            dialog.Filter = "Afbeeldingen (*.png; *.jpg)| *.png; *.jpg";

            if (dialog.ShowDialog() == true)
            {
                string bestandsPad = dialog.FileName;
                MessageBox.Show("Geselecteerde afbeelding: " + bestandsPad);
            }
        }

        private void VoegPartnerToeButton_Click(object sender, RoutedEventArgs e)
        {
            VoegPartnerToeAanLijst();
            
            NaamPartnerTextBox.Clear();
            EmailPartnerTextBox.Clear();
            TelefoonPartnerTextBox.Clear();
            PartnerRolTextBox.Clear();
        }

        private void VoegPartnerToeAanLijst()
        {
            Partner partner = new(NaamPartnerTextBox.Text, EmailPartnerTextBox.Text,
                TelefoonPartnerTextBox.Text, PartnerRolTextBox.Text);
            partners.Add(partner);
            PartnersListBox.Items.Add(partner.Naam);
        }

        private void GaVerderButtonTab1_Click(object sender, RoutedEventArgs e)
        {
            GaVerderButtonTab_Click(sender, e);
        }


        //hieronder alles ivm tab2: Stadsontwikkeling

        private void GaVerderButtonTab2_Click(object sender, RoutedEventArgs e)
        {
            GaVerderButtonTab_Click(sender, e);
        }

        //hieronder alles ivm tab3: Groene Ruimte

        private void BiodiversiteitSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int waardeBioDiv = (int)BiodiversiteitSlider.Value;
        }

        private void GaVerderButtonTab3_Click(object sender, RoutedEventArgs e)
        {
            GaVerderButtonTab_Click(sender, e);
        }

        private void BezoekersBeoordelingSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int waardeBezoekersOordeel = (int)BezoekersBeoordelingSlider.Value;
        }
        //hieronder alles ivm tab4: Innovatief Wonen

        private void ArchitecturaleInnoScoreSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int waardeArchInno = (int)ArchitecturaleInnoScoreSlider.Value;
        }

        //laatste tab heeft geen verder, maar een bevestigen knop die het project gaat proberen aan te maken

        private void MaakProjectAanButton_Click(object sender, RoutedEventArgs e)
        {
            

            //alle inputvariabelen algemene info

            string titel = TitelInputTextBox.Text;
            DateTime startDatum = StartDatumCalendarButton.SelectedDate.Value;
            ProjectStatus projectStatus = (ProjectStatus)StatusComboBox.SelectedItem;
            bool IsStadsontwikkeling = StadsontwikkelingCheckBox.IsChecked == true; //door dit zo te schrijven cancel je de optie null-waarde voor de bool
            bool IsGroeneRuimte = GroeneRuimteCheckBox.IsChecked == true;
            bool IsInnovatiefWonen = InnovatiefWonenCheckBox.IsChecked == true;
            int postcode = int.Parse(PostcodeTextBox.Text);
            Adres adres = new(StraatTextBox.Text, HuisnummerTextBox.Text, postcode, GemeenteTextBox.Text);
            string beschrijving = BeschrijvingTextBox.Text;
            foreach (string bijlage in BijlagesListBox.Items) bijlages.Add(bijlage);
            VoegPartnerToeAanLijst();   //voeg partner uit de inputvelden ook toe aan de lijst van partners en geef die lijst hier dan mee

            //variabelen stadsontwikkeling
            
            string bouwfirma = BouwfirmaTextBox.Text;
            string bouwfirmaGegevens = GegevensBouwfirmaTextBox.Text;
            VergunningsStatus vergunningsStatus = (VergunningsStatus)VergunningsStatusComboBox.SelectedItem;
            bool IsArchitecturaleWaarde;
            if (ArchitecturaleWaardeJaRadioButton.IsChecked == true) IsArchitecturaleWaarde = true;
            else if (ArchitecturaleWaardeNeeRadioButton.IsChecked == true) IsArchitecturaleWaarde = false;
            Toegankelijkheid toegankelijkheid = (Toegankelijkheid)ToegankelijkheidComboBox.SelectedItem;
            bool IsToeristischeBezienswaardigheid;
            if (ToeristischeBezienswaardigheidJaRadioButton.IsChecked == true) IsToeristischeBezienswaardigheid = true;
            else if (ToeristischeBezienswaardigheidNeeRadioButton.IsChecked == true) IsToeristischeBezienswaardigheid = false;
            bool IsUitlegbord = UitlegbordCheckBox.IsChecked == true;
            bool IsInfowandeling = UitlegbordCheckBox.IsChecked == true;

            //variabelen groene ruimte

            double oppervlaktInVierkanteMeter = double.Parse(OppervlakteTextBox.Text);
            int bioDiversiteitScore = (int)BiodiversiteitSlider.Value;
            int aantalWandelpaden = int.Parse(AantalWandelpadenTextBox.Text); 
            List<string> faciliteiten = new List<string>();
            bool IsSpeeltuin = SpeeltuinFaciliteitCheckBox.IsChecked == true;
            if (IsSpeeltuin) faciliteiten.Add("Speeltuin");
            bool IsPicknickZone = PicknickZoneFaciliteitCheckBox.IsChecked == true;
            if (IsPicknickZone) faciliteiten.Add("Picknickzone");
            bool IsInfoborden = InfobordenFaciliteitCheckBox.IsChecked == true;
            if (IsInfoborden) faciliteiten.Add("Infoborden");
            bool IsAndereFaciliteit = AndereFaciliteitCheckBox.IsChecked == true;
            if (IsAndereFaciliteit) faciliteiten.Add(AndereFaciliteitTextBox.Text);
            bool opgenomenInWandelroute;
            if(ToeristischeWandelroutesJaRadioButton.IsChecked == true) opgenomenInWandelroute = true;
            else if(ToeristischeWandelroutesNeeRadioButton.IsChecked == true) opgenomenInWandelroute = false;
            int bezoekersScore = (int)BezoekersBeoordelingSlider.Value;

                //alle invoer + logica projectTypes adhv checkboxen types



                BepaalTypeProjectEnMaakAan(IsStadsontwikkeling, IsGroeneRuimte, IsInnovatiefWonen);
        }

        private void BepaalTypeProjectEnMaakAan(bool IsStadsontwikkeling, bool IsGroeneRuimte, bool IsInnovatiefWonen)
        {
            switch((IsStadsontwikkeling, IsGroeneRuimte, IsInnovatiefWonen))
            {
                case (true, false, false):
                    projectManager.MaakStadsontwikkelingsProjectAan();
                    break;
                
                case (false, true, false):
                    projectManager.MaakGroeneruimteProjectAan();
                    break;

                case (false, false, true):
                    projectManager.MaakInnovatiefWonenProjectAan();
                    break;

                case(true, true, false):
                    projectManager.MaakStadsOntwikkelingGroeneRuimteProjectAan();
                    break;

                case(true, false, true):
                    projectManager.MaakStadsOntwikkelingInnovatiefWonenProjectAan();
                    break;

                case(false, true, true):
                    projectManager.MaakGroeneRuimteInnovatiefWonenProjectAan();
                    break;

                case (true, true, true):
                    projectManager.MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject();
                    break;

            }
        }
    }
}
