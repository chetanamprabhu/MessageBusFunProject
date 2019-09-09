using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class SubscriberRegisteredHandler : IHandleMessages<MessageBusFun.Core.SubscriberRegistered>
    {
        static ILog log = LogManager.GetLogger<SubscriberRegisteredHandler>();
        public Task Handle(MessageBusFun.Core.SubscriberRegistered message, IMessageHandlerContext context)
        {
               log.Info($"Subscriber Registered successfully.... ChannelName registered to: {message.ChannelName}...");
                return Task.CompletedTask;
        }
    }
}
