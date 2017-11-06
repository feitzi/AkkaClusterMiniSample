using System;
using Akka.Actor;
using SharedCalculation.BusinessDomain.CLI.Messages;

namespace SharedCalculation.BusinessDomain.CLI {
    public class CliCommandParserActor : ReceiveActor {
        public CliCommandParserActor() {
            Receive<string>(x => ParseMessage(x));
        }

        private void ParseMessage(string input) {

            if (input.Equals("x")) {
                Context.System.Terminate();
                return;
            }
            var splitInput = input.Split('+', '-');

            if (splitInput.Length != 2) {
                Sender.Tell(new InputParsedMessage(InputParsedMessage.CommandType.InvalidCommand, Double.NegativeInfinity, Double.NegativeInfinity));
                return;
            }

            var commandParameter = input[splitInput[0].Length];

            var operand1 = Convert.ToDouble(splitInput[0]);
            var operand2 = Convert.ToDouble(splitInput[1]);

            InputParsedMessage.CommandType commandType;

            switch (commandParameter) {
                case '+':
                    commandType = InputParsedMessage.CommandType.Add;
                    break;
                default:
                    commandType = InputParsedMessage.CommandType.InvalidCommand;
                    break;
            }
            
            Sender.Tell(new InputParsedMessage(commandType, operand1, operand2));

        }
    }
}