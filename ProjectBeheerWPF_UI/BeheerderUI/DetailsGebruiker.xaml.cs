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
using ProjectBeheerBL.Domein;

namespace ProjectBeheerWPF_UI.BeheerderUI
{
    /// <summary>
    /// Interaction logic for DetailsGebruiker.xaml
    /// </summary>
    public partial class DetailsGebruiker : Window
    {
        private Gebruiker selectedGebruiker;
        private Gebruiker ingelogdeGebruiker;

        public DetailsGebruiker(Gebruiker selectedGebruiker, Gebruiker ingelogdeGebruiker)
        {
            InitializeComponent();
            this.DataContext = selectedGebruiker;

            this.selectedGebruiker = selectedGebruiker;
            this.ingelogdeGebruiker = ingelogdeGebruiker;
        }

        //public DetailsGebruiker(Gebruiker gebruiker, Gebruiker ingelogdeGebruiker)
        //{
        //    this.gebruiker = gebruiker;
        //    this.ingelogdeGebruiker = ingelogdeGebruiker;
        //}
    }
}
