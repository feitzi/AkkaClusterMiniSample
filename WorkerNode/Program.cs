using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WorkerNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WORKER");
            using (var system = ActorSystem.Create("calculation"))
            {
                system.WhenTerminated.Wait();
            }
        }
    }
}
