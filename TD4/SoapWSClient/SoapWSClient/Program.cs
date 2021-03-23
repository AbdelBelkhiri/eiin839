using ServiceReference1;
using System;

namespace SoapWSClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorSoap calculator = new CalculatorSoapClient(CalculatorSoapClient.EndpointConfiguration.CalculatorSoap);
            Console.WriteLine("2*2 : " + calculator.MultiplyAsync(2, 2).Result);
            Console.ReadLine();
        }
    }
}
