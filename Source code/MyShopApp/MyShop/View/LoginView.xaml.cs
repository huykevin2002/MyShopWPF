using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using Microsoft.VisualBasic.FileIO;
using MyShop.Service;

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private bool _isFirstTime;
        public LoginView()
        {
            InitializeComponent();

            Loaded += LoginView_Loaded;
        }

        private async void LoginView_Loaded(object sender, RoutedEventArgs e)
        {
            string username = ConfigurationManager.AppSettings["Username"]!;
            string passwordIn64 = ConfigurationManager.AppSettings["Password"]!;
            string entropyIn64 = ConfigurationManager.AppSettings["Entropy"]!;
            string isChecked = ConfigurationManager.AppSettings["IsChecked"]!;
            string itemsPerPage = ConfigurationManager.AppSettings["ItemsPerPage"]!;
            string isFirstTime = ConfigurationManager.AppSettings["IsFirstTime"]!;

            if (isFirstTime.Equals("true"))
            {
                _isFirstTime = true;
            }

            if (!itemsPerPage.Equals(string.Empty))
            {
                Model.Setting.SetItemsPerPage(int.Parse(itemsPerPage));
            }

            if (isChecked.Equals("true"))
            {
                rememberMe.IsChecked = true;
            }

            if (passwordIn64.Length != 0)
            {
                byte[] entropyInBytes = Convert.FromBase64String(entropyIn64);
                byte[] cypherTextInBytes = Convert.FromBase64String(passwordIn64);

                byte[] passwordInBytes = ProtectedData.Unprotect(cypherTextInBytes,
                    entropyInBytes,
                    DataProtectionScope.CurrentUser
                );

                string password = Encoding.UTF8.GetString(passwordInBytes);

                txtUser.Text = username;
                txtPassword.Password = password;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton== MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (rememberMe.IsChecked == true)
            {
                var config = ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);
                config.AppSettings.Settings["Username"].Value = txtUser.Text;
                config.AppSettings.Settings["IsChecked"].Value = "true";

                var passwordInBytes = Encoding.UTF8.GetBytes(txtPassword.Password);
                var entropy = new byte[20];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(entropy);
                }

                var cypherText = ProtectedData.Protect(
                    passwordInBytes,
                    entropy,
                    DataProtectionScope.CurrentUser
                );

                var passwordIn64 = Convert.ToBase64String(cypherText);
                var entropyIn64 = Convert.ToBase64String(entropy);
                config.AppSettings.Settings["Password"].Value = passwordIn64;
                config.AppSettings.Settings["Entropy"].Value = entropyIn64;


                config.Save(ConfigurationSaveMode.Full);
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                var config = ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);
                config.AppSettings.Settings["Username"].Value = "";
                config.AppSettings.Settings["IsChecked"].Value = "";
                config.AppSettings.Settings["Password"].Value = "";
                config.AppSettings.Settings["Entropy"].Value = "";

                config.Save(ConfigurationSaveMode.Full);
                ConfigurationManager.RefreshSection("appSettings");
            }
            if (await LoginService.Login(txtUser.Text, txtPassword.Password))
            {
                if (_isFirstTime) 
                {
                    List<Object> classifications = new List<Object>();
                    List<Object> books = new List<Object>();
                    List<Object> orders = new List<Object>();
                    List<Object> orderItems = new List<Object>();

                    string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                    string resourcePath = "Data/classification.csv"; 
                    Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/{resourcePath}");
                    StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);
                    using (TextFieldParser parser = new TextFieldParser(resourceInfo.Stream))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");

                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();

                            // Process the fields
                            foreach (string field in fields)
                            {
                                classifications.Add(new { name = field });
                            }
                        }
                    }
                    await ClassificationService.CreateMultiple(classifications);

                    resourcePath = "Data/book.csv";
                    uri = new Uri($"pack://application:,,,/{assemblyName};component/{resourcePath}");
                    resourceInfo = Application.GetResourceStream(uri);
                    using (TextFieldParser parser = new TextFieldParser(resourceInfo.Stream))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(";");

                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();

                            books.Add(new { 
                                title = fields[0],
                                author = fields[1],
                                year = int.Parse(fields[2]),
                                sellingPrice = int.Parse(fields[3]),
                                purchasePrice = int.Parse(fields[4]),
                                quantity = int.Parse(fields[5]),
                                classification = new
                                {
                                    id = int.Parse(fields[6])
                                }
                            });
                        }
                    }
                    await BookService.CreateMultiple(books);

                    resourcePath = "Data/order.csv";
                    uri = new Uri($"pack://application:,,,/{assemblyName};component/{resourcePath}");
                    resourceInfo = Application.GetResourceStream(uri);
                    using (TextFieldParser parser = new TextFieldParser(resourceInfo.Stream))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(";");

                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();

                            orders.Add(new
                            {
                                orderNo = fields[0],
                                createdDate = fields[1],
                                lastModifiedDate = fields[2],
                            });
                        }
                    }
                    await OrderService.CreateMultiple(orders);

                    resourcePath = "Data/order-item.csv";
                    uri = new Uri($"pack://application:,,,/{assemblyName};component/{resourcePath}");
                    resourceInfo = Application.GetResourceStream(uri);
                    using (TextFieldParser parser = new TextFieldParser(resourceInfo.Stream))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(";");

                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();

                            orderItems.Add(new
                            {
                                price = int.Parse(fields[0]),
                                quantity = int.Parse(fields[1]),
                                book = new
                                {
                                    id = int.Parse(fields[2])
                                },
                                order = new
                                {
                                    id = int.Parse(fields[3])
                                }
                            });
                        }
                    }
                    await OrderItemService.CreateMultiple(orderItems);
                }

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide();

                var config = ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);
                config.AppSettings.Settings["IsFirstTime"].Value = "false";

                config.Save(ConfigurationSaveMode.Full);
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                txtWarning.Text = "Wrong username or password. Try again!";
            }
        }

    }
}
