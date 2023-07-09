using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        bool IsDrawingGo = false;

        bool IsDrawingGo2 = false;

        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            //AddEllipse();

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

                    scatterPlotCanvas.Children.Clear();
                    scatterPlotCanvas2.Children.Clear();
                }


            };

            //DrawScatterPlot();
            DrawScatterPlot2();

        }


        private ObservableCollection<EllipseData> ellipseDataCollection;

        private void DrawScatterPlot2()
        {
            double x;
            double y;

            for (int i = 0;i < 8192;i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 3;
                ellipse.Height = 3;
                ellipse.Fill = Brushes.Red;
                scatterPlotCanvas2.Children.Add(ellipse);
            }

            foreach (Ellipse ellipse in scatterPlotCanvas2.Children.OfType<Ellipse>())
            {
                x = random.NextDouble() * (scatterPlotCanvas.Width - 6);
                y = random.NextDouble() * (scatterPlotCanvas.Height - 6);

                Canvas.SetLeft(ellipse, x);
                Canvas.SetTop(ellipse, y);
            }

            scatterPlotCanvas2.Visibility = Visibility.Collapsed;

            Task.Run(async() =>
            {

                while (true)
                {

                    if (IsDrawingGo2)
                    {

                        
                        Dispatcher.Invoke(new Action(() =>
                        {
                            scatterPlotCanvas2.Visibility = Visibility.Visible;
                            foreach (Ellipse ellipse in scatterPlotCanvas2.Children.OfType<Ellipse>())
                            {
                                x = random.NextDouble() * (scatterPlotCanvas.Width - 6);
                                y = random.NextDouble() * (scatterPlotCanvas.Height - 6);

                                Canvas.SetLeft(ellipse, x);
                                Canvas.SetTop(ellipse, y);
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



        private void DrawScatterPlot()
        {

            Task.Run(async () =>
            {
                while(!IsDisposed)
                {
                    if (IsDrawingGo)
                    {
                        //获取点数据
                        //double[] x = await getpoint();
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
                        await Task.Delay(1000);

                    
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
