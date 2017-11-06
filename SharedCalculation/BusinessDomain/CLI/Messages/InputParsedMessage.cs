using SharedCalculation.Utility;

namespace SharedCalculation.BusinessDomain.CLI.Messages {

    [Immutable]
    public sealed  class InputParsedMessage {
        public  CommandType Command { get; }
        public double Operand1 { get; }
        public double Operand2 { get; }

        public enum CommandType {
            Add,
            Sub,
            Mul,
            Div,
            InvalidCommand
        }

        public InputParsedMessage(CommandType command, double operand1, double operand2) {
            Command = command;
            Operand1 = operand1;
            Operand2 = operand2;
        }
    }

    
}