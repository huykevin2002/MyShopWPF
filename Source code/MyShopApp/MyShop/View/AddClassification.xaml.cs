using MyShop.Service;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddClassification.xaml
    /// </summary>
    public partial class AddClassification : UserControl
    {
        private int _currentPage;
        public AddClassification(int currentPage)
        {
            InitializeComponent();
            _currentPage = currentPage;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Classification classification = new Classification(_currentPage);
            addClassificationPanel.Content = classification;
        }

        private async void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtClassificationName.Text.Equals(String.Empty))
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
            Object classification = new { name = txtClassificationName.Text };
            await ClassificationService.Create(classification);
            Classification classificationScreen = new Classification(_currentPage);
            addClassificationPanel.Content = classificationScreen;
        }
    }
}
