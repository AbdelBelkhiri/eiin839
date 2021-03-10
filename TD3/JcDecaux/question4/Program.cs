using Newtonsoft.Json;
using question2;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using GeoCoordinatePortable;

namespace question4
{
    class Program
    {

        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            //change parameters to test
            string contract = "marseille";

            GeoCoordinate point = new GeoCoordinate(50.35, 27.22);

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + "&apiKey=d4cd239c5e4a2f0335a9003f81d466a18d0dac8f");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                //Console.WriteLine(responseBody);

                var objResponse1 = JsonConvert.DeserializeObject<List<Station>>(responseBody);

                Station station = closest(point, contract, objResponse1);       

                Console.WriteLine("La station la plus proche est : " + station.name);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public static Station closest(GeoCoordinate point, string contract, List<Station> stations){
            Station cloesestStation = null;

            foreach (Station element in stations)
            {
                GeoCoordinate p = new GeoCoordinate(element.position.lat, element.position.lng);
                double distance = p.GetDistanceTo(point);

                if (p.GetDistanceTo(point) <= distance)
                {
                    cloesestStation = element;
                }
            }

            return cloesestStation;
        }
    }
}
