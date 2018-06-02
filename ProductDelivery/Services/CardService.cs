using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProductDelivery.Models;

namespace ProductDelivery.Services
{
    public class CardService : ICardService
    {
        Dictionary<string, ShoppingCart> card = new Dictionary<string, ShoppingCart>();
        public int Length => card.Keys.Count;
        HttpClient client;

        public CardService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:58123");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Add(string userId,int productId)
        {
            string adress = "/api/Product/getproduct/" + productId;
            HttpResponseMessage response = client.GetAsync(adress).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            Product prodToAdd = JsonConvert.DeserializeObject<Product>(data);
            if (response.IsSuccessStatusCode)
            {
                if (!card.Keys.Contains(userId))
                {
                    ShoppingCart userCard = new ShoppingCart();
                    card.Add(userId, userCard);
                }
                card[userId].Add(prodToAdd);
            }
        }

        public void CheckOut(string userId)
        {
            if (!card.Keys.Contains(userId))
                return;
            Delivery delivery = card[userId].ToDelivery();
            delivery.Client = new Client() { Email = userId };
            delivery.Date = DateTime.Now;
            delivery.Status = DeliveryStatus.InTransist;
            var deliveryJSON = JsonConvert.SerializeObject(delivery);
            string adress = "api/Delivery";
            var content = new StringContent(deliveryJSON, Encoding.UTF8, "application/json");
            if (client.PostAsync(adress, content).Result.IsSuccessStatusCode)
                card.Remove(userId);
        }

        public void Remove(string userId,int productId)
        {
            if(card.Keys.Contains(userId))
            {
                card[userId].Remove(productId);
                if (card[userId].Length == 0)
                    card.Remove(userId);
            }
        }

        public ShoppingCart GetCard(string userId)
        {
            if (!card.Keys.Contains(userId))
                return null;
            return card[userId];
        }
    }
}
