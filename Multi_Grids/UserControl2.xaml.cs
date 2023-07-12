using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Multi_Grids
{
    /// <summary>
    /// UserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        private ScatterPlotViewModel viewModel;
        public UserControl2()
        {
            InitializeComponent();
            viewModel = new ScatterPlotViewModel();
            DataContext = viewModel;
        }


        public class ScatterPoint
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        public class ScatterPlotViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private List<ScatterPoint> scatterPoints;
            public List<ScatterPoint> ScatterPoints
            {
                get { return scatterPoints; }
                set
                {
                    scatterPoints = value;
                    OnPropertyChanged(nameof(ScatterPoints));
                }
            }

            public ScatterPlotViewModel()
            {
                // 初始化散点数据
                //ScatterPoints = new List<ScatterPoint>
                //{
                //    new ScatterPoint { X = 1, Y = 3 },
                //    new ScatterPoint { X = 2, Y = 5 },
                //    new ScatterPoint { X = 3, Y = 2 },
                //    // ...
                //};

                Random random = new Random();
                ScatterPoints = new List<ScatterPoint>();
                for (int i = 0; i < 100; i++)
                {
                    double x = random.NextDouble() * 102.4;
                    double y = random.NextDouble() * 102.4;
                    var newpoint = new ScatterPoint { X = x, Y = y };
                    ScatterPoints.Add(newpoint);
                }


            }



            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void GeneratePoints()
        {
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                var ScatterPoints = new List<ScatterPoint>();
                double x = random.NextDouble() * scatterPlotCanvas.Width;
                double y = random.NextDouble() * scatterPlotCanvas.Height;
                var newpoint = new ScatterPoint { X = x, Y = y };
                ScatterPoints.Add(newpoint);
            }
        }
    }
}
