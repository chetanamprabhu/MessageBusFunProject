using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber
{
    class Program
    {
        public static bool IsSubscriberRegistered { get; set; }
        public static bool IsChannelOneSubReg { get; set; }
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }


        static async Task AsyncMain()
        {
            Console.Title = "Subscriber";

            var endpointConfiguration = new EndpointConfiguration("Subscriber");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.SubscriberRegistered), "PublisherOne");
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.SubscriberRegistered), "Consumer");
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.SubscriberRegistered), "PublisherTwo");



            var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            await endpointInstance.Stop()
           .ConfigureAwait(false);
        }

    }
}
