using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using SharedCalculation.BusinessDomain.Calculation.Actors;

namespace WorkerNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WORKER");
            using (var system = ActorSystem.Create("calculation")) {
                system.ActorOf(Props.Create<CalculationCoordinatorActor>(), "calculationCoordinator");
                system.WhenTerminated.Wait();
            }
        }
    }
}
