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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleGenerator
{
    /// <summary>
    /// Interaction logic for BoxModuleView.xaml
    /// </summary>
    public partial class SphereModuleView : UserControl
    {
        public SphereModuleView()
        {
            InitializeComponent();

            this.DataContext = new SphereModuleViewModel();
        }
    }
}
