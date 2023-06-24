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
    /// Interaction logic for EditClassification.xaml
    /// </summary>
    public partial class EditClassification : UserControl
    {
        private static Model.Classification _classification;

        private int _currentPage;
        public EditClassification(Model.Classification classification, int currentPage)
        {
            InitializeComponent();

            _classification = classification;
            txtId.Text = $"{classification.Id}";
            txtClassificationName.Text = classification.Name;
            _currentPage = currentPage;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Classification classification = new Classification(_currentPage);
            editClassificationPanel.Content = classification;
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
            Object classification = new { 
                id = _classification.Id,
                name = txtClassificationName.Text
            };
            await ClassificationService.Update(_classification.Id, classification);
            Classification classificationScreen = new Classification(_currentPage);
            editClassificationPanel.Content = classificationScreen;
        }
    }
}
