using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace myHttpListener
{
    class HeadersDetail
    {
        WebHeaderCollection headers;

        public HeadersDetail(HttpListenerRequest request)
        {
            this.headers = (WebHeaderCollection) request.Headers;
        }

        public void printAllHeaders() {       
          
            //permet de récupérer chaque header et d'afficher chaque valeur
            foreach (string key in this.headers.AllKeys)
            {
                string[] values = this.headers.GetValues(key);

                if (values.Length > 0) {

                    //print les valeurs des headers
                    Console.WriteLine(" Les valeurs de {0} header sont : ", key);

                    foreach (string value in values)
                    {
                        Console.WriteLine("  {0}", value);
                    }
                }
                else
                {
                    Console.WriteLine("Pas de valeur associé au header");
                }
            }
        }

        public void printSpecificHeader(HttpRequestHeader header)
        {
            Console.WriteLine($"{header}: {this.headers.Get(header.ToString())}");
        }

        public WebHeaderCollection getHeaders()
        {
            return this.headers;
        }

    }
}