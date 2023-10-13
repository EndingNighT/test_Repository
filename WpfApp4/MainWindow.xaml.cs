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
using System.Collections.ObjectModel;

using System.ComponentModel;

namespace WpfApp4
{
    public enum TaskType
    {
        Home,
        Work
    }

     public class Task : INotifyPropertyChanged //using System.ComponentModel;
    {
            private string _description;
            private string _name;
            private int _priority;
            private TaskType _type;

            public Task()
            {
            }

            public Task(string name, string description, int priority, TaskType type)
            {
                _name = name;
                _description = description;
                _priority = priority;
                _type = type;
            }

            public string TaskName
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("TaskName");
                }
            }

            public string Description
            {
                get { return _description; }
                set
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }

            public int Priority
            {
                get { return _priority; }
                set
                {
                    _priority = value;
                    OnPropertyChanged("Priority");
                }
            }

            public TaskType TaskType
            {
                get { return _type; }
                set
                {
                    _type = value;
                    OnPropertyChanged("TaskType");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public override string ToString() => _name;

            protected void OnPropertyChanged(string info)
            {
                var handler = PropertyChanged;
                handler?.Invoke(this, new PropertyChangedEventArgs(info));
            }
        }



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CollectionView _myView;

        public MainWindow()
        {
            InitializeComponent();
        }





        private void AddGrouping(object sender, RoutedEventArgs e)
        {
            _myView = (CollectionView)CollectionViewSource.GetDefaultView(myItemsControl.ItemsSource);
            if (_myView.CanGroup)
            {
                var groupDescription
                    = new PropertyGroupDescription("@Type");
                _myView.GroupDescriptions.Add(groupDescription);
            }
        }

        private void RemoveGrouping(object sender, RoutedEventArgs e)
        {
            _myView = (CollectionView)CollectionViewSource.GetDefaultView(myItemsControl.ItemsSource);
            _myView.GroupDescriptions.Clear();
        }



    }
    //TodoList集合类 两种定义方式
    //using System.Collections.ObjectModel;
    public class TodoList : ObservableCollection<string>
    {
        public TodoList()
        {
            Add("Item 1");
            Add("Item 2");
            Add("Item 3");
        }
    }

    public class Tasks : ObservableCollection<Task>
    {
        public Tasks()
        {
            Add(new Task("Groceries", "Pick up Groceries and Detergent", 2, TaskType.Home));
            Add(new Task("Laundry", "Do my Laundry", 2, TaskType.Home));
            Add(new Task("Email", "Email clients", 1, TaskType.Work));
            Add(new Task("Clean", "Clean my office", 3, TaskType.Work));
            Add(new Task("Dinner", "Get ready for family reunion", 1, TaskType.Home));
            Add(new Task("Proposals", "Review new budget proposals", 2, TaskType.Work));
        }
    }





}
