using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherTwo
{
    class DeregisterPublisherHandler : IHandleMessages<MessageBusFun.Core.DeregisterPublisher>
    {
        static ILog log = LogManager.GetLogger<RegisterPublisherHandler>();

        public Task Handle(MessageBusFun.Core.DeregisterPublisher message, IMessageHandlerContext context)
        {
            if (Program.isChannelTwoRegistered)
            {
                log.Info($"Received Deregistration request, ChannelName = 'ChannelTwo' ");
                Program.isChannelTwoRegistered = false;

                var deregPublisher = new MessageBusFun.Core.PublisherDeregistered
                {
                    PublisherName = "PublisherTwo",
                    ChannelName = "ChannelTwo",
                };
                return context.Publish(deregPublisher);
            }
            else
            {
                // Do nothing since Publisher is already registered.
                return Task.CompletedTask;
            }
        }
    }
}

