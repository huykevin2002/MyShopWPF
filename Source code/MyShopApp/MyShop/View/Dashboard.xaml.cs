using MyShop.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private bool isWeek = true;
        public Dashboard()
        {
            InitializeComponent();

            Loaded += Dashboard_Loaded;
        }

        private async void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            LoadScreen();
        }

        private async void LoadScreen()
        {
            long totalBooks = await BookService.countAll();
            long totalOrders = 0;

            ObservableCollection<Model.Book> lowOnStocks = await BookService.QueryLeastQuantity();

            ObservableCollection<Model.Order> latestOrders = null;

            long totalRevenue = 0;

            if (isWeek)
            {
                latestOrders = await OrderService.QueryLatestOrdersInThisWeek();
                totalRevenue = await OrderItemService.totalRevenueInThisWeek();
                totalOrders = await OrderService.CountLatestOrdersInThisWeek();
            }
            else
            {
                latestOrders = await OrderService.QueryLatestOrdersInThisMonth();
                totalRevenue = await OrderItemService.totalRevenueInThisMonth();
                totalOrders = await OrderService.CountLatestOrdersInThisMonth();
            }

            if (lowOnStocks.Count() > 0)
            {
                los1.Title = lowOnStocks[0].Title;
                los1.Desc = $"Quantity: {lowOnStocks[0].Quantity}";
                los1.Visibility = Visibility.Visible;
            }
            if (lowOnStocks.Count() > 1)
            {
                los2.Title = lowOnStocks[1].Title;
                los2.Desc = $"Quantity: {lowOnStocks[1].Quantity}";
                los2.Visibility = Visibility.Visible;
            }
            if (lowOnStocks.Count() > 2)
            {
                los3.Title = lowOnStocks[2].Title;
                los3.Desc = $"Quantity: {lowOnStocks[2].Quantity}";
                los3.Visibility = Visibility.Visible;
            }
            if (lowOnStocks.Count() > 3)
            {
                los4.Title = lowOnStocks[3].Title;
                los4.Desc = $"Quantity: {lowOnStocks[3].Quantity}";
                los4.Visibility = Visibility.Visible;
            }
            if (lowOnStocks.Count() > 4)
            {
                los5.Title = lowOnStocks[4].Title;
                los5.Desc = $"Quantity: {lowOnStocks[4].Quantity}";
                los5.Visibility = Visibility.Visible;
            }


            if (latestOrders.Count() > 0)
            {
                lo1.Title = latestOrders[0].OrderNo;
                lo1.Desc = $"{latestOrders[0].LastModifiedDate}";
                lo1.Visibility = Visibility.Visible;
            }
            if (latestOrders.Count() > 1)
            {
                lo2.Title = latestOrders[1].OrderNo;
                lo2.Desc = $"{latestOrders[1].LastModifiedDate}";
                lo2.Visibility = Visibility.Visible;
            }
            if (latestOrders.Count() > 2)
            {
                lo3.Title = latestOrders[2].OrderNo;
                lo3.Desc = $"{latestOrders[2].LastModifiedDate}";
                lo3.Visibility = Visibility.Visible;
            }
            if (latestOrders.Count() > 3)
            {
                lo4.Title = latestOrders[3].OrderNo;
                lo4.Desc = $"{latestOrders[3].LastModifiedDate}";
                lo4.Visibility = Visibility.Visible;
            }
            if (latestOrders.Count() > 4)
            {
                lo5.Title = latestOrders[4].OrderNo;
                lo5.Desc = $"{latestOrders[4].LastModifiedDate}";
                lo5.Visibility = Visibility.Visible;
            }

            cardTotalBooks.Number = string.Format("{0:n0}", totalBooks);
            cardTotalOrders.Number = string.Format("{0:n0}", totalOrders);
            cardTotalRevenue.Number = string.Format("{0:n0}", totalRevenue);
        }

        private void btnWeek_Click(object sender, RoutedEventArgs e)
        {
            isWeek= true;
            LoadScreen();
        }

        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            isWeek= false;
            LoadScreen();
        }
    }
}
