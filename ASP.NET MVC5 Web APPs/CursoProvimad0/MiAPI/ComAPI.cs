using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MisDatos2;

using System.Net.Http;
using System.Net.Http.Headers;

namespace MiAPI
{
    public class ComAPI
    {
        protected string URLBase;
        protected HttpClient mCLient = new HttpClient();
        public ComAPI(string url)
        {
            URLBase = url;
        }

        public async Task<Product> Get(int id)
        {
            mCLient = new HttpClient();
            mCLient.BaseAddress = new Uri(URLBase);
            mCLient.DefaultRequestHeaders.Accept.Clear();
            mCLient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = $"{URLBase}/Products/{id}";
            var result = await mCLient.GetAsync(path);

            Product p = null;

            if (result.IsSuccessStatusCode)
            {
                p = await result.Content.ReadAsAsync<Product>();
            }

            return p;
        }
    }
}
