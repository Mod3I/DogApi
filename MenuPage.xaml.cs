using Newtonsoft.Json;
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
    public partial class MenuPage : Page
    {

        public class Breeds
        {
            public string Breed { get; set; }

            public byte[] BreedImg { get; set; }
        }

        public MenuPage()
        {
            InitializeComponent();

            this.DataContext = this;

            ListImages.Items.Clear();

            dynamic jobj;
            JObject list = null;

            if (RedisCacheClass.cacheDataBase.StringGet("allBreedsNames") == string.Empty)
            {
                jobj = JObject.Parse(HttpClientClass.httpResponse(@"https://dog.ceo/api/breeds/list/all").Result);
                list = jobj["message"];

                RedisCacheClass.cacheDataBase.StringSet("allBreedsNames", list.ToString());
            }
            else
            {
                list = JObject.Parse(RedisCacheClass.cacheDataBase.StringGet("allBreedsNames"));
            }

            if (RedisCacheClass.cacheDataBase.StringGet("allBreedsPreviewImages") == string.Empty)
            {
                List<byte[]> allImages = new List<byte[]>();

                foreach (var a in list.Properties())
                {
                    string breedString = a.Name.ToString().Substring(0, 1).ToUpper() + a.Name.ToString().Substring(1, a.Name.Length - 1);
                    string url = "https://dog.ceo/api/breed/" + breedString.ToLower() + "/images";
                    dynamic jobj1 = JObject.Parse(HttpClientClass.httpResponse(url).Result);
                    JArray list1 = jobj1["message"];
                    foreach (var b in list1)
                    {
                        var webClient = new WebClient();
                        byte[] imageBytes = webClient.DownloadData(b.ToString());
                        allImages.Add(imageBytes);
                        ListImages.Items.Add(new Breeds { Breed = breedString, BreedImg = imageBytes });
                        break;
                    }
                }

                string serializedImages = JsonConvert.SerializeObject(allImages);

                RedisCacheClass.cacheDataBase.StringSet("allBreedsPreviewImages", serializedImages);
            }
            else
            {
                string serializedImages = RedisCacheClass.cacheDataBase.StringGet("allBreedsPreviewImages");
                List<byte[]> allImages = JsonConvert.DeserializeObject<List<byte[]>>(serializedImages);
                int i = 0;
                foreach (var a in list.Properties())
                {
                    string breedString = a.Name.ToString().Substring(0, 1).ToUpper() + a.Name.ToString().Substring(1, a.Name.Length - 1);
                    //foreach (var b in allImages)
                    //{
                    //    ListImages.Items.Add(new Breeds { Breed = breedString, BreedImg = b });
                    //    break;
                    //}
                    if (i < allImages.Count && i < list.Count)
                    {
                        ListImages.Items.Add(new Breeds { Breed = breedString, BreedImg = allImages[i] });
                        i++;
                    }
                }
            }

            ListImages.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentBreed = ((sender as Button).DataContext as Breeds).Breed.ToLower();
            Manager.MainFrame.Navigate(new ImagesPage());
        }
    }
}
