using MyShop.Model;
using MyShop.Service;
using MyShop.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EditOrderItem.xaml
    /// </summary>
    public partial class EditOrderItem : UserControl
    {
        public ObservableCollection<Model.OrderItem> orderItems;

        private static Model.Order _order;

        private Model.OrderItem _orderItem;

        private int _oldQuantity;

        private int _currentPage;

        public EditOrderItem(Model.Order order, Model.OrderItem orderItem, int currentPage)
        {
            InitializeComponent();

            _order = order;
            _orderItem = orderItem;

            EditOrderItemForm.DataContext = _orderItem;

            _oldQuantity = orderItem.Quantity;


            Loaded += EditOrderItem_Loaded;
            _currentPage = currentPage;
        }

        private void EditOrderItem_Loaded(object sender, RoutedEventArgs e)
        {
            txtDesc.Text = $"{_orderItem.Book.Title} - {_orderItem.Book.Author} - {_orderItem.Book.Year}";
            txtStockQuantity.Text = $"{_orderItem.Book.Quantity}";
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            OrderItem orderItem = new OrderItem(_order, _currentPage);
            editOrderItemPanel.Content = orderItem;
        }

        private async void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (
                txtQuantity.Text.Equals(String.Empty)
            )
            {
                txtWarning.Text = "Empty fields are now allowed!";
                txtWarning.Visibility = Visibility.Visible;

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(5);
                timer.Tick += (sender, args) =>
                {
                    txtWarning.Visibility = Visibility.Hidden;
                    timer.Stop();
                };
                timer.Start();
                return;
            }

            try
            {
                if (int.Parse(txtQuantity.Text) > int.Parse(txtStockQuantity.Text) + _oldQuantity)
                {
                    txtWarning.Text = "Quantity must not be greater than Stock Quantity!";
                    txtWarning.Visibility = Visibility.Visible;

                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(5);
                    timer.Tick += (sender, args) =>
                    {
                        txtWarning.Visibility = Visibility.Hidden;
                        timer.Stop();
                    };
                    timer.Start();
                    return;
                }
            }
            catch
            {
                txtWarning.Text = $"Invalid input {txtQuantity.Text}, {txtStockQuantity.Text}";
                txtWarning.Visibility = Visibility.Visible;

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(5);
                timer.Tick += (sender, args) =>
                {
                    txtWarning.Visibility = Visibility.Hidden;
                    timer.Stop();
                };
                timer.Start();
                return;
            }

            Object orderItem = new
            {
                id = _orderItem.Id,
                quantity = int.Parse(txtQuantity.Text),
                book = new
                {
                    id = _orderItem.Book.Id
                }
            };
            await OrderItemService.Update(_orderItem.Id, orderItem);
            OrderItem orderItemScreen = new OrderItem(_order, _currentPage);
            editOrderItemPanel.Content = orderItemScreen;
            await OrderService.Update(_order.Id, new { id = _order.Id });
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.IsTextAllowed(e.Text);
        }
    }
}
