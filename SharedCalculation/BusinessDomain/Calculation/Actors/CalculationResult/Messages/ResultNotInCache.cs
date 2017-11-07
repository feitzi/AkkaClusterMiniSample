namespace SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult.Messages {
    public class ResultNotInCache {
        public ResultNotInCache(object command) {
            this.command = command;
        }

        public object command { get; }


    }
}