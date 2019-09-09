using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherOne
{
    class DeregisterPublisherHandler : IHandleMessages<MessageBusFun.Core.DeregisterPublisher>
    {
        static ILog log = LogManager.GetLogger<RegisterPublisherHandler>();

        public Task Handle(MessageBusFun.Core.DeregisterPublisher message, IMessageHandlerContext context)
        {
            if (Program.isChannelOneRegistered)
            {
                log.Info($"Received Deregistration request, ChannelName = 'ChannelOne' ");
                Program.isChannelOneRegistered = false;

                var deregPublisher = new MessageBusFun.Core.PublisherDeregistered
                {
                    PublisherName = "PublisherOne",
                    ChannelName = "ChannelOne",
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

