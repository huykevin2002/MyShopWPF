using MyShop.Model;
using MyShop.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for OrderItem.xaml
    /// </summary>
    public partial class OrderItem : UserControl
    {
        private static ObservableCollection<Model.OrderItem> _orderItems;

        private static Model.Order _order;

        private int _currentPage = 0;

        private static int _parentCurrentPage;

        private static int totalPage;

        private static long totalPrice;

        public OrderItem(int currentPage, int parentCurrentPage)
        {
            InitializeComponent();
            _currentPage = currentPage;
            _parentCurrentPage = parentCurrentPage;
        }
        public OrderItem(Model.Order order, int currentPage, int parentCurrentPage)
        {
            InitializeComponent();
            _order = order;
            _currentPage= currentPage;
            _parentCurrentPage = parentCurrentPage;
            Loaded += OrderItem_Loaded;
            orderNo.Text = order.OrderNo;
        }

        public OrderItem(Model.Order order, int currentPage)
        {
            InitializeComponent();
            _order = order;
            _currentPage = currentPage;
            Loaded += OrderItem_Loaded;
            orderNo.Text = order.OrderNo;
        }

        private async void OrderItem_Loaded(object sender, RoutedEventArgs e)
        {
            LoadScreen();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Orders order = new Orders(_parentCurrentPage);
            orderItemPanel.Content = order;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddOrderItem addOrderItem = new AddOrderItem(_order, _currentPage);
            orderItemPanel.Content = addOrderItem;
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
            totalPrice = await OrderItemService.totalPriceByOrderId(_order.Id);
            _orderItems = await OrderItemService.QueryByOrderId(_order.Id, _currentPage);
            orderItemDataGrid.ItemsSource = _orderItems;
            txtTotalPrice.Text = string.Format("{0:n0}", totalPrice);
            long totalItems = await OrderItemService.countAllByOrderId(_order.Id);
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

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = orderItemDataGrid.SelectedIndex;
            EditOrderItem editOrderItem = new EditOrderItem(_order, _orderItems[i], _currentPage);
            orderItemPanel.Content = editOrderItem;
        }

        private async void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = orderItemDataGrid.SelectedIndex;
            int index = _orderItems[i].Id;
            await OrderItemService.Delete(index);
            LoadScreen();
        }
    }
}
