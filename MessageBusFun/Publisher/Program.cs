using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherOne
{
    class Program
    {

        public static bool isSubscriberRegistered = false;
        public static bool isChannelOneRegistered = false;

        public static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }


        public static async Task AsyncMain()
        {

            Console.Title = "PublisherOne";

            var endpointConfiguration = new EndpointConfiguration("PublisherOne");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.SendMessageSubscribed), "Subscriber");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

            await Start(endpointInstance)
          .ConfigureAwait(false);

            await endpointInstance.Stop()
           .ConfigureAwait(false);

        }

        static async Task Start(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                Console.WriteLine("Press '1' to publish the SendMessage event");
                Console.WriteLine("Press any other key to exit");

                #region PublishLoop


                var key = Console.ReadKey();
                Console.WriteLine();

                var orderReceivedId = Guid.NewGuid();
                if (key.Key == ConsoleKey.D1)
                {
                    var messageSend = new MessageBusFun.Core.SendMessageSubscribed
                    {
                        MessageID = orderReceivedId,
                        ChannelName = "ChannelOne"
                    };
                    await endpointInstance.Publish(messageSend)
                        .ConfigureAwait(false);
                    Console.WriteLine($"Published SendMessage Event with Id {orderReceivedId}.");
                }
                else
                {
                    return;
                }
            }
            #endregion          

        }


    }
    class SubscriberRegisteredHandler : IHandleMessages<MessageBusFun.Core.SubscriberRegistered>
    {
        static ILog log = LogManager.GetLogger<SubscriberRegisteredHandler>();
        public Task Handle(MessageBusFun.Core.SubscriberRegistered message, IMessageHandlerContext context)
        {
            Program.isSubscriberRegistered = true;
            if (message.ChannelName == "ChannelOne" && PublisherOne.Program.isChannelOneRegistered && PublisherOne.Program.isSubscriberRegistered)
            {
                Console.WriteLine($"Thank you for registering to our Channel... {message.ChannelName}...");
                Program.Main();
                return Task.CompletedTask;

            }
            else
            {
                return Task.CompletedTask;
            }
        }

    }
}
