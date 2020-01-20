using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeuPedido
{
    public class AppData
    {
        public static List<Product> Products { get; set; } = new List<Product>();
        public static List<Sale> Sales { get; set; } = new List<Sale>();
        public static List<Category> Categories { get; set; } = new List<Category>();
        public static Cart CurrentCart = new Cart();

        private static AppData instance;
        public static AppData GetInstance()
        {
            if(instance == null)
            {
                instance = new AppData();
              
            }
            return instance;
        }

        private AppData()
        {
            
        }

        public async Task UpdateData()
        {

            var httpClient = new HttpClient();

            string productUrl = "https://pastebin.com/raw/eVqp7pfX";
            Task<string> productsTask = httpClient.GetStringAsync(productUrl);
            string productsContent = await productsTask;
            Products = JsonConvert.DeserializeObject<List<Product>>(productsContent);
            Products.Sort((a, b) => a.Category_id < b.Category_id ? -1 : 1);

            string salesUrl = "https://pastebin.com/raw/R9cJFBtG";
            Task<string> salesTask = httpClient.GetStringAsync(salesUrl);
            string salesContent = await salesTask;
            Sales = JsonConvert.DeserializeObject<List<Sale>>(salesContent);

            string categoriesUrl = "https://pastebin.com/raw/YNR2rsWe";
            Task<string> categoriesTask = httpClient.GetStringAsync(categoriesUrl);
            string categoriesContent = await categoriesTask;
            Categories = JsonConvert.DeserializeObject<List<Category>>(categoriesContent);
        }
    }
}
