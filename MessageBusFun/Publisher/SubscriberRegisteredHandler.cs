using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherOne
{
    class SubscriberRegisteredHandler: IHandleMessages<MessageBusFun.Core.SubscriberRegistered>
    {
        static ILog log = LogManager.GetLogger<SubscriberRegisteredHandler>();
        public Task Handle(MessageBusFun.Core.SubscriberRegistered message, IMessageHandlerContext context)
        {
            PublisherOne.Program.isSubscriberRegistered = true;
            if (message.ChannelName == "ChannelOne" && PublisherOne.Program.isChannelOneRegistered && PublisherOne.Program.isSubscriberRegistered)
            {
                log.Info($"Thank you for registering to our Channel... message.ChannelName...");
                Console.WriteLine("Press '1' to send a message to the Subscriber");
                Console.WriteLine("Press any other key to exit");

                var key = Console.ReadKey();
                Console.WriteLine();

                var orderReceivedId = Guid.NewGuid();
                if (key.Key == ConsoleKey.D1)
                {
                    var orderReceived = new MessageBusFun.Core.SendMessageSubscribed
                    {
                        MessageID = orderReceivedId,
                        ChannelName = "ChannelOne"
                    };
                    //await context.Publish(orderReceived)
                    //    .ConfigureAwait(false);
                    Console.WriteLine($"Published SendMessage Event with Id {orderReceivedId}.");
                    return context.Publish(orderReceived);
                }
                else
                {
                    return Task.CompletedTask;
                }
            }
            else
            {
                return Task.CompletedTask;
            }

        }

        //public static async Task AsyncPublish(IMessageHandlerContext context)
        //{
        //     Console.WriteLine("Press '1' to send a message to the Subscriber");
        //     Console.WriteLine("Press any other key to exit");

        //        #region PublishLoop

        //      //  while (true)
        //       // {
        //            var key = Console.ReadKey();
        //            Console.WriteLine();

        //            var orderReceivedId = Guid.NewGuid();
        //            if (key.Key == ConsoleKey.D1)
        //            {
        //                var orderReceived = new MessageBusFun.Core.SendMessageSubscribed
        //                {
        //                    MessageID = orderReceivedId,
        //                    ChannelName = "ChannelOne"
        //                };
        //                await context.Publish(orderReceived)
        //                    .ConfigureAwait(false);
        //                Console.WriteLine($"Published OrderReceived Event with Id {orderReceivedId}.");
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }

        //        #endregion
        //    }
    
        
    }
}
