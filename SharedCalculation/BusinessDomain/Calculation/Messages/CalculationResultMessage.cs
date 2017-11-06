namespace SharedCalculation.BusinessDomain.Calculation.Messages {
    public class CalculationResultMessage {
        public double Result { get; }

        public CalculationResultMessage(double result) {
            Result = result;
        }
    }
}