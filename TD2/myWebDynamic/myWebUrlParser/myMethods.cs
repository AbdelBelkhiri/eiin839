using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace myWebUrlParser

{
    class MyMethods
    {

        /*Voir program.cs de myWebUrlParser pour voir le parsing et l'appel des méthodes par URL*/
        public string myMethod(string param1, string param2)
        {
            return "<HTML><BODY> Hello " + param1 + " et " + param2 + "</BODY></HTML>";
        }

        //Simple test avec une autre méthode pour voir si mon parsing d'URL fonctionne bien
        //http://localhost:8089/a/test/welcome?param1=Belkhiri&param2=Abdel
        public string welcome(string name, string firstName)
        {
            return "<HTML>" +
                    "<BODY> " +
                        "<h1> " + 
                            "Hello M." + name + " "  +  firstName +
                        "</h1>" +
                        "<p>Bienvenue à toi blablabla </p>" +
                    "</BODY>" +
                   "</HTML>";
        }

         //QUESTION 5
        public string myExternalMethod(string param1, string param2) {
            ProcessStartInfo start = new ProcessStartInfo();
            // on spécifie le .Exe de la méthode externe que l'on souhaite appelée.
            start.FileName = @"C:\Users\user\Documents\Si4polytech\S8\Soc\TP1\TD2\myWebDynamic\externalMethod\bin\Debug\netcoreapp3.1\externalMethod.exe";
            start.Arguments = param1 + " " + param2;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }
    }
}
