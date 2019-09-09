using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber
{
    class ChannelAvailabilityChangedHandler : IHandleMessages<MessageBusFun.Core.ChannelAvailabilityChanged>
    {
        static ILog log = LogManager.GetLogger<ChannelAvailabilityChangedHandler>();

        public Task Handle(MessageBusFun.Core.ChannelAvailabilityChanged message, IMessageHandlerContext context)
        {
            log.Info($"Received Channel availability changed notification.... ChannelName = {message.ChannelName}");

            if (message.isAvailable == true)
                log.Info($"{message.ChannelName} has become available...");
            else if(message.isAvailable == false)
                log.Info($"{message.ChannelName} has become unavailable...");

            return Task.CompletedTask;
        }
    }
}
