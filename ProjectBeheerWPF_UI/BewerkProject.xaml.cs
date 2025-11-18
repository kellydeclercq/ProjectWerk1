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
using ProjectBeheerBL.Beheerder;
using ProjectBeheerBL.Domein;
using ProjectBeheerUtils;

namespace ProjectBeheerWPF_UI
{
    /// <summary>
    /// Interaction logic for BewerkProject.xaml
    /// </summary>
    public partial class BewerkProject : Window
    {
        private Project project;

        public BewerkProject(ExportManager exportManager, GebruikersManager gebruikersManager,
            ProjectManager projectManager, BeheerMemoryFactory beheerMemoryFactory)
        {
            InitializeComponent();
        }

        public BewerkProject(Project project)
        {
            this.project = project;
        }
    }
}
