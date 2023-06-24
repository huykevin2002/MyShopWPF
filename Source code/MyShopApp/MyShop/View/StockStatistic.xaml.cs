using LiveCharts.Wpf;
using LiveCharts;
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
using static System.Windows.Forms.LinkLabel;

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for StockStatistic.xaml
    /// </summary>
    public partial class StockStatistic : UserControl
    {
        private static bool isDay;
        private static bool isWeek = true;
        private static bool isMonth;
        private static bool isYear;

        private static int _currentPage;

        private Model.Book _book;

        public StockStatistic(Model.Book book, int currentPage)
        {
            InitializeComponent();
            _book = book;
            _currentPage = currentPage;
            Loaded += StockStatistic_Loaded;
        }

        private async void StockStatistic_Loaded(object sender, RoutedEventArgs e)
        {
            LineX.Labels = await OrderItemService.WeeklyStockLabels(_book.Id);
            chooseDate.Visibility = Visibility.Hidden;
            List<long> values = await OrderItemService.WeeklyRevenueValues();
            LineSeries.Values = new ChartValues<long>();
            if (values != null)
            {
                values.ForEach(e =>
                {
                    LineSeries.Values.Add(e);
                });
                LineY.MaxValue = values.Max();
                LineY.Separator.Step = values.Min();
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Stock stock = new Stock(_currentPage);
            stockStatisticPanel.Content = stock;
        }

        private async void datePickerFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerFrom.SelectedDate != null && datePickerTo.SelectedDate != null && datePickerFrom.SelectedDate <= datePickerTo.SelectedDate)
            {
                LineX.Labels = await OrderItemService.WithinDateStockLabels(_book.Id, datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                List<long> values = await OrderItemService.WithinDateStockValues(_book.Id, datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                LineSeries.Values = new ChartValues<long>();
                if (values != null)
                {
                    values.ForEach(e =>
                    {
                        LineSeries.Values.Add(e);
                    });
                    LineY.MaxValue = values.Max();
                    LineY.Separator.Step = values.Min();
                }
            }
        }

        private async void datePickerTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerFrom.SelectedDate != null && datePickerTo.SelectedDate != null && datePickerFrom.SelectedDate <= datePickerTo.SelectedDate)
            {
                LineX.Labels = await OrderItemService.WithinDateStockLabels(_book.Id, datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                List<long> values = await OrderItemService.WithinDateStockValues(_book.Id, datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                LineSeries.Values = new ChartValues<long>();
                if (values != null)
                {
                    values.ForEach(e =>
                    {
                        LineSeries.Values.Add(e);
                    });
                    LineY.MaxValue = values.Max();
                    LineY.Separator.Step = values.Min();
                }
            }
        }

        private async void btnDay_Click(object sender, RoutedEventArgs e)
        {
            isDay = true;
            isWeek = false;
            isMonth = false;
            isYear = false;
            chooseDate.Visibility = Visibility.Visible;

            if (datePickerFrom.SelectedDate != null && datePickerTo.SelectedDate != null && datePickerFrom.SelectedDate <= datePickerTo.SelectedDate)
            {
                LineX.Labels = await OrderItemService.WithinDateStockLabels(_book.Id, datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                List<long> values = await OrderItemService.WithinDateStockValues(_book.Id, datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                LineSeries.Values = new ChartValues<long>();
                if (values != null)
                {
                    values.ForEach(e =>
                    {
                        LineSeries.Values.Add(e);
                    });
                    LineY.MaxValue = values.Max();
                    LineY.Separator.Step = values.Min();
                }
            }
        }

        private async void btnWeek_Click(object sender, RoutedEventArgs e)
        {
            isWeek = true;
            isDay = false;
            isMonth = false;
            isYear = false;

            LineX.Labels = await OrderItemService.WeeklyStockLabels(_book.Id);
            chooseDate.Visibility = Visibility.Hidden;
            List<long> values = await OrderItemService.WeeklyStockValues(_book.Id);
            LineSeries.Values = new ChartValues<long>();

            if (values != null)
            {
                values.ForEach(e =>
                {
                    LineSeries.Values.Add(e);
                });
                LineY.MaxValue = values.Max();
                LineY.Separator.Step = values.Min();
            }
        }

        private async void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            isMonth = true;
            isDay = false;
            isWeek = false;
            isYear = false;

            LineX.Labels = await OrderItemService.MonthlyStockLabels(_book.Id);
            chooseDate.Visibility = Visibility.Hidden;
            List<long> values = await OrderItemService.MonthlyStockValues(_book.Id);
            LineSeries.Values = new ChartValues<long>();

            if (values != null)
            {
                values.ForEach(e =>
                {
                    LineSeries.Values.Add(e);
                });
                LineY.MaxValue = values.Max();
                LineY.Separator.Step = values.Min();
            }
        }

        private async void btnYear_Click(object sender, RoutedEventArgs e)
        {
            isYear = true;
            isDay = false;
            isWeek = false;
            isMonth = false;

            LineX.Labels = await OrderItemService.YearlyStockLabels(_book.Id);
            chooseDate.Visibility = Visibility.Hidden;
            List<long> values = await OrderItemService.YearlyStockValues(_book.Id);
           
            LineSeries.Values = new ChartValues<long>();

            if (values != null)
            {
                values.ForEach(e =>
                {
                    LineSeries.Values.Add(e);
                });
                LineY.MaxValue = values.Max();
                LineY.Separator.Step = values.Min();
            }
        }
    }
}
