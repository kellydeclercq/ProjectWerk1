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

namespace ProjectBeheerWPF_UI
{
    /// <summary>
    /// Interaction logic for ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        private Project project;

        public ExportWindow()
        {
            InitializeComponent();
        }

        public ExportWindow(Project project)
        {
            this.project = project;
        }

        private void BladerenButton_Click(object sender, RoutedEventArgs e)
        {
            //als de gebruiker een admin is ga naar alle porjecten
            //als een gebruiker geen admin is ga naar al de gebruiker zijn projecten
            
        }

        private void ExporteerAlsCSVButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExporteerAlsPDFButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
