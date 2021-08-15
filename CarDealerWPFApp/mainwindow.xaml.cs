using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace CarDealerWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void getAllCarsButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:40419/api/v1/Car/GetAll");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    //dataGrid.ItemsSource = response
                    serverInfoLabel.Content = "OK";
                }
                else
                {
                    serverInfoLabel.Content = $"Server communication error {response.StatusCode}";
                }
            }
        }

        private async void addCarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteCarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateCarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
