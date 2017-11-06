using System;
using Akka.Actor;
using Akka.Routing;
using SharedCalculation.BusinessDomain.Calculation.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors {
    public class CalculationCoordinatorActor : ReceiveActor {

        private static readonly string SumCalculationName = "sumCalculation";

        public CalculationCoordinatorActor() {
            Receive<AddMessage>(x => {
                Context.Child(SumCalculationName).Forward(x);
            });
            Receive<Terminated>(x => {
                Console.WriteLine("Sender terminated!");
                Self.Tell(PoisonPill.Instance);
            });
        }
        
        protected override void PreStart() {
            Console.WriteLine("CalculationCoordinatorActor started");
            base.PreStart();
            //Context.ActorOf(Props.Create<SummationActor>().WithRouter(new RoundRobinPool(5)), SumCalculationName);
            Context.ActorOf(Props.Create<SummationActor>(), SumCalculationName);

            Context.Watch(Context.Parent);

        }

        protected override void PostStop() {
            base.PostStop();
            Console.WriteLine("CalculationCoordinatorActor stopped");

        }
    }
}