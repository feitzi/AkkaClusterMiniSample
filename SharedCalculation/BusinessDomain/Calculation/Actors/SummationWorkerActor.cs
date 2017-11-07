using System;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using SharedCalculation.BusinessDomain.Calculation.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors
{
    public class SummationActor : ReceiveActor {

        private IActorRef cacheResultPublisher;
    
        public SummationActor() {

            Receive<AddMessage>(x => HandleAddMessage(x));
        }

        protected override void PreStart() {
            base.PreStart();
            cacheResultPublisher = DistributedPubSub.Get(Context.System).Mediator;
        }

        private void HandleAddMessage(AddMessage addMessage) {

            Console.WriteLine($"Ich berechne die Aufgabe {addMessage.Summand1}+{addMessage.Summand2}");
            var result = addMessage.Summand1 + addMessage.Summand2;
            var resultMessage = new CalculationResultMessage(result, addMessage);
            addMessage.ResultReceiver.Tell(resultMessage);
            cacheResultPublisher.Tell(new Publish("resultCache", resultMessage));
        }
    }
}