using System;
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
			MyWebView.Source = new Uri("https://localhost:7034");
        }
        public string JSONFormatting(string jsonResponse)
        {
			jsonResponse = jsonResponse.Replace("\\", "");
			jsonResponse = jsonResponse.Substring(1, jsonResponse.Length - 2);
			return jsonResponse;
		}

    }
}
