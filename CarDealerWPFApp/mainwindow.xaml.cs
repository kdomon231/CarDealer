using CarDealerWPFApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        private List<Car> listOfCars = new List<Car>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void getAllCarsButton_Click(object sender, RoutedEventArgs e)
        {
            await GetAllCars();
        }

        private async Task GetAllCars()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:40419/api/v1/Car/GetAll");

                if (response.IsSuccessStatusCode)
                {
                    var carResponse = await response.Content.ReadAsStringAsync();
                    listOfCars = JsonSerializer.Deserialize<List<Car>>(carResponse);

                    dataGrid.ItemsSource = listOfCars;
                    serverInfoLabel.Content = "Car list loaded";
                }
                else
                {
                    serverInfoLabel.Content = $"Server error {response.StatusCode}";
                }
            }
        }

        private async void addCarButton_Click(object sender, RoutedEventArgs e)
        {
            await GetAllCars();
            if (listOfCars.Exists(x => x.vin == vinTextBox.Text))
            {
                serverInfoLabel.Content = "Car with this VIN already exists";
                return;
            }
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

                if (request.IsSuccessStatusCode)
                {
                    await GetAllCars();
                    serverInfoLabel.Content = "Car added";
                }
                else
                {
                    serverInfoLabel.Content = $"Car adding error {request.StatusCode}";
                }
            }
        }

        private async void deleteCarButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                var vin = vinLabel.Content;
                var request = await client.DeleteAsync($"http://localhost:40419/api/v1/Car/Delete/{vin}");
                await GetAllCars();

                if (request.IsSuccessStatusCode)
                {
                    serverInfoLabel.Content = "Car removed";
                }
                else
                {
                    serverInfoLabel.Content = "Error! Car doesn't exist";
                }
            }
        }

        private async void updateCarButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                Car updatedCar = new Car
                {
                    //vin = vinTextBox.Text,
                    brand = brandTextBox.Text,
                    model = modelTextBox.Text,
                    year = Int32.Parse(yearTextBox.Text),
                    price = Convert.ToDecimal(priceTextBox.Text)
                };

                var carSerialize = JsonSerializer.Serialize(updatedCar);
                var stringContent = new StringContent(carSerialize, Encoding.UTF8, "application/json");

                var vin = vinLabel.Content;
                var request = await client.PutAsync($"http://localhost:40419/api/v1/Car/Update/{vin}", stringContent);

                if (request.IsSuccessStatusCode)
                {
                    await GetAllCars();
                    serverInfoLabel.Content = "Car updated";
                }
                else
                {
                    serverInfoLabel.Content = $"Error updating car {request.StatusCode}";
                }
            }
        }

        private async void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var carView = dataGrid.SelectedItem as Car;

            if (carView != null)
            {
                vinLabel.Content = carView.vin;
                brandTextBox.Text = carView.brand;
                modelTextBox.Text = carView.model;
                yearTextBox.Text = carView.year.ToString();
                priceTextBox.Text = carView.price.ToString();
            }
        }

        private void clearFieldsButton_Click(object sender, RoutedEventArgs e)
        {
            vinLabel.Content = null;
            vinTextBox.Text = null;
            brandTextBox.Text = null;
            modelTextBox.Text = null;
            yearTextBox.Text = null;
            priceTextBox.Text = null;
            serverInfoLabel.Content = "Fields cleared";
        }
    }
}
