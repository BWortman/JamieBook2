// MainWindow.xaml.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using WebApi2Book.Windows.Legacy.Client.TaskServiceReference;

namespace WebApi2Book.Windows.Legacy.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private readonly TeamTaskServiceSoapClient _taskServiceSoapClient;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            GetCategoriesCommand = new DelegateCommand(GetCategories);
            Categories = new ObservableCollection<Category>();

            _taskServiceSoapClient = new TeamTaskServiceSoapClient();
            _taskServiceSoapClient.Open();
        }

        public ObservableCollection<Category> Categories { get; private set; }

        public DelegateCommand GetCategoriesCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void GetCategories()
        {
            Categories.Clear();
            var result = _taskServiceSoapClient.GetCategories();
            result.ToList().ForEach(x => Categories.Add(x));
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}