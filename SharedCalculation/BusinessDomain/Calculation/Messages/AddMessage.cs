using Akka.Actor;

namespace SharedCalculation.BusinessDomain.Calculation.Messages {

    public sealed class AddMessage : ICalculationMessage {
        public AddMessage(double summand1, double summand2, IActorRef resultReceiver) {
            Summand1 = summand1;
            Summand2 = summand2;
            ResultReceiver = resultReceiver;
        }

        public double Summand1 { get; }

        public double Summand2 { get; }

        public IActorRef ResultReceiver { get; }
    }
}