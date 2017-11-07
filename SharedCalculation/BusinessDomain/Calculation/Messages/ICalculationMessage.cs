using Akka.Actor;

namespace SharedCalculation.BusinessDomain.Calculation.Messages {
    public interface ICalculationMessage {
        IActorRef ResultReceiver { get; }
    }
}