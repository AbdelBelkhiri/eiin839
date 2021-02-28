using System;
using System.Diagnostics;
using System.Text;

namespace ExternalPythonScript
{
    class Program
    {
        //appel à un script python qui additionne 2 nonmbres et renvoie le résultat
        static void Main(string[] args) => scriptExec(18, 20);

        public static void scriptExec(int param1, int param2)

        {
            string fileName = @"C:\Users\user\Documents\Si4polytech\S8\Soc\TP1\TD2\myWebDynamic\ExternalPhpScript\program.py" + " " + param1 + " " + param2;
            
            Process process = new Process();
            
            //process.StartInfo.Arguments = param1 + " " + param2;
            process.EnableRaisingEvents = true;
            process.StartInfo.UseShellExecute = false;
            //C:\Users\user\AppData\Local\Microsoft\WindowsApps

            //on charge l'éxécutable python
            process.StartInfo = new ProcessStartInfo(@"C:\Users\user\AppData\Local\Microsoft\WindowsApps\python3.9.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            Console.WriteLine("Résulat de l'appel du script python");

            Console.WriteLine(output);

        }
    }
}
