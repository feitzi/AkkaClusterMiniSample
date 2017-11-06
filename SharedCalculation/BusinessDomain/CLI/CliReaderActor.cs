using System;
using Akka.Actor;
using SharedCalculation.BusinessDomain.CLI.Messages;

namespace SharedCalculation.BusinessDomain.CLI {
    public class CliReaderActor : ReceiveActor {
        public CliReaderActor() {

            Receive<CliClientActor.AskUserForInputMessage>(message => {
                Console.WriteLine((string) message.Message);
                var input = Console.ReadLine();

                Sender.Tell(new CliInputReceivedMessage(input));

            });

        }
    }
}