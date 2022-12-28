using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace DogApiApp
{
    public class RedisCacheClass
    {
        public static IDatabase cacheDataBase { get; set; }

        public static void Initialize(IDatabase cache)
        {
            cache.StringSet("allBreedsNames", string.Empty);
            cache.StringSet("allBreedsPreviewImages", string.Empty);
        }
    }
}
