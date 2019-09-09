using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBusFun;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace PublisherTwo
{
    public class RegisterPublisherHandler: IHandleMessages<MessageBusFun.Core.RegisterPublisherTwo>
    {
        static ILog log = LogManager.GetLogger<RegisterPublisherHandler>();

        public Task Handle(MessageBusFun.Core.RegisterPublisherTwo message, IMessageHandlerContext context)
        {
            if(!Program.isChannelTwoRegistered)
            { 
                log.Info($"Received Registration request, PublisherID = {message.PublisherID}");
                Program.isChannelTwoRegistered = true;
            
                var regPublisher = new MessageBusFun.Core.PublisherRegistered
                {
                   // PublisherID = message.PublisherID,
                    ChannelName = "ChannelTwo",
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
