namespace SharedCalculation.BusinessDomain.Calculation.Messages {
    public class CalculationResultMessage {
        public double Result { get; }
        public object command { get; }

        public CalculationResultMessage(double result, object command) {
            Result = result;
            this.command = command;
        }
    }
}