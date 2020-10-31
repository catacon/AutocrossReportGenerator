using autocrossreportgenerator.Utilities;
using autocrossreportgenerator.ViewModels;
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

namespace autocrossreportgenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        // Active view model
        public ViewModelBase CurrentViewModel { get; set; }

        // Make view models members so state can persist between view changes
        private ViewModelBase _homeViewModel;

        // Settings object for managing program state
        public Settings _appSettings = new Settings();

        // Logging object
        public NLog.Logger _appLog;

        public MainWindow()
        {
            InitializeComponent();

            // TODO handle settings initialization better
            if (!_appSettings.LoadFromFile(Settings.SettingsPath + Settings.SettingsFile))
            {
                MessageBox.Show("Unable to load settings file. Creating default.");

                _appSettings.CreateDefaultFile();
            }

            if (_appSettings.CurrentDatabase == string.Empty)
            {

            }

            // Initialize view models
            _homeViewModel = new HomeViewModel("Home");

            // Set initial view model
            CurrentViewModel = _homeViewModel;
            CurrentViewModel.Update();

            // Subscribe to PropertyChanged event for all view models
            _homeViewModel.PropertyChanged += _viewModel_PropertyChanged;

            // Setup log file
            _appLog = NLog.LogManager.GetLogger(GetType().Name);

            // Set data context to this class
            DataContext = this;
        }

        /// <summary>
        /// Handler for current view model's PropertyChanged event. This is mainly used for navigating to a new view
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // If the view model has requested a view change, handle it
            if (e.PropertyName == "nextViewModel")
            {
                switch (CurrentViewModel._nextViewModel)
                {
                    case "Home":
                        CurrentViewModel = _homeViewModel;
                        break;
                    default:
                        // Do nothing
                        break;
                }

                CurrentViewModel.Update();

                // Tell the view to update
                OnPropertyChanged("CurrentViewModel");
            }
        }

        // PropertyChanged event for INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged for implementation of INotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName">Name of property that has changed</param>
        public void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
