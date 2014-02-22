// MainWindow.xaml.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using WebApi2Book.Windows.Legacy.Client.TaskServiceReference;

namespace WebApi2Book.Windows.Legacy.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private Category _category;
        private int? _categoryId;
        private bool _isProcessing;
        private bool _useWebApi;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            GetCategoriesCommand = new DelegateCommand(GetCategories, AllowFetch);
            GetCategoryCommand = new DelegateCommand(GetCategory, AllowIndividialCategoryFetch);

            Categories = new ObservableCollection<Category>();
        }

        public ObservableCollection<Category> Categories { get; private set; }

        public DelegateCommand GetCategoriesCommand { get; private set; }
        public DelegateCommand GetCategoryCommand { get; private set; }

        public Category Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }

        public int? CategoryId
        {
            get { return _categoryId; }
            set
            {
                _categoryId = value;
                OnPropertyChanged("CategoryId");
                GetCategoryCommand.RaiseCanExecuteChanged();
            }
        }

        public bool UseWebApi
        {
            get { return _useWebApi; }
            set
            {
                _useWebApi = value;
                OnPropertyChanged("UseWebApi");
            }
        }

        public bool IsProcessing
        {
            get { return _isProcessing; }
            set
            {
                _isProcessing = value;
                OnPropertyChanged("IsProcessing");
                GetCategoriesCommand.RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private bool AllowIndividialCategoryFetch()
        {
            return AllowFetch() && CategoryId.HasValue;
        }

        private bool AllowFetch()
        {
            return !IsProcessing;
        }

        public async void GetCategories()
        {
            IsProcessing = true;

            try
            {
                Categories.Clear();
                var result = await Task.Run(() => GetCategoriesAsync());
                result.Body.GetCategoriesResult.ToList().ForEach(x => Categories.Add(x));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "System Message");
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private async void GetCategory()
        {
            IsProcessing = true;

            try
            {
                Category = null;
                var result = await Task.Run(() => GetCategoryAsync());
                Category = result.Body.GetCategoryByIdResult;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "System Message");
            }
            finally
            {
                IsProcessing = false;
            }
        }

        public TeamTaskServiceSoapClient GetServiceClient()
        {
            var taskServiceSoapClient = UseWebApi
                ? new TeamTaskServiceSoapClient("TeamTaskServiceViaRest")
                : new TeamTaskServiceSoapClient("TeamTaskServiceSoap");
            return taskServiceSoapClient;
        }

        public async Task<GetCategoriesResponse> GetCategoriesAsync()
        {
            var taskServiceSoapClient = GetServiceClient();
            taskServiceSoapClient.Open();

            try
            {
                var result = await taskServiceSoapClient.GetCategoriesAsync();
                return result;
            }
            finally
            {
                taskServiceSoapClient.Close();
            }
        }

        public async Task<GetCategoryByIdResponse> GetCategoryAsync()
        {
            var taskServiceSoapClient = GetServiceClient();
            taskServiceSoapClient.Open();

            try
            {
                var result = await taskServiceSoapClient.GetCategoryByIdAsync(CategoryId.Value);
                return result;
            }
            finally
            {
                taskServiceSoapClient.Close();
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}