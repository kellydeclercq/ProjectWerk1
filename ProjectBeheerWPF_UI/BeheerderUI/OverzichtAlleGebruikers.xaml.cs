using System;
using System.CodeDom;
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
using ProjectBeheerBL.Beheerder;
using ProjectBeheerBL.Domein;
using ProjectBeheerUtils;

namespace ProjectBeheerWPF_UI.BeheerderUI
{
    /// <summary>
    /// Interaction logic for OverzichtAlleGebruikers.xaml
    /// </summary>
    public partial class OverzichtAlleGebruikers : Window
    {

        private ExportManager exportManager;
        private GebruikersManager gebruikersManager;
        private ProjectManager projectManager;
        private Gebruiker ingelogdeGebruiker;

        private List<Gebruiker> gebruikers = new List<Gebruiker>();
        public OverzichtAlleGebruikers(ExportManager exportManager, GebruikersManager gebruikersManager,
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory, Gebruiker ingelogdeGebruiker)
        {
            InitializeComponent();
            this.exportManager = exportManager;
            this.gebruikersManager = gebruikersManager;
            this.projectManager = projectManager;
            this.ingelogdeGebruiker = ingelogdeGebruiker;
            
            gebruikers = gebruikersManager.GeefAlleGebruikers();
            GebruikerOverzichtDatagrid.ItemsSource = gebruikers;

        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            if (GebruikerOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om details te kunnen bekijken.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (GebruikerOverzichtDatagrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Je kan geen meerdere projecten selecteren om details te bekijken", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Gebruiker gebruiker = GebruikerOverzichtDatagrid.SelectedItem as Gebruiker;
                if (gebruiker != null)
                {
                    //navigeer naar DetailsProject
                    var detailsGebruiker = new DetailsGebruiker(gebruiker, ingelogdeGebruiker);
                    detailsGebruiker.Show();
                }
            }
        }

        private void Bewerk_Click(object sender, RoutedEventArgs e)
        {
            if (GebruikerOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om te kunnen bewerken.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (GebruikerOverzichtDatagrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Je kan geen meerdere projecten selecteren om te bewerken.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Gebruiker gebruiker = GebruikerOverzichtDatagrid.SelectedItem as Gebruiker;
                if (gebruiker != null)
                {
                    //navigeer naar BewerkProject
                    var bewerkGebruiker = new BewerkGebruiker(gebruiker, ingelogdeGebruiker);
                    bewerkGebruiker.Show();
                }
            }
        }

        private void Verwijder_Click(object sender, RoutedEventArgs e)
        {

            if (GebruikerOverzichtDatagrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer eerst een project om te verwijderen.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            var x = GebruikerOverzichtDatagrid.SelectedItems;

            foreach (var item in x)
            {
                Gebruiker g = item as Gebruiker;
                if (g != null)
                {
                    //navigeer naar BewerkProject
                 var result = MessageBox.Show("Bent u zeker dat u deze wilt verwijderen?.", "Waarschuwing",  MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        gebruikers.Remove(g);
                    }                   
                }

            }


           

        }
    }
}
