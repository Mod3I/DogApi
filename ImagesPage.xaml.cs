using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace DogApiApp
{
    /// <summary>
    /// Логика взаимодействия для ImagesPage.xaml
    /// </summary>
    public partial class ImagesPage : Page
    {
        public class DogImg
        {
            public byte[] img { get; set; }
        }

        public ImagesPage()
        {
            InitializeComponent();
            string url = "https://dog.ceo/api/breed/" + Manager.CurrentBreed.ToString().ToLower() + "/images";
            dynamic jobj = JObject.Parse(HttpClientClass.httpResponse(url).Result);
            JArray list = jobj["message"];
            foreach (var a in list)
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(a.ToString());

                ListImages.Items.Add(new DogImg { img = imageBytes });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MenuPage());
        }
    }
}
