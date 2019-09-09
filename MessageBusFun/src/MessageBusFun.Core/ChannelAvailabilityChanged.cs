using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusFun.Core
{
    public class ChannelAvailabilityChanged : IEvent
    {
        public string ChannelName { get; set; }
        public bool isAvailable { get; set; }
    }
}
