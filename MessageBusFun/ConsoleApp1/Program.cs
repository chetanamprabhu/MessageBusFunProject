using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {

        // Create a list of strings for Available Channels.
        public static List<string> avalableChannels= new List<string>();
      
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }


       static async Task AsyncMain()
       {
            Console.Title = "Consumer";

            var endpointConfiguration = new EndpointConfiguration("Consumer");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.RegisterPublisher), "PublisherOne");
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.RegisterSubscriber), "Subscriber");
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.RegisterPublisherTwo), "PublisherTwo");
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.DeregisterPublisher), "PublisherOne");
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.DeregisterPublisherTwo), "PublisherTwo");
            routing.RouteToEndpoint(typeof(MessageBusFun.Core.DeregisterSubscriber), "Subscriber");
          
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

            await RunLoop(endpointInstance)
                .ConfigureAwait(false);

            await endpointInstance.Stop()
           .ConfigureAwait(false);
        }
        static ILog log = LogManager.GetLogger<Program>();

        public static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'R' to Register, 'D' to Deregister, 'L' to View list of available channels or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.R:
                        log.Info("Press 'P' to Register a Publisher, 'S' to Register a Subscriber");
                        var selectedoption = Console.ReadKey();
                        Console.WriteLine();
                        if (selectedoption.Key == ConsoleKey.P)
                        {
                            log.Info("Press 'F' to Register PublsiherOne and 'S' to Register PublisherTwo");
                            var SelectedPub = Console.ReadKey();
;
                            Console.WriteLine();
                            if (SelectedPub.Key == ConsoleKey.F)
                            {
                                // Instantiate the command
                                var command = new MessageBusFun.Core.RegisterPublisher
                                {
                                    PublisherID = Guid.NewGuid().ToString()
                                };

                                // Send the command to the local endpoint
                                log.Info($"Requesting registration, PublisherID = {command.PublisherID}");
                                await endpointInstance.Send(command)
                                    .ConfigureAwait(false);
                            }
                            else if(SelectedPub.Key == ConsoleKey.S)
                            {
                                // Instantiate the command
                                var command = new MessageBusFun.Core.RegisterPublisherTwo
                                {
                                    PublisherID = Guid.NewGuid().ToString()
                                };

                                // Send the command to the local endpoint
                                log.Info($"Requesting registration, PublisherID = {command.PublisherID}");
                                await endpointInstance.Send(command)
                                    .ConfigureAwait(false);

                            }
                            else
                            {
                                log.Info("Unknown input. Please try again.");
                            }

                        }
                        else if (selectedoption.Key == ConsoleKey.S)
                        {
                            if (avalableChannels != null && avalableChannels.Count > 0)
                            {
                                log.Info("Subscriber will be subscribed to the below available channels...");
                                foreach (var channels in avalableChannels)
                                {
                                    Console.WriteLine("ChannelName: " + channels);
                                }
                                log.Info("Available channels count..." + avalableChannels.Count);

                                if(avalableChannels.Count == 1)
                                {
                                    var command = new MessageBusFun.Core.RegisterSubscriber
                                    {
                                        SubscriberID = Guid.NewGuid().ToString(),
                                        ChannelName = avalableChannels[0].ToString()
                                    };
                                    // Send the command to the local endpoint
                                    log.Info($"Requesting registration, SubscriberID = {command.SubscriberID}, ChannelName = {command.ChannelName}");
                                    await endpointInstance.Send(command)
                                        .ConfigureAwait(false);
                                }
                                else if(avalableChannels.Count == 2)
                                {
                                    //var command = new MessageBusFun.Core.RegisterSubscriber
                                    //{
                                    //    SubscriberID = Guid.NewGuid().ToString(),
                                    //    ChannelName = avalableChannels[0].ToString()
                                    //};
                                    //// Send the command to the local endpoint
                                    //log.Info($"Requesting registration, SubscriberID = {command.SubscriberID}, ChannelName = {command.ChannelName}");
                                    //await endpointInstance.Send(command)
                                    //    .ConfigureAwait(false);

                                    var commandTwo = new MessageBusFun.Core.RegisterSubscriber
                                    {
                                        SubscriberID = Guid.NewGuid().ToString(),
                                        ChannelName = avalableChannels[1].ToString()
                                    };
                                    // Send the command to the local endpoint
                                    log.Info($"Requesting registration, SubscriberID = {commandTwo.SubscriberID}, ChannelName = {commandTwo.ChannelName}");
                                    await endpointInstance.Send(commandTwo)
                                        .ConfigureAwait(false);

                                }

                             
                            }
                            else
                            {
                                Console.WriteLine("Sorry no available channels to subscribe to....");
                                Console.WriteLine("Please register to a Publisher first");
                                break;
                            }                         
                        }
                        else
                            log.Info("Unknown input. Please try again.");
                        break;

                    case ConsoleKey.D:
                        log.Info("Press 'P' to Deregister a Publisher, 'S' to Deregister a Subscriber");
                        var SelectedDeRegOption = Console.ReadKey();
                        Console.WriteLine();
                        if (SelectedDeRegOption.Key == ConsoleKey.P)
                        {
                            log.Info("Press 'F' to Deregister PublsiherOne and 'S' to Deegister PublisherTwo");
                            var SelectedDePub = Console.ReadKey();
;
                            Console.WriteLine();
                            if (SelectedDePub.Key == ConsoleKey.F)
                            {
                                // Instantiate the command
                                var command = new MessageBusFun.Core.DeregisterPublisher
                                {
                                    PublisherName = "PublisherOne"
                                };

                                // Send the command to the local endpoint
                                log.Info($"Requesting deregistration of Publiser.... {command.PublisherName}");
                                await endpointInstance.Send(command)
                                    .ConfigureAwait(false);
                            }
                            else if (SelectedDePub.Key == ConsoleKey.S)
                            {
                                // Instantiate the command
                                var command = new MessageBusFun.Core.DeregisterPublisherTwo
                                {
                                    PublisherName ="PublisherTwo"
                                };

                                // Send the command to the local endpoint
                                log.Info($"Requesting deregistration of Publisher...  {command.PublisherName}");
                                await endpointInstance.Send(command)
                                    .ConfigureAwait(false);

                            }
                            else
                            {
                                log.Info("Unknown input. Please try again.");
                            }


                        }
                        else if (SelectedDeRegOption.Key == ConsoleKey.S)
                        {
                            // TODO: Nominate a Channel
                            // Instantiate the command
                            var command = new MessageBusFun.Core.DeregisterSubscriber
                            {
                                DeregistrationID = Guid.NewGuid().ToString()
                            };

                            // Send the command to the local endpoint
                            log.Info($"Requesting deregistration of Subscriber, RequestID = {command.DeregistrationID}");
                            await endpointInstance.Send(command)
                                .ConfigureAwait(false);
                        }
                        else
                            log.Info("Unknown input. Please try again.");
                        break;

                    case ConsoleKey.L:
                        log.Info("Please find below the list of available channels....");
                        // Iterate through the list.
                        if (avalableChannels != null && avalableChannels.Count > 0)
                        {
                            foreach (var channels in avalableChannels)
                            {
                                Console.WriteLine("ChannelName: " + channels);
                            }
                        }
                        else
                            Console.Write("Sorry no available channels to display....");
                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
    
}
