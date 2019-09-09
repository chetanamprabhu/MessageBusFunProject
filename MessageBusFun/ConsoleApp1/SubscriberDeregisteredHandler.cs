using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class SubscriberDeregisteredHandler : IHandleMessages<MessageBusFun.Core.SubscriberDeregistered>
    {
        static ILog log = LogManager.GetLogger<PublisherDeregisteredHandler>();

        public Task Handle(MessageBusFun.Core.SubscriberDeregistered message, IMessageHandlerContext context)
        {
            log.Info($"Subscriber Deregistered successfully...");
            return Task.CompletedTask;
        }
    }
}
