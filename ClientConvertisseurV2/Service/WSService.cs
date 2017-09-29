using ClientConvertisseurV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.Service
{
    public class WSService
    {

        private static HttpClient client = HttpClientSingleton.getInstance();

        public WSService()
        {

        }

        public static async Task<List<Devise>> getAllDevisesAsync()
        {
            List<Devise> devises = new List<Devise>();

            HttpResponseMessage response = await client.GetAsync("Devise");
            if (response.IsSuccessStatusCode)
            {
                devises = await response.Content.ReadAsAsync<List<Devise>>();
            }

            return devises;
        }
    }

    public class HttpClientSingleton
    {
        private static HttpClient client;
        
        public static HttpClient getInstance()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:1721/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            return client;
        }
    }
}
