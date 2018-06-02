using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;

namespace ProductDelivery.Models
{
    public class ShoppingCart
    {
        Dictionary<Product, int> products = new Dictionary<Product, int>();

        public int Length {
            get {
                return products.Keys.Count;
            }
        }

        public IEnumerable<Product> Products {
            get
            {
                return products.Keys;
            }
        }

        public int this[int prodId]
        {
            get
            {
                var prod = products.Keys.FirstOrDefault(p => p.Id == prodId);
                if (prod == null)
                    return 0;
                return products[prod];
            }
        }

        public void Add(int productId)
        {
            if (!products.Keys.Select(p => p.Id).Contains(productId))
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:58123");

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string adress = "/api/Product/getproduct/" + productId;

                    HttpResponseMessage response = client.GetAsync(adress).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Product));
                        var data = response.Content.ReadAsStringAsync().Result;
                        Product prodToAdd = JsonConvert.DeserializeObject<Product>(data);
                        if (!products.Keys.Select(p => p.Id).Contains(prodToAdd.Id))
                        {
                            products.Add(prodToAdd, 1);
                        }
                    }
                }
            }
            else
            {
                products[products.Keys.First(p=> p.Id == productId)]++;
            }
        }

        public void Remove(int productId)
        {
            var prodToDelete = products.Keys.FirstOrDefault(p => p.Id == productId);
            if (prodToDelete!=null)
            {
                if (--products[prodToDelete] == 0)
                    products.Remove(prodToDelete);
            }
        }
    }
}
