using System;
using Akka.Actor;
using Akka.Routing;
using SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult;
using SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult.Messages;
using SharedCalculation.BusinessDomain.Calculation.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors {
    public class CalculationCoordinatorActor : ReceiveActor {

        private static readonly string SumCalculationName = "summationWorker";
        private static readonly string UltimateQuestionWorker = "ultimateQuestionWorker";
        private static readonly string CacheName = "cache";
        

        public CalculationCoordinatorActor() {
            Receive<ICalculationMessage>(x => {
                Context.Child(CacheName).Tell(x);
            });

            Receive<ResultNotInCache>(x => {
                if (x.command is AddMessage) {
                Context.Child(SumCalculationName).Forward(x.command);
                }
                if (x.command is UltimateQuestion)
                {
                    Context.Child(UltimateQuestionWorker).Forward(x.command);
                }
            });
  
        }
        
        protected override void PreStart() {
            //base.PreStart();
            Console.WriteLine("CalculationCoordinatorActor started");
            Context.ActorOf(Props.Create<SummationWorkerActor>().WithRouter(new RoundRobinPool(5)), SumCalculationName);
            Context.ActorOf(Props.Create<UltimateQuestionLifeWorker>().WithRouter(new RoundRobinPool(5)), UltimateQuestionWorker);
            Context.ActorOf(Props.Create<CalculationResultStoreActor>(), CacheName);

        }

        protected override void PostStop() {
            base.PostStop();
            Console.WriteLine("CalculationCoordinatorActor stopped");

        }
    }
}