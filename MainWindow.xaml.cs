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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ServiceStack.Text;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Drawing;
using System.Windows.Interop;
using System.IO;
using System.Threading;
using StackExchange.Redis;

namespace DogApiApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RedisCacheClass.Initialize(RedisCacheClass.cacheDataBase);

            Manager.MainFrame = MainFrame;
            Manager.MainFrame.Navigate(new MenuPage());
        }
    }
}
