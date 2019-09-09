using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class PublisherDeregisteredHandler : IHandleMessages<MessageBusFun.Core.PublisherDeregistered>
    {
        static ILog log = LogManager.GetLogger<PublisherDeregisteredHandler>();

        public Task Handle(MessageBusFun.Core.PublisherDeregistered message, IMessageHandlerContext context)
        {
            log.Info($"Publisher Deregistered successfully..., ChannelName = {message.ChannelName}");
            // Removing Channels that are registered and available.
            string ChannelName = message.ChannelName;
            bool ChannelExists = false;

            if (Consumer.Program.avalableChannels != null && Consumer.Program.avalableChannels.Count > 0)
            {
                foreach (string Channel in Consumer.Program.avalableChannels)
                {
                    if (Channel.ToString().Equals(ChannelName))
                        ChannelExists = true;
                }
            }

            if (ChannelExists)
            {
                Consumer.Program.avalableChannels.Remove(ChannelName);
                var channelRemoved = new MessageBusFun.Core.ChannelAvailabilityChanged
                {
                    ChannelName = message.ChannelName,
                    isAvailable = false
                };
                return context.Publish(channelRemoved);                
            }
                

            // Todo a new handler and class for channel's unavailability

            return Task.CompletedTask;
        }
    }
}
