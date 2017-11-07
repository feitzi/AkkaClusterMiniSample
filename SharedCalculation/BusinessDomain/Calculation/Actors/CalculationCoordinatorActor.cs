﻿using System;
using Akka.Actor;
using Akka.Routing;
using SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult;
using SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult.Messages;
using SharedCalculation.BusinessDomain.Calculation.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors {
    public class CalculationCoordinatorActor : ReceiveActor {

        private static readonly string SumCalculationName = "sumCalculation";
        private static readonly string CacheName = "cache";
        

        public CalculationCoordinatorActor() {
            Receive<AddMessage>(x => {
                Context.Child(CacheName).Tell(x);
            });

            Receive<ResultNotInCache>(x => {
                if (x.command is AddMessage) {
                Context.Child(SumCalculationName).Forward(x.command);
                }
            });
  
        }
        
        protected override void PreStart() {
            //base.PreStart();
            Console.WriteLine("CalculationCoordinatorActor started");
            Context.ActorOf(Props.Create<SummationActor>().WithRouter(new RoundRobinPool(5)), SumCalculationName);
            Context.ActorOf(Props.Create<CalculationResultStoreActor>(), CacheName);

        }

        protected override void PostStop() {
            base.PostStop();
            Console.WriteLine("CalculationCoordinatorActor stopped");

        }
    }
}