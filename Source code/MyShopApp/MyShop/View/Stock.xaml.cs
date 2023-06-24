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
using MyShop.Model;
using MyShop.Service;
using MyShop.ViewModel;
using static System.Reflection.Metadata.BlobBuilder;

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for Stock.xaml
    /// </summary>
    public partial class Stock : UserControl
    {
        private static ObservableCollection<Book> books;

        public ObservableCollection<Model.Classification> classifications;

        private int _currentPage = 0;

        private static int totalPage;

        public Stock()
        {
            InitializeComponent();

            Loaded += Stock_Loaded;
        }

        public Stock(int currentPage)
        {
            InitializeComponent();
            _currentPage = currentPage;
            Loaded += Stock_Loaded;
        }

        private async void Stock_Loaded(object sender, RoutedEventArgs e)
        {
            classifications = await ClassificationService.QueryAll();
            classifications.Insert(0, new Model.Classification
            {
                Id = 0,
                Name = "All Classifications"
            });
            cbBoxClassification.ItemsSource = classifications;
            cbBoxClassification.Tag = classifications.First();
            cbBoxClassification.SelectedItem = classifications.First();
            if (txtSearch.Text.Equals(string.Empty))
            {
                LoadScreen();
            }
            else
            {
                LoadScreenSearch(0);
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddStock addStock = new AddStock(_currentPage);
            stockPanel.Content = addStock;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = stockDataGrid.SelectedIndex;
            EditStock editStock = new EditStock(books[i], _currentPage);
            stockPanel.Content = editStock;
        }

        private async void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = stockDataGrid.SelectedIndex;
            int index = books[i].Id;
            await BookService.Delete(index);
            if (txtSearch.Text.Equals(string.Empty))
            {
                LoadScreen();
            }
            else
            {
                LoadScreenSearch(_currentPage);
            }
        }

        private async void LoadScreen()
        {
            btnNext.IsEnabled = true;
            btnPrev.IsEnabled = true;
            books = await BookService.Query(_currentPage);
            stockDataGrid.ItemsSource = books;
            long totalItems = await BookService.countAll();
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

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 0)
            {
                _currentPage--;
            }
            if (txtSearch.Text.Equals(string.Empty) && (cbBoxClassification.SelectedValue == null || cbBoxClassification.SelectedIndex == 0) && cbBoxPriceRange.SelectedIndex == 0)
            {
                LoadScreen();
            }
            else
            {
                LoadScreenSearch(_currentPage);
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < totalPage - 1)
            {
                _currentPage++;
            }
            if (txtSearch.Text.Equals(string.Empty) && (cbBoxClassification.SelectedValue == null || cbBoxClassification.SelectedIndex == 0) && cbBoxPriceRange.SelectedIndex == 0)
            {
                LoadScreen();
            }
            else
            {
                LoadScreenSearch(_currentPage);
            }
        }

        private async void LoadScreenSearch(int page)
        {
            if (btnNext != null)
            {
                btnNext.IsEnabled = true;

            }

            if (btnPrev != null)
            {
                btnPrev.IsEnabled = true;

            }

            string minPrice = "";
            string maxPrice = "";

            string classId = "";
            int classificationId = cbBoxClassification.SelectedIndex;

            if (classificationId > 0)
            {
                classId = $"{classifications[classificationId].Id}";
            }

            int priceRangeId = cbBoxPriceRange.SelectedIndex;

            if (priceRangeId == 1)
            {
                maxPrice = "10";
            }
            else if (priceRangeId == 2)
            {
                minPrice= "10";
                maxPrice= "20";
            }
            else if (priceRangeId == 3)
            {
                minPrice = "20";
                maxPrice = "30";
            }
            else if (priceRangeId == 4)
            {
                minPrice = "30";
                maxPrice = "40";
            }
            else if (priceRangeId == 5)
            {
                minPrice = "40";
                maxPrice = "50";
            }
            else if (priceRangeId == 6)
            {
                minPrice = "50";
            }


            books = await BookService.Search(page, txtSearch.Text, classId, minPrice, maxPrice);
            stockDataGrid.ItemsSource = books;

            long totalItems = await BookService.countAll();
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

        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadScreenSearch(0);
            }
        }

        private void cbBoxClassification_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadScreenSearch(0);
        }

        private void cbBoxPriceRange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadScreenSearch(0);
        }

        private void Detail_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = stockDataGrid.SelectedIndex;
            StockStatistic stockStatistic = new StockStatistic(books[i], _currentPage);
            stockPanel.Content = stockStatistic;
        }
    }
}
