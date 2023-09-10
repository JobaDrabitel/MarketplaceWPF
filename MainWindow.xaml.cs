﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
using System.Net.Http.Json;
using API_Marketplace_.net_7_v1.Models;
using System.Text.RegularExpressions;

namespace MarketplaceWPF
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
        private async void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            // Получите введенные значения имени пользователя (или email) и пароля
            string email = EmailTB.Text;
            string password = PasswordBox.Password;

            // Создаем запрос
            var loginRequest = new
            {
                Email = email,
                PasswordHash = password
            };

            try
            {
                using (var client = new HttpClient())
                {
                    // Адрес API
                    client.BaseAddress = new Uri("http://localhost:8080/"); // Укажите адрес своего API

                    // Хуй знает че это если честно
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                    // Сериализуем в JSON
                    var jsonBody = JsonSerializer.Serialize(loginRequest);

                    // POST-запрос с JSON-телом на авторизацию
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("api/user/auth", content);

                    // Ответ
                    if (response.IsSuccessStatusCode)
                    {
                        // Получите JSON-ответ и десериализуйте его в объект пользователя
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        // ХЗ почему, json файл приходит пиздец кривой, в лишних ковычках и с лишними \ поэтому приходится его поправлять, заебало, надеюсь когда-то поправлю
                        jsonResponse = JSONFormatting (jsonResponse);
                        var user = JsonSerializer.Deserialize<User>(jsonResponse);
                        if (user != null)
                        {
                            // Пользователь авторизован
                            MessageBox.Show("Authorization successful.");
                        }
                        else
                        {
                            // Пользователь не найден или аутентификация не удалась
                            MessageBox.Show("User not found or authentication failed.");
                        }
                    }
                    else
                    {
                        // На всякий
                        MessageBox.Show("Authorization error: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработайте исключение, если необходимо
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private static string JSONFormatting(string jsonResponse)
        {
            jsonResponse = jsonResponse.Replace("\\", "");
            jsonResponse = jsonResponse.Substring(1, jsonResponse.Length - 2);
            return jsonResponse;
        }
    }
}
