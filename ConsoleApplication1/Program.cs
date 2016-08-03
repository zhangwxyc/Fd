using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(new StartOptions(url: "http://localhost:7000")))
            {
                Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
