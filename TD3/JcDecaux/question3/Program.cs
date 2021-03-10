using Newtonsoft.Json;
using question2;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace question3
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

            string number = "9207";
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/stations/" + number + "?contract=" + contract +"&apiKey=d4cd239c5e4a2f0335a9003f81d466a18d0dac8f");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine(responseBody + "\n");

                var objResponse1 = JsonConvert.DeserializeObject<Station>(responseBody);

                Console.WriteLine(objResponse1);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
