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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Threading;

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for Tags.xaml
    /// </summary>
    public partial class Classification : UserControl
    {
        public ObservableCollection<Model.Classification> classifications;

        private int _currentPage = 0;

        private static int totalPage;

        public Classification()
        {
            InitializeComponent();

            Loaded += Classification_Loaded;
        }
        public Classification(int currentPage)
        {
            InitializeComponent();

            _currentPage = currentPage;

            Loaded += Classification_Loaded;
        }

        private async void Classification_Loaded(object sender, RoutedEventArgs e)
        {
            LoadScreen();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddClassification addClassification = new AddClassification(_currentPage);
            classificationPanel.Content = addClassification;
        }

        private async void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = classificationDataGrid.SelectedIndex;
            int index = classifications[i].Id;
            bool status = await ClassificationService.Delete(index);

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

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = classificationDataGrid.SelectedIndex;
            EditClassification editClassification = new EditClassification(classifications[i], _currentPage);
            classificationPanel.Content = editClassification;
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
            classifications = await ClassificationService.Query(_currentPage);
            classificationDataGrid.ItemsSource = classifications;
            long totalItems = await ClassificationService.countAll();
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
    }
}
