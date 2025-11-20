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
using System.IO;
using Path = System.IO.Path;
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
        private ExportManager exportManager;
        private GebruikersManager gebruikersManager;
        private ProjectManager projectManager;
        private BeheerMemoryFactory beheerMemoryFactory;
        private Gebruiker ingelogdeGebruiker;
        private FileDialog fileDialog;
        private List<string> projectStatussen = Enum.GetNames(typeof(ProjectStatus)).ToList();
        private List<string> vergunningsStatussen = Enum.GetNames(typeof(VergunningsStatus)).ToList();
        private List<string> toegankelijkheden = Enum.GetNames(typeof(Toegankelijkheid)).ToList();

        private List<Partner> partners = new();
        private List<string> bijlages = new();
        private List<byte[]> documenten = new();
        private List<byte[]> fotos = new();
        private List<BouwFirma> bouwFirmas = new();
        private List<string> geselecteerdeWoonvormen = new();

        //alle inputvariabelen algemene info

        private string titel;
        private DateTime startDatum;
        private ProjectStatus projectStatus;
        private bool IsStadsontwikkeling;
        private bool IsGroeneRuimte;
        private bool IsInnovatiefWonen;
        private int postcode = 0;
        private Adres adres;
        private string wijk;
        private string beschrijving;

        //variabelen stadsontwikkeling

        private string bouwfirma;
        private string emailBouwfirma;
        private string telefoonBouwfirma;
        private string? websiteBouwfirma;
        private VergunningsStatus vergunningsStatus;
        private bool IsArchitecturaleWaarde;
        private Toegankelijkheid toegankelijkheid;
        private bool IsToeristischeBezienswaardigheid;
        private bool IsUitlegbordOfWandeling;

        //variabelen groene ruimte

        private double oppervlaktInVierkanteMeter;
        private int bioDiversiteitScore;
        private int aantalWandelpaden = 0;
        private List<string> faciliteiten = new List<string>();
        private bool IsSpeeltuin;
        private bool IsPicknickZone;
        private bool IsInfoborden;
        private bool IsAndereFaciliteit;
        private bool opgenomenInWandelroute;
        private int bezoekersScore = 0;

        //variabelen innovatief wonen

        private int aantalWooneenheden = 0;
        private bool isCohousing;
        private bool isModulairWonen;
        private List<string> geselecteerdeWooneenheden;
        private bool isRondleidingenMogelijk;
        private bool isShowWoningBeschikbaar;
        private int architecturaleInnoscore;
        private bool isErfgoedSamenwerking;
        private bool isToerismeSamenwerking;

        public NieuwProject(ExportManager exportManager, GebruikersManager gebruikersManager,
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory, Gebruiker ingelogdeGebruiker)
        {
            InitializeComponent();
            this.exportManager = exportManager;
            this.gebruikersManager = gebruikersManager;
            this.projectManager = projectManager;
            this.beheerMemoryFactory = beheerMemoryFactory;
            this.ingelogdeGebruiker = ingelogdeGebruiker;

            StatusComboBox.ItemsSource = projectStatussen;
            VergunningsStatusComboBox.ItemsSource = vergunningsStatussen;
            ToegankelijkheidComboBox.ItemsSource = toegankelijkheden;
        }

        //hier staan algemene methodes voor nieuwProject window

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Vraag bevestiging aan de gebruiker
            MessageBoxResult result = MessageBox.Show(
                "De ingevulde gegevens zullen verloren gaan. Wilt u sluiten?",
                "Bevestig sluiten",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            // Als gebruiker op Nee klikt → sluiten annuleren
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
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

                byte[] fileBytes = File.ReadAllBytes(bestandsPad);
                documenten.Add(fileBytes);

                bijlages.Add(Path.GetFileName(bestandsPad));
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

                byte[] fileBytes = File.ReadAllBytes(bestandsPad);
                fotos.Add(fileBytes);

                bijlages.Add(Path.GetFileName(bestandsPad));
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
            Partner partner = new(null, NaamPartnerTextBox.Text, EmailPartnerTextBox.Text,
                TelefoonPartnerTextBox.Text, PartnerRolTextBox.Text);
            partners.Add(partner);
            PartnersListBox.Items.Add(partner.Naam);
        }

        private void GaVerderButtonTab1_Click(object sender, RoutedEventArgs e)
        {
            //elke keer je op verder klikt bij algemene info checkt hij welke tabs er open moeten staan,
            //dus als er al open staan die toch niet nodig zijn, moeten die dicht

            StadsontwikkelingTab.Visibility = Visibility.Collapsed;
            GroeneRuimteTab.Visibility = Visibility.Collapsed;
            InnovatiefWonenTab.Visibility = Visibility.Collapsed;
            BevestigenTab.Visibility = Visibility.Collapsed;

            //adhv checkboxen wordt hier bepaald welke tabs opengaan
            IsStadsontwikkeling = StadsontwikkelingCheckBox.IsChecked == true;
            IsGroeneRuimte = GroeneRuimteCheckBox.IsChecked == true;
            IsInnovatiefWonen = InnovatiefWonenCheckBox.IsChecked == true;

            switch((IsStadsontwikkeling, IsGroeneRuimte, IsInnovatiefWonen))
            {
                case (true, false, false):
                    StadsontwikkelingTab.Visibility = Visibility.Visible;
                    StadsontwikkelingTab.IsSelected = true;
                    BevestigenTab.Visibility = Visibility.Visible;
                    break;
                 
                case (false, true, false):
                    GroeneRuimteTab.Visibility = Visibility.Visible;
                    GroeneRuimteTab.IsSelected = true;
                    BevestigenTab.Visibility = Visibility.Visible;
                    break;

                case (false, false, true):
                    InnovatiefWonenTab.Visibility = Visibility.Visible;
                    InnovatiefWonenTab.IsSelected = true;
                    BevestigenTab.Visibility = Visibility.Visible;
                    break;

                case (true, true, false):
                    StadsontwikkelingTab.Visibility = Visibility.Visible;
                    GroeneRuimteTab.Visibility = Visibility.Visible;
                    StadsontwikkelingTab.IsSelected = true;
                    BevestigenTab.Visibility = Visibility.Visible;
                    break;

                case (true, false, true):
                    StadsontwikkelingTab.Visibility = Visibility.Visible;
                    InnovatiefWonenTab.Visibility = Visibility.Visible;
                    StadsontwikkelingTab.IsSelected = true;
                    BevestigenTab.Visibility = Visibility.Visible;
                    break;

                case (false, true, true):
                    GroeneRuimteTab.Visibility = Visibility.Visible;
                    InnovatiefWonenTab.Visibility = Visibility.Visible;
                    GroeneRuimteTab.IsSelected = true;
                    BevestigenTab.Visibility = Visibility.Visible;
                    break;

                case (true, true, true):
                    StadsontwikkelingTab.Visibility = Visibility.Visible;
                    GroeneRuimteTab.Visibility = Visibility.Visible;
                    InnovatiefWonenTab.Visibility = Visibility.Visible;
                    StadsontwikkelingTab.IsSelected = true;
                    BevestigenTab.Visibility = Visibility.Visible;
                    break;
            }
        }

        //hieronder alles ivm tab2: Stadsontwikkeling

        //bouwfirma toevoegen
        private void VoegBouwfirmaToeButton_Click(object sender, RoutedEventArgs e)
        {
            VoegBouwfirmaToeAanLijst();

            BouwfirmaTextBox.Clear();
            EmailBouwfirmaTextBox.Clear();
            TelefoonBouwfirmaTextBox.Clear();
            WebsiteBouwfirmaTextBox.Clear();
        }

        private void VoegBouwfirmaToeAanLijst()
        {
            BouwFirma bouwfirma = new(null ,BouwfirmaTextBox.Text, EmailBouwfirmaTextBox.Text, 
                TelefoonBouwfirmaTextBox.Text, WebsiteBouwfirmaTextBox.Text);
            bouwFirmas.Add(bouwfirma);
            BouwfirmasListBox.Items.Add(bouwfirma.Naam);
        }
        //hieronder alles ivm tab3: Groene Ruimte

        private void BiodiversiteitSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int waardeBioDiv = (int)BiodiversiteitSlider.Value;
        }

        private void BezoekersBeoordelingSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int waardeBezoekersOordeel = (int)BezoekersBeoordelingSlider.Value;
        }

        //hieronder alles ivm tab4: Innovatief Wonen

        private void VoegAndereWoonvormToeButton_Click(object sender, RoutedEventArgs e)
        {
            geselecteerdeWoonvormen.Add(AndereWoonvormTextBox.Text);
            AndereWoonvormenListBox.Items.Add(AndereWoonvormTextBox.Text);

            AndereWoonvormTextBox.Clear();
        }

        private void ArchitecturaleInnoScoreSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int waardeArchInno = (int)ArchitecturaleInnoScoreSlider.Value;
            ArchInnoScoreSliderValueTextBlock.Text = ArchitecturaleInnoScoreSlider.Value.ToString();
        }

        //laatste tab heeft geen verder, maar een bevestigen knop die het project gaat proberen aan te maken

        private void MaakProjectAanButton_Click(object sender, RoutedEventArgs e)
        {
            //alle inputvariabelen algemene info opvullen

            titel = TitelInputTextBox.Text;
            startDatum = StartDatumCalendarButton.SelectedDate.Value;
            projectStatus = (ProjectStatus)StatusComboBox.SelectedItem;
            IsStadsontwikkeling = StadsontwikkelingCheckBox.IsChecked == true; //door dit zo te schrijven cancel je de optie null-waarde voor de bool
            IsGroeneRuimte = GroeneRuimteCheckBox.IsChecked == true;
            IsInnovatiefWonen = InnovatiefWonenCheckBox.IsChecked == true;
            postcode = int.Parse(PostcodeTextBox.Text);
            adres = new(StraatTextBox.Text, HuisnummerTextBox.Text, postcode, GemeenteTextBox.Text);
            wijk = WijkTextBox.Text;
            beschrijving = BeschrijvingTextBox.Text;
            foreach (string bijlage in BijlagesListBox.Items) bijlages.Add(bijlage);
            VoegPartnerToeAanLijst();   //voeg partner uit de inputvelden ook toe aan de lijst van partners en geef die lijst hier dan mee

            //variabelen stadsontwikkeling

            bouwfirma = BouwfirmaTextBox.Text;
            emailBouwfirma = EmailBouwfirmaTextBox.Text;
            telefoonBouwfirma = TelefoonBouwfirmaTextBox.Text;
            websiteBouwfirma = WebsiteBouwfirmaTextBox.Text;
            vergunningsStatus = (VergunningsStatus)VergunningsStatusComboBox.SelectedItem;
            if (ArchitecturaleWaardeJaRadioButton.IsChecked == true) IsArchitecturaleWaarde = true;
            else if (ArchitecturaleWaardeNeeRadioButton.IsChecked == true) IsArchitecturaleWaarde = false;
            toegankelijkheid = (Toegankelijkheid)ToegankelijkheidComboBox.SelectedItem;
            if (ToeristischeBezienswaardigheidJaRadioButton.IsChecked == true) IsToeristischeBezienswaardigheid = true;
            else if (ToeristischeBezienswaardigheidNeeRadioButton.IsChecked == true) IsToeristischeBezienswaardigheid = false;
            if (UitlegbordCheckBox.IsChecked == true || InfowandelingCheckBox.IsChecked == true) IsUitlegbordOfWandeling = true;

            //variabelen groene ruimte

            oppervlaktInVierkanteMeter = double.Parse(OppervlakteTextBox.Text);
            bioDiversiteitScore = (int)BiodiversiteitSlider.Value;
            aantalWandelpaden = int.Parse(AantalWandelpadenTextBox.Text);
            IsSpeeltuin = SpeeltuinFaciliteitCheckBox.IsChecked == true;
            if (IsSpeeltuin) faciliteiten.Add("Speeltuin");
            IsPicknickZone = PicknickZoneFaciliteitCheckBox.IsChecked == true;
            if (IsPicknickZone) faciliteiten.Add("Picknickzone");
            IsInfoborden = InfobordenFaciliteitCheckBox.IsChecked == true;
            if (IsInfoborden) faciliteiten.Add("Infoborden");
            IsAndereFaciliteit = AndereFaciliteitCheckBox.IsChecked == true;
            if (IsAndereFaciliteit) faciliteiten.Add(AndereFaciliteitTextBox.Text);
            if (ToeristischeWandelroutesJaRadioButton.IsChecked == true) opgenomenInWandelroute = true;
            else if (ToeristischeWandelroutesNeeRadioButton.IsChecked == true) opgenomenInWandelroute = false;
            bezoekersScore = (int)BezoekersBeoordelingSlider.Value;

            //variabelen innovatief wonen

            aantalWooneenheden = int.Parse(AantalWooneenhedenTextBox.Text);
            isCohousing = CohousingCheckBox.IsChecked == true;
            if (isCohousing) geselecteerdeWooneenheden.Add("Co-housing");
            isModulairWonen = ModulairWonenCheckBox.IsChecked == true;
            if (isModulairWonen) geselecteerdeWooneenheden.Add("Modulair wonen");
            //de andere staan al in de lijst vanuit de methode VoegAndereWoonvormToeButton_Click onder innovatief wonen
            if (RondleidingJaRadioButton.IsChecked == true) isRondleidingenMogelijk = true;
            else if (RondeleidingNeeRadioButton.IsChecked == true) isRondleidingenMogelijk = false;
            if (ShowwoningJaRadioButton.IsChecked == true) isShowWoningBeschikbaar = true;
            else if (ShowwoningNeeRadioButton.IsChecked == true) isShowWoningBeschikbaar = false;
            architecturaleInnoscore = (int)ArchitecturaleInnoScoreSlider.Value;
            if (ErfgoedJaRadioButton.IsChecked == true) isErfgoedSamenwerking = true;
            else if (ErfgoedNeeRadioButton.IsChecked == true) isErfgoedSamenwerking = false;
            if (ToerismeGentJaRadioButton.IsChecked == true) isToerismeSamenwerking = true;
            else if (ToerismeGentNeeRadioButton.IsChecked == true) isToerismeSamenwerking = false;

            //alle invoer + logica projectTypes adhv checkboxen types

            BepaalTypeProjectEnMaakAan(IsStadsontwikkeling, IsGroeneRuimte, IsInnovatiefWonen);
        }

        private void BepaalTypeProjectEnMaakAan(bool IsStadsontwikkeling, bool IsGroeneRuimte, bool IsInnovatiefWonen)
        {
            switch ((IsStadsontwikkeling, IsGroeneRuimte, IsInnovatiefWonen))
            {
                case (true, false, false):
                    projectManager.MaakStadsontwikkelingsProjectAan
                        (titel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, vergunningsStatus, 
                        IsArchitecturaleWaarde, toegankelijkheid, IsToeristischeBezienswaardigheid, IsUitlegbordOfWandeling, 
                        bouwFirmas, ingelogdeGebruiker, adres);
                    break;

                case (false, true, false):
                    projectManager.MaakGroeneruimteProjectAan
                        (titel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, 
                        oppervlaktInVierkanteMeter, bioDiversiteitScore, aantalWandelpaden, opgenomenInWandelroute,
                        bezoekersScore, faciliteiten, ingelogdeGebruiker, adres);
                    break;

                case (false, false, true):
                    projectManager.MaakInnovatiefWonenProjectAan
                        (titel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, 
                        aantalWooneenheden, isRondleidingenMogelijk, architecturaleInnoscore, isShowWoningBeschikbaar, 
                        isErfgoedSamenwerking, isToerismeSamenwerking, geselecteerdeWooneenheden, ingelogdeGebruiker, 
                        adres);
                    break;

                case (true, true, false):
                    projectManager.MaakStadsOntwikkelingGroeneRuimteProjectAan
                        (titel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, ingelogdeGebruiker,
                        adres, vergunningsStatus, IsArchitecturaleWaarde, toegankelijkheid, IsToeristischeBezienswaardigheid, 
                        IsUitlegbordOfWandeling, bouwFirmas, oppervlaktInVierkanteMeter, bioDiversiteitScore, aantalWandelpaden, 
                        opgenomenInWandelroute, bezoekersScore, faciliteiten);
                    break;

                case (true, false, true):
                    projectManager.MaakStadsOntwikkelingInnovatiefWonenProjectAan
                        (titel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, vergunningsStatus, 
                        IsArchitecturaleWaarde, toegankelijkheid, IsToeristischeBezienswaardigheid,
                        IsUitlegbordOfWandeling, bouwFirmas, aantalWooneenheden, isRondleidingenMogelijk, architecturaleInnoscore, 
                        isShowWoningBeschikbaar, isErfgoedSamenwerking, isToerismeSamenwerking, geselecteerdeWooneenheden, 
                        ingelogdeGebruiker,
                        adres);
                    break;

                case (false, true, true):
                    projectManager.MaakGroeneRuimteInnovatiefWonenProjectAan
                        (titel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, 
                        oppervlaktInVierkanteMeter, bioDiversiteitScore, aantalWandelpaden, opgenomenInWandelroute,
                        bezoekersScore, faciliteiten, aantalWooneenheden, isRondleidingenMogelijk, architecturaleInnoscore,
                        isShowWoningBeschikbaar, isErfgoedSamenwerking, isToerismeSamenwerking, geselecteerdeWooneenheden, 
                        ingelogdeGebruiker, adres);
                    break;

                case (true, true, true):
                    projectManager.MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject
                        (titel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, 
                        oppervlaktInVierkanteMeter, bioDiversiteitScore, aantalWandelpaden, opgenomenInWandelroute,
                        bezoekersScore, faciliteiten, vergunningsStatus, IsArchitecturaleWaarde, toegankelijkheid, 
                        IsToeristischeBezienswaardigheid, IsUitlegbordOfWandeling, bouwFirmas, aantalWooneenheden, 
                        isRondleidingenMogelijk, architecturaleInnoscore, isShowWoningBeschikbaar, isErfgoedSamenwerking, 
                        isToerismeSamenwerking, geselecteerdeWooneenheden, ingelogdeGebruiker, adres);
                    break;

            }
        }

        
    }
}
