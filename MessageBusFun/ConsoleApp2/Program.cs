using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherTwo
{
    class Program
    {

        public static bool isSubscriberRegistered = false;
        public static bool isChannelTwoRegistered = false;

        public static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }


        public static async Task AsyncMain()
        {

            Console.Title = "PublisherTwo";

            var endpointConfiguration = new EndpointConfiguration("PublisherTwo");

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
            Console.WriteLine("Press '1' to publish the SendMessage event");
            Console.WriteLine("Press any other key to exit");

            var key = Console.ReadKey();
            Console.WriteLine();

            while (true)
            {
                var orderReceivedId = Guid.NewGuid();
                if (key.Key == ConsoleKey.D1)
                {
                    var messageSend = new MessageBusFun.Core.SendMessageSubscribed
                    {
                        MessageID = orderReceivedId,
                        ChannelName = "ChannelTwo"
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
    }


    }
    class SubscriberRegisteredHandler : IHandleMessages<MessageBusFun.Core.SubscriberRegistered>
    {
        static ILog log = LogManager.GetLogger<SubscriberRegisteredHandler>();
        public Task Handle(MessageBusFun.Core.SubscriberRegistered message, IMessageHandlerContext context)
        {
            Program.isSubscriberRegistered = true;
            if (message.ChannelName == "ChannelTwo" && PublisherTwo.Program.isChannelTwoRegistered && PublisherTwo.Program.isSubscriberRegistered)
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
