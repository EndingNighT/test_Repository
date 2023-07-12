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

namespace Multi_Grids
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            Draw2daxis();
            DrawScatterPlot();
        }


        private void Draw2daxis()
        {
            //Path 
            Path path_x = new Path
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            PathGeometry pathGeometry = new PathGeometry();

            PathFigure pathFigure = new PathFigure
            {
                StartPoint = new Point(20, 980)
            };

            LineSegment lineSegment = new LineSegment
            {
                Point = new Point(980, 980)
            };

            pathFigure.Segments.Add(lineSegment);
            pathGeometry.Figures.Add(pathFigure);


            path_x.Data = pathGeometry;

            scatterPlotCanvas.Children.Add(path_x);


            Path path_y = new Path
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            PathGeometry pathGeometry2 = new PathGeometry();

            PathFigure pathFigure2 = new PathFigure
            {
                StartPoint = new Point(20, 20)
            };

            LineSegment lineSegment2 = new LineSegment
            {
                Point = new Point(20, 980)
            };

            pathFigure2.Segments.Add(lineSegment2);
            pathGeometry2.Figures.Add(pathFigure2);

            path_y.Data = pathGeometry2;
            scatterPlotCanvas.Children.Add(path_y);

            //Canvas.SetLeft(line_x, x);
            //Canvas.SetTop(line_y, y);

            var x = scatterPlotCanvas.Width - 20;
            var y = scatterPlotCanvas.Width - 20;



            for (int i = 0; i < 10; i++)
            {
                TextBlock x_num = new TextBlock();
                TextBlock y_num = new TextBlock();
                //x_num


            }




        }


        private void DrawScatterPlot()
        {
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 5;
                ellipse.Height = 5;
                ellipse.Fill = Brushes.LightBlue;

                double x = random.NextDouble() * (scatterPlotCanvas.Width - 20);
                double y = random.NextDouble() * (scatterPlotCanvas.Height - 20);

                Canvas.SetLeft(ellipse, x);
                Canvas.SetTop(ellipse, y);

                scatterPlotCanvas.Children.Add(ellipse);

                //scatterPlotCanvas.

            }
        }

        private void cleanPoints()
        {
            scatterPlotCanvas.Children.Clear();
        }
    }
}
