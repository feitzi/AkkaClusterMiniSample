using Akka.Actor;

namespace SharedCalculation.BusinessDomain.Calculation.Messages {

    public sealed class UltimateQuestion : ICalculationMessage {
        public UltimateQuestion(IActorRef resultReceiver) {
            ResultReceiver = resultReceiver;
        }

        public IActorRef ResultReceiver { get; }
    }
}