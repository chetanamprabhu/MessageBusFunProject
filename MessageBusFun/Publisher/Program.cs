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

        static void Main()
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

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            await endpointInstance.Stop()
           .ConfigureAwait(false);

        }

       
    }
}
