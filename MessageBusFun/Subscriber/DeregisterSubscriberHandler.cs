using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber
{
    class DeregisterSubscriberHandler : IHandleMessages<MessageBusFun.Core.DeregisterSubscriber>
    {
        static ILog log = LogManager.GetLogger<DeregisterSubscriberHandler>();

        public Task Handle(MessageBusFun.Core.DeregisterSubscriber message, IMessageHandlerContext context)
        {
            if (Program.IsSubscriberRegistered)
            {
                log.Info($"Received Deregistration request for Subscriber requestID... {message.DeregistrationID} ");
                Program.IsSubscriberRegistered = false;

                var deregSubscriber = new MessageBusFun.Core.SubscriberDeregistered
                {
                    SubscriberName = "Subscriber"
                   
                };
                return context.Publish(deregSubscriber);
            }
            else
            {
                // Do nothing since Subscriber is already deregistered.
                return Task.CompletedTask;
            }
        }
    }
}

