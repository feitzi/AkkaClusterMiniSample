using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using SharedCalculation.BusinessDomain.Calculation.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors
{
    public class UltimateQuestionLifeWorker : ReceiveActor {

        private IActorRef cacheResultPublisher;
    
        public UltimateQuestionLifeWorker() {

            Receive<UltimateQuestion>(x => HandleUltimateQuestion(x));
        }

        protected override void PreStart() {
            base.PreStart();
            cacheResultPublisher = DistributedPubSub.Get(Context.System).Mediator;
        }

        private void HandleUltimateQuestion(UltimateQuestion addMessage) {

            Console.WriteLine($"Ich berechne die Frage nach dem Leben, dem Universum und dem ganzen Rest");
              Thread.Sleep(TimeSpan.FromSeconds(3));
            var resultMessage = new CalculationResultMessage(42, addMessage);
            addMessage.ResultReceiver.Tell(resultMessage);
            cacheResultPublisher.Tell(new Publish("resultCache", resultMessage));
        }
    }
}