using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {        
        public Settings()
        {
            InitializeComponent();

            Loaded += Orders_Loaded;
        }

        private void Orders_Loaded(object sender, RoutedEventArgs e)
        {
            int itemsPerPage = Model.Setting.GetItemsPerPage();
            if (itemsPerPage == 5)
            {
                ipp5.IsSelected= true;
            }
            else if (itemsPerPage == 10) 
            {
                ipp10.IsSelected= true;
            }
            else if (itemsPerPage == 15)
            {
                ipp15.IsSelected = true;
            }
            else if (itemsPerPage == 20)
            {
                ipp20.IsSelected = true;
            }
        }

        private void cbBoxPriceRange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = cbBoxPriceRange.SelectedIndex;
            if (i == 0)
            {
                Model.Setting.SetItemsPerPage(5);
                ipp5.IsSelected = true;
            }
            else if (i == 1)
            {
                Model.Setting.SetItemsPerPage(10);
                ipp10.IsSelected = true;
            }
            else if (i == 2)
            {
                Model.Setting.SetItemsPerPage(15);
                ipp15.IsSelected = true;
            }
            else if (i == 3)
            {
                Model.Setting.SetItemsPerPage(20);
                ipp20.IsSelected = true;
            }

            var config = ConfigurationManager.OpenExeConfiguration(
                               ConfigurationUserLevel.None);
            config.AppSettings.Settings["ItemsPerPage"].Value = $"{Model.Setting.GetItemsPerPage()}";

            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
