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
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : UserControl
    {
        private static bool isDay;
        private static bool isWeek = true;
        private static bool isMonth;
        private static bool isYear;
        private static bool isRevenue = true;
        private static bool isProfit;

        public Statistic()
        {
            InitializeComponent();

            Loaded += Statistic_Loaded;
        }

        private async void Statistic_Loaded(object sender, RoutedEventArgs e)
        {
            LineX.Labels = await OrderItemService.WeeklyRevenueLabels();
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

        private async void datePickerFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerFrom.SelectedDate != null && datePickerTo.SelectedDate != null && datePickerFrom.SelectedDate <= datePickerTo.SelectedDate)
            {
                LineX.Labels = await OrderItemService.WithinDateRevenueLabels(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                List<long> values = null;
                LineSeries.Values = new ChartValues<long>();

                if (isRevenue)
                {
                    values = await OrderItemService.WithinDateRevenueValues(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                }
                if (isProfit)
                {
                    values = await OrderItemService.WithinDateProfitValues(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                }

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
                LineX.Labels = await OrderItemService.WithinDateRevenueLabels(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                List<long> values = null;
                LineSeries.Values = new ChartValues<long>();

                if (isRevenue)
                {
                    values = await OrderItemService.WithinDateRevenueValues(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                }
                if (isProfit)
                {
                    values = await OrderItemService.WithinDateProfitValues(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                }

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
                LineX.Labels = await OrderItemService.WithinDateRevenueLabels(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                List<long> values = null;
                LineSeries.Values = new ChartValues<long>();

                if (isRevenue)
                {
                    values = await OrderItemService.WithinDateRevenueValues(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                }
                if (isProfit)
                {
                    values = await OrderItemService.WithinDateProfitValues(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                }

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
            LineX.Labels = await OrderItemService.WeeklyRevenueLabels();
            chooseDate.Visibility = Visibility.Hidden;
            List<long> values = null;
            LineSeries.Values = new ChartValues<long>();

            if (isRevenue)
            {
                values = await OrderItemService.WeeklyRevenueValues();
            }
            
            if (isProfit)
            {
                values = await OrderItemService.WeeklyProfitValues();
            }

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
            LineX.Labels = await OrderItemService.MonthlyRevenueLabels();
            chooseDate.Visibility = Visibility.Hidden;
            List<long> values = null;
            LineSeries.Values = new ChartValues<long>();

            if (isRevenue)
            {
                values = await OrderItemService.MonthlyRevenueValues();
            }

            if (isProfit)
            {
                values = await OrderItemService.MonthlyProfitValues();
            }

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
            LineX.Labels = await OrderItemService.YearlyRevenueLabels();
            chooseDate.Visibility = Visibility.Hidden;
            List<long> values = null;
            LineSeries.Values = new ChartValues<long>();

            if (isRevenue)
            {
                values = await OrderItemService.YearlyRevenueValues();
            }

            if (isProfit)
            {
                values = await OrderItemService.YearlyProfitValues();
            }

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

        private async void btnRevenue_Click(object sender, RoutedEventArgs e)
        {
            isRevenue = true;
            isProfit = false;

            if (isDay)
            {
                if (datePickerFrom.SelectedDate != null && datePickerTo.SelectedDate != null && datePickerFrom.SelectedDate <= datePickerTo.SelectedDate)
                {
                    LineX.Labels = await OrderItemService.WithinDateRevenueLabels(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                    List<long> values = null;
                    LineSeries.Values = new ChartValues<long>();
                    values = await OrderItemService.WithinDateRevenueValues(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
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

            if (isWeek)
            {
                LineX.Labels = await OrderItemService.WeeklyRevenueLabels();
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

            if (isMonth)
            {
                LineX.Labels = await OrderItemService.MonthlyRevenueLabels();
                chooseDate.Visibility = Visibility.Hidden;
                List<long> values = await OrderItemService.MonthlyRevenueValues();
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

            if (isYear)
            {
                LineX.Labels = await OrderItemService.YearlyRevenueLabels();
                chooseDate.Visibility = Visibility.Hidden;
                List<long> values = await OrderItemService.YearlyRevenueValues();
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

        private async void btnProfit_Click(object sender, RoutedEventArgs e)
        {
            isProfit = true;
            isRevenue = false;

            if (isDay)
            {
                if (datePickerFrom.SelectedDate != null && datePickerTo.SelectedDate != null && datePickerFrom.SelectedDate <= datePickerTo.SelectedDate)
                {
                    LineX.Labels = await OrderItemService.WithinDateRevenueLabels(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
                    List<long> values = null;
                    LineSeries.Values = new ChartValues<long>();
                    values = await OrderItemService.WithinDateProfitValues(datePickerFrom.SelectedDate.ToString(), datePickerTo.SelectedDate.ToString());
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

            if (isWeek)
            {
                LineX.Labels = await OrderItemService.WeeklyRevenueLabels();
                chooseDate.Visibility = Visibility.Hidden;
                List<long> values = await OrderItemService.WeeklyProfitValues();
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

            if (isMonth)
            {
                LineX.Labels = await OrderItemService.MonthlyRevenueLabels();
                chooseDate.Visibility = Visibility.Hidden;
                List<long> values = await OrderItemService.MonthlyProfitValues();
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

            if (isYear)
            {
                LineX.Labels = await OrderItemService.YearlyRevenueLabels();
                chooseDate.Visibility = Visibility.Hidden;
                List<long> values = await OrderItemService.YearlyProfitValues();
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
}
