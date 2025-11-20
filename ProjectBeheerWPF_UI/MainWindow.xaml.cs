using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectBeheerBL.Beheerder;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerUtils;
using ProjectBeheerWPF_UI.BeheerderUI;

namespace ProjectBeheerWPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExportManager exportManager;
        private GebruikersManager gebruikersManager;
        private ProjectManager projectManager;
        private BeheerMemoryFactory beheerMemoryFactory = new();
        private Gebruiker ingelogdeGebruiker;

        public MainWindow()
        {
            InitializeComponent();

            //nieuwe managers aanmaken
            exportManager = new ExportManager();
            gebruikersManager = new GebruikersManager(beheerMemoryFactory.GeefGebruikerRepo());
            projectManager = new ProjectManager(beheerMemoryFactory.GeefProjectRepo());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = LoginEmailTextBox.Text;
            var gebruiker = gebruikersManager.GeefGebruikeradhvEmail(email);
            ingelogdeGebruiker = gebruiker;

            if (ingelogdeGebruiker != null)
            {
                //TODO niet twee projectbeheer windows, maar 1, twee extra knoppen hidden zetten
                if(ingelogdeGebruiker.GebruikersRol == GebruikersRol.Beheerder)
                {
                    BeheerderHomeProjectBeheer beheerderHomeProjectBeheer = new
                        (exportManager, gebruikersManager, projectManager, beheerMemoryFactory, ingelogdeGebruiker);
                    beheerderHomeProjectBeheer.Show();
                }
                    
                else
                {
                    HomeProjectBeheer homeProjectBeheer = new 
                        (exportManager, gebruikersManager, projectManager, beheerMemoryFactory, ingelogdeGebruiker);
                    homeProjectBeheer.Show();
                }
                Close();
            }
            else
            {
                MessageBox.Show("Dit e-mailadres is niet gekend", "E-mailadres niet gekend", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}