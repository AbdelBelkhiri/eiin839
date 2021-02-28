using System;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace myWebUrlParser
{
    internal class Program

    /*
     * QUESTION 2 -->

        J'ai effectivement tester de lancer le projet BasicWebServerUrlParser et j'ai mis dans le browser
        l'url : http://localhost:8080/a/b/c?param1=1&param2=2&param3=3&param4=4
        
        Le module HttpUtility permet de parser correctement l'url et de récupérer tous les paramètres.
        
        exemple :
        HttpUtility.ParseQueryString(request.Url.Query).Get("param1")

     */


    {
        private static void Main(string[] args)
        {

            //if HttpListener is not supported by the Framework
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }


            // Create a listener.
            HttpListener listener = new HttpListener();

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                    // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
                    // user="Tout le monde" is language dependent, use user=Everyone in english 

                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();

            // get args 
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            // Trap Ctrl-C on console to exit 
            Console.CancelKeyPress += delegate {
                // call methods to close socket and exit
                listener.Stop();
                listener.Close();
                Environment.Exit(0);
            };


            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }

                // get url 
                Console.WriteLine($"Received request for {request.Url}");

                
                //get url protocol
                Console.WriteLine(request.Url.Scheme);
                //get user in url
                Console.WriteLine(request.Url.UserInfo);
                //get host in url
                Console.WriteLine(request.Url.Host);
                //get port in url
                Console.WriteLine(request.Url.Port);
                //get path in url 
                Console.WriteLine(request.Url.LocalPath);
                


                /*************************** Question 4 - 5 *******************************/

                /** J'ai tester avec cette méthode cela me renvoie bien le contenue HTML avec les paramètres
                  * que je passe dans la méthode la classe.
                  * http://localhost:8089/a/test/myMethod?param1=2&param2=3   
                   **/

                //on récupére les segments que l'on stocke dans un tableau 
                string[] segments = request.Url.Segments;

                //on parse l'URL à l'aide du HTTPUTILITY vu dans la question 2 du TD
                Object[] parametres = new Object[2];
                parametres[0] = HttpUtility.ParseQueryString(request.Url.Query).Get("param1");
                parametres[1] = HttpUtility.ParseQueryString(request.Url.Query).Get("param2");

                //Récupération de la classe comme vu dans l'exmple de Reflection Sample
                //récupération de la méthode à éxécuter 
                //on pourrait ici faire en fonction d'un paramètre dans l'URL faire la récupération dynamique du nom de la classe
                Type type = typeof(MyMethods);
                MethodInfo method = type.GetMethod(segments[segments.Length - 1]);

                

                string result = " ";
                MyMethods mmethods = new MyMethods();

                //gestion de l'appel de la double requête avec favicon 
                //je ne sais pas comment emp^cher ce double appel
                if (method != null)
                {
                    Console.WriteLine("méthode ---> " + segments[segments.Length - 1]);
                    result = (string)method.Invoke(mmethods, parametres);
                    Console.WriteLine("Resultat de la méthode : " + result + "\n");
                }
                else
                {
                    // par défault 
                    result = "< HTML >< BODY > Hello world! </ BODY ></ HTML >";
                    Console.WriteLine("Erreurrrr");
                }

                // parse path in url 
                foreach (string str in request.Url.Segments)
                {
                    Console.WriteLine("-------");
                    Console.WriteLine(str);
                    
                    // On récupère la classe et la méthode
                    //Type type = typeof(myMethods);

                }

                /*********************************************************************/

                //get params un url. After ? and between &

                Console.WriteLine(request.Url.Query);

                //parse params in url
                Console.WriteLine("param1 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param1"));
                Console.WriteLine("param2 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param2"));
                /*Console.WriteLine("param3 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param3"));
                Console.WriteLine("param4 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param4"));*/


                Console.WriteLine(documentContents);

                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                // Construct a response.
                // Si on a reçoit une URL (avec paramètres) on l'a parse et on renvoie le résultat HTML 
                //sino laise par défaut le HELLO WORLD !
                string responseString = result;

                
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            // Httplistener neither stop ... But Ctrl-C do that ...
            // listener.Stop();
        }
    }
}
