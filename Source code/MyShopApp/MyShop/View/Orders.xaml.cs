using MyShop.Model;
using MyShop.Service;
using MyShop.Utilities;
using MyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
using System.Windows.Threading;

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        public ObservableCollection<Model.Order> orders;

        private int _currentPage = 0;

        private static int totalPage;

        public Orders()
        {
            InitializeComponent();

            Loaded += Orders_Loaded;
        }

        public Orders(int currentPage)
        {
            InitializeComponent();
            _currentPage= currentPage;
            Loaded += Orders_Loaded;
        }

        private async void Orders_Loaded(object sender, RoutedEventArgs e)
        {
            LoadScreen();
        }

        private void Detail_Item_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = orderDataGrid.SelectedIndex;
            OrderItem orderItem = new OrderItem(orders[i], 0, _currentPage);
            orderPanel.Content= orderItem;
        }

        private async void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = orderDataGrid.SelectedIndex;
            int index = orders[i].Id;
            bool status = await OrderService.Delete(index);

            if (status)
            {
                LoadScreen();
            }
            else
            {
                txtWarning.Visibility = Visibility.Visible;

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(5);
                timer.Tick += (sender, args) =>
                {
                    txtWarning.Visibility = Visibility.Hidden;
                    timer.Stop();
                };
                timer.Start();

            }
        }

        private async void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Model.Order newOrder = await OrderService.Create(new { });
            OrderItem orderItem = new OrderItem(newOrder, 0, _currentPage);
            orderPanel.Content= orderItem;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 0)
            {
                _currentPage--;
            }
            LoadScreen();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < totalPage - 1)
            {
                _currentPage++;
            }
            LoadScreen();

        }

        private async void LoadScreen()
        {
            btnNext.IsEnabled = true;
            btnPrev.IsEnabled = true;
            orders = await OrderService.Query(_currentPage);
            orderDataGrid.ItemsSource = orders;
            long totalItems = await OrderService.countAll();
            totalPage = (int)(totalItems / Setting.GetItemsPerPage());
            if (totalItems % Setting.GetItemsPerPage() != 0)
            {
                totalPage++;
            }
            if (_currentPage == totalPage - 1)
            {
                btnNext.IsEnabled = false;
            }
            if (_currentPage == 0)
            {
                btnPrev.IsEnabled = false;
            }
        }

        private async void LoadScreenSearch()
        {
            btnNext.IsEnabled = true;
            btnPrev.IsEnabled = true;
            orders = await OrderService.QueryWithinDate(_currentPage, datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
            orderDataGrid.ItemsSource = orders;
            long totalItems = await OrderService.countAll();
            totalPage = (int)(totalItems / Setting.GetItemsPerPage());
            if (totalItems % Setting.GetItemsPerPage() != 0)
            {
                totalPage++;
            }
            if (_currentPage == totalPage - 1)
            {
                btnNext.IsEnabled = false;
            }
            if (_currentPage == 0)
            {
                btnPrev.IsEnabled = false;
            }
        }

        private void datePickerFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerFrom.SelectedDate != null && datePickerTo.SelectedDate != null && datePickerFrom.SelectedDate <= datePickerTo.SelectedDate) {
                LoadScreenSearch();
            }
        }

        private void datePickerTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerFrom.SelectedDate != null && datePickerTo.SelectedDate != null && datePickerFrom.SelectedDate <= datePickerTo.SelectedDate)
            {
                LoadScreenSearch();
            }
        }
    }
}
