using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber
{
    public class RegisterSubscriberHandler : IHandleMessages<MessageBusFun.Core.RegisterSubscriber>
    {
        static ILog log = LogManager.GetLogger<RegisterSubscriberHandler>();

   
        public Task Handle(MessageBusFun.Core.RegisterSubscriber message, IMessageHandlerContext context)
        {
            if(message.ChannelName == "ChannelOne")
            {
                if (!Subscriber.Program.IsChannelOneSubReg)
                {
                    log.Info($"Received Registration request, SubscriberID = {message.SubscriberID} + ChannelName = {message.ChannelName}");
                    Subscriber.Program.IsChannelOneSubReg = true;

                    var subReg = new MessageBusFun.Core.SubscriberRegistered
                    {
                        SubscriberID = message.SubscriberID,
                        ChannelName = message.ChannelName
                    };
                    return context.Publish(subReg);
                }
                else
                    return Task.CompletedTask;

            }
            else if(message.ChannelName == "ChannelTwo")
            {
                if (!Subscriber.Program.IsSubscriberRegistered)
                {
                    log.Info($"Received Registration request, SubscriberID = {message.SubscriberID} + ChannelName = {message.ChannelName}");
                    Subscriber.Program.IsSubscriberRegistered = true;

                    var subReg = new MessageBusFun.Core.SubscriberRegistered
                    {
                        SubscriberID = message.SubscriberID,
                        ChannelName = message.ChannelName
                    };
                    return context.Publish(subReg);
                }
                else
                    return Task.CompletedTask;
            }          
            else 
            {
                // Do nothing since Subscriber is already registered.
                return Task.CompletedTask;
            }
        }
    }
}
