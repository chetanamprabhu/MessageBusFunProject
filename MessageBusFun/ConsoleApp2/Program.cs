using NServiceBus;
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

        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }


        static async Task AsyncMain()
        {
            Console.Title = "PublisherTwo";

            var endpointConfiguration = new EndpointConfiguration("PublisherTwo");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            await endpointInstance.Stop()
           .ConfigureAwait(false);
        }
    }
}
