using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace ScatterPlot_CanvasTest
{
    public class EllipseData
    {
        public double X { get; set; }
        public double Y { get; set; }
    }


    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<EllipseData> EllipseDataCollection { get; } = new ObservableCollection<EllipseData>();

        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged implementation omitted for brevity
    }



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , IDisposable
    {
        bool IsDisposed = false;
        bool IsPause = false;

        bool IsDrawingGo = false;

        bool IsDrawingGo2 = false;

        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            //AddEllipse();

            this.ResizeMode = ResizeMode.CanMinimize;
            //this.SizeToContent = SizeToContent.WidthAndHeight;

            DataContext = new ViewModel();

                

            checkBoxScatter.Click += (s, e) =>
            {

                if (checkBoxScatter.IsChecked.Value)
                {
                    IsDrawingGo = true;
                    IsDrawingGo2 = true;
                }
                else
                {
                    IsDrawingGo = false;
                    IsDrawingGo2 = false;


                }


            };

            //DrawScatterPlot();
            DrawScatterPlot2();

        }

        (double[], double[]) generatePoints()
        {
            Random random = new Random();
            double[] points_x = new double[8192];
            double[] points_y = new double[8192];
            for (int i = 0; i < 8192; i++)
            {
                double x = random.NextDouble();
                double y = random.NextDouble();
                points_x[i] = x;
                points_y[i] = y;
            }
            return (points_x, points_y);
        }

        public class csvPointsClass
        {
            List<double[]> points_list = new List<double[]>();
            int loop = 0;

            public csvPointsClass()
            {
                readfromcsv();
                loop = 0;
            }

            private void readfromcsv()
            {

                double[] x1 = new double[8192];
                double[] y1 = new double[8192];
                double[] x2 = new double[8192];
                double[] y2 = new double[8192];
                double[] x3 = new double[8192];
                double[] y3 = new double[8192];

                try
                {
                    using (StreamReader reader = new StreamReader("C:\\Users\\chenxy\\Desktop\\spectrum data\\p1.csv"))
                    {
                        int lineNumber = 0;
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();

                            string[] fields = line.Split(',');
                            double value;
                            if (double.TryParse(fields[0], out value))
                            {
                                x1[lineNumber - 1] = value;
                            }
                            if (double.TryParse(fields[1], out value))
                            {
                                y1[lineNumber - 1] = value;
                            }
                            if (double.TryParse(fields[2], out value))
                            {
                                x2[lineNumber - 1] = value;
                            }
                            if (double.TryParse(fields[3], out value))
                            {
                                y2[lineNumber - 1] = value;
                            }
                            if (double.TryParse(fields[4], out value))
                            {
                                x3[lineNumber - 1] = value;
                            }
                            if (double.TryParse(fields[5], out value))
                            {
                                y3[lineNumber - 1] = value;
                            }
                            lineNumber++;
                        }
                    }
                }
                catch
                {

                }


                points_list.Add(x1);
                points_list.Add(y1);
                points_list.Add(x2);
                points_list.Add(y2);
                points_list.Add(x3);
                points_list.Add(y3);
            }

            public (double[], double[]) generatePoints2()
            {
                
                double[] points_x = new double[8192];
                double[] points_y = new double[8192];

                if (loop == 0)
                {
                    points_x = points_list[0];
                    points_y = points_list[1];
                }
                if (loop == 1)
                {
                    points_x = points_list[2];
                    points_y = points_list[3];
                }
                if (loop == 2)
                {
                    points_x = points_list[4];
                    points_y = points_list[5];
                }

                loop = (loop + 1) % 3;

                return (points_x, points_y);
            }
        }



        private ObservableCollection<EllipseData> ellipseDataCollection;

        private void DrawScatterPlot2()
        {
            double x;
            double y;
            double distance;
            double MAXdistance;
            byte c;
            var centerX = scatterPlotCanvas.Width / 2;
            var centerY = scatterPlotCanvas.Height / 2;

            double[] points_x = new double[8192];
            double[] points_y = new double[8192];

            var brushes = new SolidColorBrush[8192];

            csvPointsClass csvPoints = new csvPointsClass();

            //初始化
            for (int i = 0;i < 8192;i++)
            {
                brushes[i] = new SolidColorBrush();
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 3;
                ellipse.Height = 3;
                ellipse.Fill = brushes[i];
                scatterPlotCanvas2.Children.Add(ellipse);
            }

            scatterPlotCanvas2.Visibility = Visibility.Collapsed;



            var t = Task.Run(async() =>
            {

                while (true)
                {



                    if (IsDrawingGo2)
                    {

                        //(points_x, points_y) = generatePoints();

                        (points_x, points_y) = csvPoints.generatePoints2();

                        Dispatcher.Invoke(new Action(() =>
                        {
                            scatterPlotCanvas2.Visibility = Visibility.Visible;
                            int i = 0;
                            foreach (Ellipse ellipse in scatterPlotCanvas2.Children.OfType<Ellipse>())
                            {

                                //归一化 再 映射到Canvas上
                                //(原始值 - 最小值) / (最大值 - 最小值)
                                var x_ = (points_x[i] - (-15)) / 30;//x -15~15
                                var y_ = (points_y[i] - (-160)) / 100;//y -152~-73

                                x = x_ * (scatterPlotCanvas.ActualWidth - 6);
                                y = y_ * (scatterPlotCanvas.ActualWidth - 6);
                                y = scatterPlotCanvas.ActualWidth - y;
                                //x = points_x[i] * (scatterPlotCanvas.Width - 6);
                                //y = points_y[i] * (scatterPlotCanvas.Height - 6);



                                if (double.IsNaN(x) || double.IsInfinity(x) || double.IsNaN(y) || double.IsInfinity(y))
                                {
                                    i++;
                                    continue;
                                }

                                Canvas.SetLeft(ellipse, x);
                                Canvas.SetTop(ellipse, y);

                                distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
                                MAXdistance = Math.Sqrt(Math.Pow(centerX, 2) + Math.Pow(centerY, 2));
                                c = (byte)(int)((distance / MAXdistance) * 255);


                                //Color color = Color.FromRgb(255, r, r);
                                //SolidColorBrush brush = new SolidColorBrush(color);
                                //ellipse.Fill = brush;

                                brushes[i].Color = Color.FromRgb(255, c, c);
                                i++;
                            }
                        }));
                        await Task.Delay(200);
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            scatterPlotCanvas2.Visibility = Visibility.Collapsed;

                        }));
                        await Task.Delay(1000);
                    }
                        

                }

                
            });



        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 3;
            ellipse.Height = 3;
            ellipse.Fill = Brushes.Red;

            Canvas.SetLeft(ellipse, 10);
            Canvas.SetTop(ellipse, 20);

            scatterPlotCanvas.Visibility = Visibility.Visible;

            scatterPlotCanvas.Children.Add(ellipse);


        }


        private void Button_Click_Pause(object sender, RoutedEventArgs e)
        {
            IsPause = true;
        }

        private void DrawScatterPlot()
        {

            Task.Run(async () =>
            {
                while(!IsDisposed)
                {
                    if (IsDrawingGo)
                    {
                        //获取点数据
                        //double[] x = await random.NextDouble();
                        //double[] y = ...

                        Dispatcher.Invoke(new Action(() =>
                        {

                            scatterPlotCanvas.Children.Clear();
                            

                            for (int i = 0; i < 8192; i++)
                            {
                                Ellipse ellipse = new Ellipse();
                                ellipse.Width = 3;
                                ellipse.Height = 3;
                                ellipse.Fill = Brushes.Red;

                                double x = random.NextDouble() * (scatterPlotCanvas.Width - 6);
                                double y = random.NextDouble() * (scatterPlotCanvas.Height - 6);

                                Canvas.SetLeft(ellipse, x);
                                Canvas.SetTop(ellipse, y);

                                scatterPlotCanvas.Children.Add(ellipse);
                            }


                        }));
                        await Task.Delay(200);
                    }
                    else
                    {
                        await Task.Delay(1000);
                        Dispatcher.Invoke(new Action(() =>
                        {
                            if (scatterPlotCanvas.Children.Count != 0)
                                scatterPlotCanvas.Children.Clear();
                        }));
                    }
                        

                    
                }

            });
            

        }

        /// <summary>
        /// bad 
        /// </summary>
        private void PointCloudUpdate()
        {
            double[] spectrum_points_x = new double[8192];
            double[] spectrum_points_y = new double[8192];

            var centerX = scatterPlotCanvas.Width / 2;
            var centerY = scatterPlotCanvas.Height / 2;

            Task.Run(async () =>
            {
                while (!IsDisposed)
                {

                    if (IsDrawingGo)//IsEnablePointCloudUpdate
                    {
                        try
                        {
                            //var spec = JYPanelProccessor.GetSpectrum();

                            (spectrum_points_x, spectrum_points_y) = generatePoints();

                            //spec.status = true;
                            //if (!spec.status)
                            //{
                            //    continue;
                            //}
                            //spectrum_points_x = spec.point_x;
                            //spectrum_points_y = spec.point_y;

                            Dispatcher.Invoke(new Action(() =>
                            {

                                scatterPlotCanvas.Children.Clear();

                                for (int i = 0; i < 8192; i++)//8192 spectrum.point_x.Length
                                {
                                    Ellipse ellipse = new Ellipse();
                                    ellipse.Width = 3;
                                    ellipse.Height = 3;
                                    ellipse.Fill = Brushes.Red;


                                    //归一化 再 映射到Canvas上
                                    //var x_ = spectrum_points_x[i] / 30 + 0.5;//x -15~15
                                    //var y_ = spectrum_points_y[i] / 160 + 1;//y -152~-73
                                    //double x = x_ * (scatterPlotCanvas.ActualWidth - 6);
                                    //double y = y_ * (scatterPlotCanvas.ActualWidth - 6);

                                    double x = spectrum_points_x[i] * (scatterPlotCanvas.ActualWidth - 6);
                                    double y = spectrum_points_y[i] * (scatterPlotCanvas.ActualWidth - 6);

                                    double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
                                    double MAXdistance = Math.Sqrt(Math.Pow(centerX, 2) + Math.Pow(centerY, 2));
                                    byte r = (byte)(int)((distance / MAXdistance) * 255);
                                    Color color = Color.FromRgb(255, r, r);
                                    SolidColorBrush brush = new SolidColorBrush(color);
                                    ellipse.Fill = brush;

                                    Canvas.SetLeft(ellipse, x);
                                    Canvas.SetTop(ellipse, y);

                                    scatterPlotCanvas.Children.Add(ellipse);

                                }
                            }));

                        }
                        catch { }
                        await Task.Delay(200);
                    }
                    else
                    {
                        await Task.Delay(1000);
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                if (scatterPlotCanvas.Children.Count != 0)
                                    scatterPlotCanvas.Children.Clear();
                            }));
                        }

                    }

                }
            });


        }






        public void Dispose()
        {
            IsDisposed = true;

            throw new NotImplementedException();
        }


    }
}
