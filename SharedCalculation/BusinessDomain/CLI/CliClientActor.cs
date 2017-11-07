using System;
using Akka.Actor;
using Akka.Routing;
using SharedCalculation.BusinessDomain.Calculation.Actors;
using SharedCalculation.BusinessDomain.Calculation.Messages;
using SharedCalculation.BusinessDomain.CLI.Messages;

namespace SharedCalculation.BusinessDomain.CLI {
    public class CliClientActor : ReceiveActor {
        private static readonly string CLI_COMMAND_PARSER_NAME = "commandParser";
        private static readonly string CalculationCoordinatorName = "calculationCoordinators";
        private static readonly string CONSOLE_READER_NAME = "consoleReader";
        
        #region messages

        public sealed class AskUserForInputMessage {
            public string Message { get; }

            public AskUserForInputMessage(string message) {
                Message = message;
            }
        }

        #endregion

        protected override void PreStart() {
            base.PreStart();
            Self.Tell(new AskUserForInputMessage("Frage.."));

            Become(DefaultBehavior);
        }

        public CliClientActor() {
            Context.ActorOf(Props.Create<CliCommandParserActor>(), CLI_COMMAND_PARSER_NAME);
            Context.ActorOf(Props.Create<CalculationCoordinatorActor>().WithRouter(FromConfig.Instance), CalculationCoordinatorName);
            Context.ActorOf(Props.Create<CliReaderActor>(), CONSOLE_READER_NAME);
        }

        private void WaitForResultBehavior() {
            SetReceiveTimeout(TimeSpan.FromSeconds(5));
            Receive<CalculationResultMessage>(x => HandleCalculationResult(x));
            Receive<ReceiveTimeout>(x => HandleReceiveTimeout(x));

        }


        private void DefaultBehavior() {
            SetReceiveTimeout(null);
            Receive<AskUserForInputMessage>(x => HandleAskUserForInput(x));
            Receive<CliInputReceivedMessage>(x => HandleCliInputReceivedMessage(x));
            Receive<InputParsedMessage>(x => HandleInputParsed(x));
        }

        private void HandleCalculationResult(CalculationResultMessage x) {
            Console.WriteLine($"Result is {x.Result}");

            Become(DefaultBehavior);
            Self.Tell(new AskUserForInputMessage("Eine neue Frage"));
            
        }

        private void HandleInputParsed(InputParsedMessage inputParsedMessage) {

            var calculator = Context.Child(CalculationCoordinatorName);
            switch (inputParsedMessage.Command) {

                    case InputParsedMessage.CommandType.Add:
                    calculator.Tell(new AddMessage(inputParsedMessage.Operand1, inputParsedMessage.Operand2, Self));
                        break;
                    case InputParsedMessage.CommandType.InvalidCommand:
                    Self.Tell(new AskUserForInputMessage("Input fehlerhaft. Probieren Sie es nochmals: "));
                        return;
            }
            Become(WaitForResultBehavior);
        }
        

        private void HandleAskUserForInput(AskUserForInputMessage askUserForInputMessage) {
            Context.Child(CONSOLE_READER_NAME).Tell(askUserForInputMessage);
        }

        private void HandleCliInputReceivedMessage(CliInputReceivedMessage cliInputReceivedMessage) {
            Context.Child(CLI_COMMAND_PARSER_NAME).Tell(cliInputReceivedMessage.Input);
        }

        private void HandleReceiveTimeout(ReceiveTimeout m) {
            Become(DefaultBehavior);
            Self.Tell(new AskUserForInputMessage("Die Rechnung konnte nich berrechnet werden. Probieren Sie es nochmals: "));
        }
    }
}