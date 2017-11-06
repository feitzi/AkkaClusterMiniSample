using System;
using Akka.Actor;
using SharedCalculation.BusinessDomain.Calculation.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors
{
    public class SummationActor : ReceiveActor {

    
        public SummationActor() {

            Receive<AddMessage>(x => HandleAddMessage(x));
        }

        private void HandleAddMessage(AddMessage addMessage) {

            Console.WriteLine($"Ich berrechne die Aufgabe {addMessage.Summand1}+{addMessage.Summand2}");
            var result = addMessage.Summand1 + addMessage.Summand2;
            Sender.Tell(new CalculationResultMessage(result));

        }
    }
}