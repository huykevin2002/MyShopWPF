using MyShop.Utilities;
using MyShop.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object? _currentView;
        public object? CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand? DashboardCommand { get; set; }
        public ICommand? ClassificationCommand { get; set; }
        public ICommand? StockCommand { get; set; }
        public ICommand? OrdersCommand { get; set; }
        public ICommand? StatisticCommand { get; set; }
        public ICommand? SettingsCommand { get; set; }
        public ICommand? OrderItemCommand { get; set; }


        private void Dashboard(object obj) {
            CurrentView = new DashboardVM();
            var config = ConfigurationManager.OpenExeConfiguration(
            ConfigurationUserLevel.None);
            config.AppSettings.Settings["StartScreen"].Value = "Dashboard";
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void Classification(object obj)
        {
            CurrentView = new ClassificationVM();
            var config = ConfigurationManager.OpenExeConfiguration(
            ConfigurationUserLevel.None);
            config.AppSettings.Settings["StartScreen"].Value = "Classification";
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void Stock(object obj)
        {
            CurrentView = new StockVM();
            var config = ConfigurationManager.OpenExeConfiguration(
            ConfigurationUserLevel.None);
            config.AppSettings.Settings["StartScreen"].Value = "Stock";
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void Orders(object obj)
        {
            CurrentView = new OrdersVM();
            var config = ConfigurationManager.OpenExeConfiguration(
            ConfigurationUserLevel.None);
            config.AppSettings.Settings["StartScreen"].Value = "Orders";
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void Statistic(object obj) {
            CurrentView = new StatisticVM();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["StartScreen"].Value = "Statistic";
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void Settings(object obj) {
            CurrentView = new SettingsVM();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["StartScreen"].Value = "Settings";
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void OrderItem(object obj) => CurrentView = new OrderItemVM();

        public NavigationVM()
        {
            DashboardCommand = new RelayCommand(Dashboard);
            ClassificationCommand = new RelayCommand(Classification);
            StockCommand = new RelayCommand(Stock);
            OrdersCommand = new RelayCommand(Orders);
            StatisticCommand = new RelayCommand(Statistic);
            SettingsCommand = new RelayCommand(Settings);
            OrderItemCommand = new RelayCommand(OrderItem);

            // Startup Page

            string startScreen = ConfigurationManager.AppSettings["StartScreen"]!;
            if (!startScreen.Equals(string.Empty))
            {
                if (startScreen.Equals("Dashboard"))
                {
                    CurrentView = new DashboardVM();
                }
                else if (startScreen.Equals("Classification"))
                {
                    CurrentView = new ClassificationVM();
                }
                else if (startScreen.Equals("Stock"))
                {
                    CurrentView = new StockVM();
                }
                else if (startScreen.Equals("Orders"))
                {
                    CurrentView = new OrdersVM();
                }
                else if (startScreen.Equals("Statistic"))
                {
                    CurrentView = new StatisticVM();
                }
                else if (startScreen.Equals("Settings"))
                {
                    CurrentView = new SettingsVM();
                }
            }
            else
            {
                CurrentView = new DashboardVM();
            }
        }
    }
}
