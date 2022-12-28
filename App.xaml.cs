using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using StackExchange.Redis;

namespace DogApiApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            RedisCacheClass.cacheDataBase = redis.GetDatabase();
        }
    }
}
