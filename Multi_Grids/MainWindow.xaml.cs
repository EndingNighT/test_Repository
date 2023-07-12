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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            init_test();
            line();
        }


        public Window JYWinPanel;
        public Window Win_Chart;

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            text1.Text = "The CheckBox is checked.";
        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            text1.Text = "The CheckBox is unchecked.";
        }

        private void HandleThirdState(object sender, RoutedEventArgs e)
        {
            text1.Text = "The CheckBox is in the indeterminate state.";
        }



        private void OpenScatter(object sender, RoutedEventArgs e)
        {
            LineGeometry myLineGeometry = new LineGeometry();
            myLineGeometry.StartPoint = new Point(10, 20);
            myLineGeometry.EndPoint = new Point(100, 130);

            Path myPath = new Path();
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;
            myPath.Data = myLineGeometry;
        }

        private void Panel_HandleCheck(object sender, RoutedEventArgs e)
        {

        }

        private void line()
        {
            LineGeometry myLineGeometry = new LineGeometry();
            myLineGeometry.StartPoint = new Point(10, 20);
            myLineGeometry.EndPoint = new Point(100, 130);

            Path myPath = new Path();
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;
            myPath.Data = myLineGeometry;


            checkBoxOpenScatter.Click += (s, e) =>
            {
                if (checkBoxOpenScatter.IsChecked == true)
                {
                    if (Win_Chart == null)
                    {
                        Win_Chart = new Window();//Window对象

                        Win_Chart.SizeToContent = SizeToContent.Manual;

                        var panel = new UserControl1();//  点图//UserControl对象

                        Win_Chart.Width = 1000;
                        Win_Chart.Height = 1000;
                        Win_Chart.MaxHeight = 1000;
                        Win_Chart.MaxWidth = 1000;
                        Win_Chart.Topmost = true;
                        Win_Chart.Content = panel;
                        Win_Chart.Closed += (s, e) =>
                        {
                            Win_Chart = null;
                            checkBoxOpenScatter.IsChecked = false;
                        };
                        Win_Chart.Show();
                        this.Unloaded += (s, e) =>
                        {
                            if (Win_Chart != null)
                            {
                                Win_Chart.Close();
                            }
                        };
                    }
                    else
                    {
                        Win_Chart.Show();
                    }
                }
                else
                {
                    if (Win_Chart != null)
                    {
                        Win_Chart.Close();
                    }
                }
            };

            buttongo.Click += (s, e) =>
            {
                Win_Chart = new Window();
                Win_Chart.Show();
            };


        }


        private void init_test()
        {
            checkBoxOpenJingYaoPanel.Click += (s, e) =>
            {
                if (checkBoxOpenJingYaoPanel.IsChecked == true)
                {
                    if (JYWinPanel == null)
                    {
                        JYWinPanel = new Window();

                        //JYWinPanel.Title = "JingYao link controller panel";
                        //  置顶在父窗口之上
                        //JYWinPanel.Height = 770;
                        //JYWinPanel.Width = 300;
                        //JYWinPanel.MaxHeight = 750;
                        //JYWinPanel.MaxWidth = 300;

                        var panel = new NumericUpDown();

                        JYWinPanel.Topmost = true;
                        JYWinPanel.Content = panel;
                        JYWinPanel.Closed += (s, e) =>
                        {
                            JYWinPanel = null;
                            checkBoxOpenJingYaoPanel.IsChecked = false;
                        };
                        JYWinPanel.Show();
                        this.Unloaded += (s, e) =>
                        {
                            if (JYWinPanel != null)
                            {
                                JYWinPanel.Close();
                            }
                        };
                    }
                    else
                    {
                        JYWinPanel.Show();

                    }

                }
                else
                {
                    if (JYWinPanel != null)
                    {
                        JYWinPanel.Close();
                    }
                }
            };
        }



        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            numericUpDown.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            numericUpDown.Visibility = Visibility.Collapsed;
        }

    }
}
