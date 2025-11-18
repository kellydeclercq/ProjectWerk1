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
using ProjectBeheerWPF_UI.BeheerderUI;

namespace ProjectBeheerWPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GebruikersManager _gebruikersManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = LoginEmailTextBox.Text;
            var gebruiker = _gebruikersManager.GeefGebruikeradhvEmail(email);
            
            if (gebruiker != null)
            {
                
                if(gebruiker.GebruikersRol == GebruikersRol.Beheerder)
                {
                    BeheerderHomeProjectBeheer beheerderHomeProjectBeheer = new();
                    beheerderHomeProjectBeheer.Show();
                }
                    
                else
                {
                    HomeProjectBeheer homeProjectBeheer = new HomeProjectBeheer();
                    homeProjectBeheer.Show();
                }
            }
            else
            {
                MessageBox.Show("Dit e-mailadres is niet gekend", "E-mailadres niet gekend", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}