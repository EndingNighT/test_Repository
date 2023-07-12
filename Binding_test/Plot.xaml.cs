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

namespace Binding_test
{
    /// <summary>
    /// plot.xaml 的交互逻辑
    /// </summary>
    public partial class Plot : Window
    {
        public Plot()
        {
            InitializeComponent();
            binding_point();
        }


        void binding_point()
        {
            scatterPlotCanvas.Children.Clear();


        }

    }
}
