using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleNetFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string param1 = ConfigurationManager.AppSettings["param1"].ToString();
                 
            Console.WriteLine($"Hello, World! {param1}");
        }
    }
}
