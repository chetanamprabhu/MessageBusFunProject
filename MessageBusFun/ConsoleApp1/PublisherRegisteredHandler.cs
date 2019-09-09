using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel
{
    class PublisherRegisteredHandler : IHandleMessages<MessageBusFun.Core.PublisherRegistered>
    {
        static ILog log = LogManager.GetLogger<PublisherRegisteredHandler>();

        public Task Handle(MessageBusFun.Core.PublisherRegistered message, IMessageHandlerContext context)
        {
            log.Info($"Publisher Registered successfully..., NominatedChannelName = {message.ChannelName}");
            // Adding Channels that are registered and available.
            string ChannelName = message.ChannelName;
            bool ChannelExists = false; 

            if(Consumer.Program.avalableChannels != null && Consumer.Program.avalableChannels.Count > 0)
            {
                foreach(string Channel in Consumer.Program.avalableChannels)
                {
                    if (Channel.ToString().Equals(ChannelName))
                        ChannelExists = true;
                }
            }

            if (!ChannelExists)
            {
                Consumer.Program.avalableChannels.Add(ChannelName);
                var channelAdded = new MessageBusFun.Core.ChannelAvailabilityChanged
                {
                    ChannelName = message.ChannelName,
                    isAvailable = true
                };
                return context.Publish(channelAdded);
            }

                return Task.CompletedTask;
        }
    }
}
