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


        public void Add(int productId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string adress = "/api/Doctors/" + productId;

                HttpResponseMessage response = client.GetAsync(adress).Result;
            }
            //if (!products.Keys.Contains())
            //{
            //    products.Add(productId, 1);
            //}
            //else
            //{
            //    products[productId]++;
            //}
        }

        public void Remove(int productId)
        {
            //if(products.Keys.Contains(productId))
            //{
            //    products.Remove(productId);
            //}
        }
    }
}
