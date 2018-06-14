using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TrackEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:58123/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Console.WriteLine("pushCoords or quit");
                var answer = Console.ReadLine();
                if (answer == "quit")
                    break;
                else
                {
                    try
                    {
                        string[] arguments = answer.Split(' ');
                        int deliveryId = Convert.ToInt32(arguments[0]);
                        double x = Convert.ToDouble(arguments[1]);
                        double y = Convert.ToDouble(arguments[2]);
                        string adress = $"/api/Tracker/{deliveryId}/{x}/{y}";
                        var data = client.PostAsync(adress,new StringContent("")).Result;
                        Console.WriteLine(data.IsSuccessStatusCode);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Wrong input");
                    }
                }
            }
        }
    }
}
