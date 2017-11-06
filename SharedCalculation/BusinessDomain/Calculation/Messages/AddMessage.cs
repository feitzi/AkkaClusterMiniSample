namespace SharedCalculation.BusinessDomain.Calculation.Messages
{
      
        public sealed class AddMessage {
            public AddMessage(double summand1, double summand2) {
                Summand1 = summand1;
                Summand2 = summand2;
            }

            public double Summand1 { get; }
            public double Summand2{ get; }
        }
}