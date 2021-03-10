using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace question1
{
    class Program
    {
       
        static readonly HttpClient client = new HttpClient();
        static async Task Main()
        {
           
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v1/contracts?apiKey=d4cd239c5e4a2f0335a9003f81d466a18d0dac8f");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                

                Console.WriteLine(responseBody);

                var objResponse1 = JsonConvert.DeserializeObject<List<Contrat>>(responseBody);
                
                int count = 0;

                foreach (Contrat element in objResponse1)
                {
                    Console.WriteLine($"Contrat #{count}: {element.name}");
                    count++;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
    public class Contrat
    {
        public string name { get; set; }
        public string commercial_name { get; set; }
        public List<string> cities { get; set; }
        public string countryCode { get; set; }
    }
}