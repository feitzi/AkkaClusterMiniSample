using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using SharedCalculation.BusinessDomain.CLI;

namespace ClientNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CLIENT");

            Console.WriteLine("Click to start");

                Console.ReadLine();
            using (var system = ActorSystem.Create("calculation"))
            {

                system.ActorOf(Props.Create<CliClientActor>(), "cliClient");
                system.WhenTerminated.Wait();
            }
        }
    }
}
