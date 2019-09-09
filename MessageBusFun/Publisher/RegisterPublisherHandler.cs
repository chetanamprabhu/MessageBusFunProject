using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBusFun;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace PublisherOne
{
    public class RegisterPublisherHandler: IHandleMessages<MessageBusFun.Core.RegisterPublisher>
    {
        static ILog log = LogManager.GetLogger<RegisterPublisherHandler>();

        public Task Handle(MessageBusFun.Core.RegisterPublisher message, IMessageHandlerContext context)
        {
            if (!Program.isChannelOneRegistered)
            {
                log.Info($"Received Registration request, PublisherID = {message.PublisherID}");
                Program.isChannelOneRegistered = true;

                var regPublisher = new MessageBusFun.Core.PublisherRegistered
                {
                    // PublisherID = message.PublisherID,
                    ChannelName = "ChannelOne",
                };
                return context.Publish(regPublisher);
            }
            else
            {
                // Do nothing since Publisher is already registered.
                return Task.CompletedTask;
            }
        }
    }
}
