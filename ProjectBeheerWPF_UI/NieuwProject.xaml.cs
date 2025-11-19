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
        //lijst met partners bijhouden hier, gewone strings die je kan weergeven in de listbox
        public List<Partner> partners;

        public NieuwProject(ExportManager exportManager, GebruikersManager gebruikersManager, 
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory)
        {
            InitializeComponent();
            this.exportManager = exportManager;
            this.gebruikersManager = gebruikersManager;
            this.projectManager = projectManager;
            this.beheerMemoryFactory = beheerMemoryFactory;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            throw new NotImplementedException();
        }

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

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void VoegPartnerToeButton_Click(object sender, RoutedEventArgs e)
        {
            Partner partner = new(NaamPartnerTextBox.Text, EmailPartnerTextBox.Text, 
                TelefoonPartnerTextBox.Text, PartnerRolTextBox.Text);
            partners.Add(partner);
            PartnersListBox.Items.Add(partner.Naam);
            NaamPartnerTextBox.Clear();
            EmailPartnerTextBox.Clear();
            TelefoonPartnerTextBox.Clear();
            PartnerRolTextBox.Clear();
        }

        private void GaVerderButtonTab1_Click(object sender, RoutedEventArgs e)
        {
            GaVerderButtonTab_Click(sender, e);
        }

        private void GaVerderButtonTab2_Click(object sender, RoutedEventArgs e)
        {
            GaVerderButtonTab_Click(sender, e);
        }

        private void GaVerderButtonTab3_Click(object sender, RoutedEventArgs e)
        {
            GaVerderButtonTab_Click(sender, e);
        }

        private void GaVerderButtonTab_Click(object sender, RoutedEventArgs e)
        {
            if (NieuwProjectTabs.SelectedIndex > NieuwProjectTabs.Items.Count - 1)
            {
                NieuwProjectTabs.SelectedIndex += 1;
            }
        }

        private void MaakProjectAanButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
