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
            
            if (gebruiker != null)
            {
                
                if(gebruiker.GebruikersRol == GebruikersRol.Beheerder)
                {
                    BeheerderHomeProjectBeheer beheerderHomeProjectBeheer = new
                        (exportManager, gebruikersManager, projectManager, beheerMemoryFactory);
                    beheerderHomeProjectBeheer.Show();
                }
                    
                else
                {
                    HomeProjectBeheer homeProjectBeheer = new 
                        (exportManager, gebruikersManager, projectManager, beheerMemoryFactory);
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