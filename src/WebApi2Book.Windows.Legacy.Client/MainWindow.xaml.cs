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
        private bool _isProcessing;
        private Status _status;
        private int? _statusId;
        private bool _useWebApi;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            GetStatusesCommand = new DelegateCommand(GetStatuses, AllowFetch);
            GetStatusCommand = new DelegateCommand(GetStatus, AllowIndividialStatusFetch);

            Statuses = new ObservableCollection<Status>();
        }

        public ObservableCollection<Status> Statuses { get; private set; }

        public DelegateCommand GetStatusesCommand { get; private set; }
        public DelegateCommand GetStatusCommand { get; private set; }

        public Status Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public int? StatusId
        {
            get { return _statusId; }
            set
            {
                _statusId = value;
                OnPropertyChanged("StatusId");
                GetStatusCommand.RaiseCanExecuteChanged();
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

                GetStatusCommand.RaiseCanExecuteChanged();
                GetStatusesCommand.RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private bool AllowIndividialStatusFetch()
        {
            return AllowFetch() && StatusId.HasValue;
        }

        private bool AllowFetch()
        {
            return !IsProcessing;
        }

        public async void GetStatuses()
        {
            IsProcessing = true;

            try
            {
                Statuses.Clear();
                var result = await Task.Run(() => GetStatusesAsync());
                result.Body.GetStatusesResult.ToList().ForEach(x => Statuses.Add(x));
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

        private async void GetStatus()
        {
            IsProcessing = true;

            try
            {
                Status = null;
                var result = await Task.Run(() => GetStatusAsync());
                Status = result.Body.GetStatusByIdResult;
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

        public async Task<GetStatusesResponse> GetStatusesAsync()
        {
            var taskServiceSoapClient = GetServiceClient();
            taskServiceSoapClient.Open();

            try
            {
                var result = await taskServiceSoapClient.GetStatusesAsync();
                return result;
            }
            finally
            {
                taskServiceSoapClient.Close();
            }
        }

        public async Task<GetStatusByIdResponse> GetStatusAsync()
        {
            var taskServiceSoapClient = GetServiceClient();
            taskServiceSoapClient.Open();

            try
            {
                var result = await taskServiceSoapClient.GetStatusByIdAsync(StatusId.Value);
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