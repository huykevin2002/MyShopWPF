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
    /// Interaction logic for AddStock.xaml
    /// </summary>
    public partial class AddStock : UserControl
    {
        public ObservableCollection<Model.Classification> classifications;

        private int _currentPage;

        public AddStock(int currentPage)
        {
            InitializeComponent();

            Loaded += AddStock_Loaded;
            _currentPage = currentPage;
        }

        private async void AddStock_Loaded(object sender, RoutedEventArgs e)
        {
            classifications = await ClassificationService.QueryAll();
            cbBoxClassification.ItemsSource = classifications;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Stock stock = new Stock(_currentPage);
            addStockPanel.Content = stock;
        }

        private async void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (
                txtTitle.Text.Equals(String.Empty) ||
                txtAuthor.Text.Equals(String.Empty) ||
                txtYear.Text.Equals(String.Empty) ||
                txtSellingPrice.Text.Equals(String.Empty) ||
                txtPurchasePrice.Text.Equals(String.Empty) ||
                txtQuantity.Text.Equals(String.Empty) ||
                cbBoxClassification.SelectedItem == null
            )
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
                return;
            }

            try 
            {
                int.Parse(txtYear.Text);
                int.Parse(txtSellingPrice.Text);
                int.Parse(txtPurchasePrice.Text);
                int.Parse(txtQuantity.Text);
            }
            catch 
            {
                return;
            }

            int classificationId = classifications[cbBoxClassification.SelectedIndex].Id;

            Object stock = new {
                title = txtTitle.Text,
                author = txtAuthor.Text,
                year = int.Parse(txtYear.Text),
                sellingPrice = int.Parse(txtSellingPrice.Text),
                purchasePrice = int.Parse(txtPurchasePrice.Text),
                quantity = int.Parse(txtQuantity.Text),
                classification = new {
                    id = classificationId
                }
            };
            await BookService.Create(stock);
            Stock stockSceen = new Stock(_currentPage);
            addStockPanel.Content = stockSceen;
        }

        private void txtYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.IsTextAllowed(e.Text);
        }

        private void txtSellingPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.IsTextAllowed(e.Text);
        }

        private void txtPurchasePrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.IsTextAllowed(e.Text);
        }

        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utils.IsTextAllowed(e.Text);
        }
    }
}
