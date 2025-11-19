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
    /// Interaction logic for ProjectFiche.xaml
    /// </summary>
    public partial class ProjectFiche : Window
    {


        public ProjectFiche(Project selectedproject)
        {
            InitializeComponent();

            //de datacontext is het source object voor alle data bindings binnen een control (button/listbox/datagrid...)
            //hier ken i khet geselecteerde project toe aan de datacontext van het propertygrid
            ProductFichePropertyGrid.DataContext = selectedproject;

        }


    }
}
