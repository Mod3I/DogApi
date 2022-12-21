using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DogApiApp
{
    class HttpClientClass
    {
        public static async Task<string> httpResponse(string url)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseProxy = false;
            handler.Proxy = null;
            var httpClient = new HttpClient(handler);
            var web = new WebClient();
            return web.DownloadString(url);
        }
    }
}
