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
        private Gebruiker gebruiker;
        private Gebruiker ingelogdeGebruiker;

        public DetailsGebruiker()
        {
            InitializeComponent();
        }

        public DetailsGebruiker(Gebruiker gebruiker, Gebruiker ingelogdeGebruiker)
        {
            this.gebruiker = gebruiker;
            this.ingelogdeGebruiker = ingelogdeGebruiker;
        }
    }
}
