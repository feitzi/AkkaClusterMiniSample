namespace SharedCalculation.BusinessDomain.CLI.Messages {
    public sealed class CliInputReceivedMessage {

        public string Input { get; }

        public CliInputReceivedMessage(string input) {
            Input = input;
        }
    }
}