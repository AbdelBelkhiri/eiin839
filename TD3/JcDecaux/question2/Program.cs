using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace question2
{
    class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.

            //on peut également le définir en tant que paramètre dans les arguments de l'application
            string contract = "marseille";
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + "&apiKey=d4cd239c5e4a2f0335a9003f81d466a18d0dac8f");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine(responseBody);

                var objResponse1 = JsonConvert.DeserializeObject<List<Station>>(responseBody);

                int count = 0;

                foreach (Station element in objResponse1)
                {
                    Console.WriteLine($"Station #{count}: {element}");
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

    public class Station
    {
        public string number { get; set; }
        public string contract_name { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        public Boolean banking { get; set; }
        public Boolean bonus { get; set; }
        public int bike_stands { get; set; }
        public int available_bike_stands { get; set; }
        public string status { get; set; }
        public string last_update { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("Number : " + this.number + "\n");
            sb.Append("contract name : " + this.contract_name + "\n");
            sb.Append("name : " + this.name + "\n");
            sb.Append("address : " + this.address + "\n");
            sb.Append("banking : " + this.banking + "\n");
            sb.Append("Position  : Latitude -> " + this.position.lat + "| Longitude -> " + this.position.lat + "\n");
            sb.Append("bike_stands : " + this.bike_stands + "\n");
            sb.Append("available_bike_stands : " + this.available_bike_stands + "\n");
            sb.Append("status : " + this.status + "\n");
            sb.Append("last_update : " + this.last_update + "\n");
            return sb.ToString();
        }
    }

    public class Position
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
}
