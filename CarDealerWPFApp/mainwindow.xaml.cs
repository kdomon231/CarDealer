using CarDealerWPFApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                    var carResponse = await response.Content.ReadAsStringAsync();
                    var carDeserialize = JsonSerializer.Deserialize<List<Car>>(carResponse);
             
                    dataGrid.ItemsSource = carDeserialize;
                    serverInfoLabel.Content = "Car list loaded";

                }
                else
                {
                    serverInfoLabel.Content = $"Server communication error {response.StatusCode}";
                }
            }
        }

        private async void addCarButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                Car newCar = new Car
                {
                    vin = vinTextBox.Text,
                    brand = brandTextBox.Text,
                    model = modelTextBox.Text,
                    year = Int32.Parse(yearTextBox.Text),
                    price = Convert.ToDecimal(priceTextBox.Text)
                };

                var carSerialize = JsonSerializer.Serialize(newCar);
                var stringContent = new StringContent(carSerialize, Encoding.UTF8, "application/json");
                var request = await client.PostAsync("http://localhost:40419/api/v1/Car/Add", stringContent);

                request.EnsureSuccessStatusCode();
                if (request.IsSuccessStatusCode)
                {
                    serverInfoLabel.Content = "Car added";
                }
                else
                {
                    serverInfoLabel.Content = $"Server communication error {request.StatusCode}";
                }
                
            }
        }

        private async void deleteCarButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {                
                var vin = deleteCarTextBox.Text;
                var request = await client.DeleteAsync($"http://localhost:40419/api/v1/Car/Delete/{vin}");               
                
            }
        }

        private async void updateCarButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                var vin = vinTextBox.Text;
                var response = await client.GetAsync($"http://localhost:40419/api/v1/Car/GetCar/{vin}");
                              
                var carResponse = await response.Content.ReadAsStringAsync();
                var carDeserialize = JsonSerializer.Deserialize<List<Car>>(carResponse);
               
                
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCar = e.AddedItems[0] as Car;
            deleteCarTextBox.Text = selectedCar.vin;
        }

    }
}
