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
using ProjectBeheerDL_SQL;
using ProjectBeheerUtils;

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
            Gebruiker gebruiker = gebruikersManager.GeefGebruikeradhvEmail(EmailTextBox.Text);
            if (beh)
            {

                BeheerdersWindow beheerdersWindow = new BeheerdersWindow(lid, ledenManager, eventsManager, bestellingManager);
                beheerdersWindow.Show();
                this.Close();
            }
            else if (lid != null)
            {
                UserWindow userWindow = new UserWindow(lid);
                userWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Gebruiker bestaat niet");
            }
        }
    }
}