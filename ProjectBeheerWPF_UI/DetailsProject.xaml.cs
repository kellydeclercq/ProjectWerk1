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
    /// Interaction logic for DetailsProject.xaml
    /// </summary>
    public partial class DetailsProject : Window
    {
        private Project project;

        public DetailsProject()
        {
            InitializeComponent();
        }

        public DetailsProject(Project project)
        {
            this.project = project;
        }
    }
}
