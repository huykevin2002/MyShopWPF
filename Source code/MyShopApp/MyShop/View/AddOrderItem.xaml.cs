using MyShop.Model;
using MyShop.Service;
using MyShop.Utilities;
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
using System.Windows.Threading;

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for AddOrderItem.xaml
    /// </summary>
    public partial class AddOrderItem : UserControl
    {
        public ObservableCollection<Model.OrderItem> orderItems;

        private static Model.Order _order;

        private ObservableCollection<Model.Book> books;

        private int _currentPage;

        public AddOrderItem(Model.Order order, int currentPage)
        {
            InitializeComponent();

            _order = order;
            txtScreenTitle.Text = $"{order.OrderNo} - Add Order Item";

            Loaded += AddOrderItem_Loaded;
            _currentPage = currentPage;
        }

        private async void AddOrderItem_Loaded(object sender, RoutedEventArgs e)
        {
            books = await BookService.QueryAll();
            ObservableCollection<Model.BookDTO> listBookDTO = new ObservableCollection<Model.BookDTO>();
            foreach(Model.Book book in books)
            {
                listBookDTO.Add(new Model.BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Year = book.Year,
                    Classification = book.Classification,
                    SellingPrice = book.SellingPrice,
                    PurchasePrice = book.PurchasePrice,
                    Quantity = book.Quantity,
                    Description = $"{book.Title} - {book.Author} - {book.Year}"
                });
            }
            cbBoxBook.ItemsSource = listBookDTO;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            OrderItem orderItem = new OrderItem(_order, _currentPage);
            addOrderItemPanel.Content = orderItem;
        }

        private async void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (
                txtPrice.Text.Equals(String.Empty) ||
                txtQuantity.Text.Equals(String.Empty) ||
                cbBoxBook.SelectedItem == null
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
                int.Parse(txtPrice.Text);
               
                if (int.Parse(txtQuantity.Text) > int.Parse(txtStockQuantity.Text))
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
                return;
            }

            int bookId = books[cbBoxBook.SelectedIndex].Id;

            Object orderItem = new
            {
                price = int.Parse(txtPrice.Text),
                quantity = int.Parse(txtQuantity.Text),
                book = new
                {
                    id = bookId
                },
                order = new
                {
                    id = _order.Id
                }
            };
            await OrderItemService.Create(orderItem);
            OrderItem orderItemScreen = new OrderItem(_order, _currentPage);
            addOrderItemPanel.Content = orderItemScreen;
            await OrderService.Update(_order.Id, new { id = _order.Id });
        }

        private void cbBoxBook_Selected(object sender, RoutedEventArgs e)
        {
            int i = cbBoxBook.SelectedIndex;
            txtPrice.Text = books[i].SellingPrice.ToString();
            txtStockQuantity.Text = books[i].Quantity.ToString();
        }

        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.IsTextAllowed(e.Text);
        }
    }
}
