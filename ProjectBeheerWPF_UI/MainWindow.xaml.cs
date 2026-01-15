using Microsoft.Extensions.Configuration;
using ProjectBeheerBL.Beheerder;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerDL_SQL;
using ProjectBeheerUtils;
using ProjectBeheerWPF_UI.BeheerderUI;
using System.IO;
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
        private BeheerMemoryFactory beheerMemoryFactory;
        private Gebruiker ingelogdeGebruiker;

        public MainWindow()
        {
            InitializeComponent();

            IConfigurationRoot config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
             .Build();

            string connectionString = config.GetConnectionString("SQLserver");


            beheerMemoryFactory = new BeheerMemoryFactory(connectionString);

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

                BeheerderHomeProjectBeheer beheerderHomeProjectBeheer = new
                    (exportManager, gebruikersManager, projectManager, beheerMemoryFactory, ingelogdeGebruiker);
                beheerderHomeProjectBeheer.ShowDialog();

              
            }
            else
            {
                MessageBox.Show("Dit e-mailadres is niet gekend", "E-mailadres niet gekend", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}