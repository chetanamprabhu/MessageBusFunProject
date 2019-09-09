using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber
{
    class ReceivedMessagesHandler : IHandleMessages<MessageBusFun.Core.SendMessageSubscribed>
    {
        static ILog log = LogManager.GetLogger<ReceivedMessagesHandler>();

        public Task Handle(MessageBusFun.Core.SendMessageSubscribed message, IMessageHandlerContext context)
        {
            if (Program.IsSubscriberRegistered)
            {
                log.Info($"Subscriber has received message {message.MessageID} from ChannelName... {message.ChannelName}.");
                return Task.CompletedTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}
