using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2 {
    class Program {
        static void Main(string[] args) {

            Task.Run(() => {
                while (true) {
                    Console.WriteLine("Test");
                    Thread.Sleep(TimeSpan.FromSeconds(4));
                }

            });

            while (true) {

                var test = Console.ReadLine();
                Console.WriteLine($"Ausgabe {test}");

            }
        }
    }
}
