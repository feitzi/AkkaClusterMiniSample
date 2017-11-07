using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Util.Internal;
using SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult.Messages;
using SharedCalculation.BusinessDomain.Calculation.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult {
    public class CalculationResultStoreActor  : ReceiveActor{

        private Dictionary<string, double> cachedResults = new Dictionary<string, double>();

        public CalculationResultStoreActor() {
            Receive<AddMessage>(x => HandleGetResultFromCacheAdd(x));
            Receive<UltimateQuestion>(x => HandleUltimateQuestion(x));
            Receive<CalculationResultMessage>(x => HandleCalculationResult(x));

            DistributedPubSub.Get(Context.System).Mediator.Tell(new Subscribe("resultCache", Self));

        }

        private void HandleUltimateQuestion(UltimateQuestion ultimateQuestion) {
            var key = $"ultimateQuestion";
            HandleCalculationWithCacheKey(ultimateQuestion, key);


        }

        private void HandleCalculationResult(CalculationResultMessage resultMessage) {
            if (resultMessage.command is AddMessage) {
                var addMessage = resultMessage.command as AddMessage;
                var key = $"add_{addMessage.Summand1}_{addMessage.Summand2}";
                cachedResults.AddOrSet(key, resultMessage.Result);
            }
            if (resultMessage.command is UltimateQuestion)
            {
                var key = $"ultimateQuestion";
                cachedResults.AddOrSet(key, resultMessage.Result);
            }
        }

        private void HandleGetResultFromCacheAdd(AddMessage add) {
            var key = $"add_{add.Summand1}_{add.Summand2}";
            HandleCalculationWithCacheKey(add, key);
        }

        private void HandleCalculationWithCacheKey(ICalculationMessage add, string key) {
            double cachedResult;
            if (cachedResults.TryGetValue(key, out cachedResult)) {
                Console.WriteLine($"Aufgabe gefunden im Cache. Result ist {cachedResult}");
                add.ResultReceiver.Tell(new CalculationResultMessage(cachedResult, add), Sender);
                return;
            }

            Sender.Tell(new ResultNotInCache(add));
        }
    }
}