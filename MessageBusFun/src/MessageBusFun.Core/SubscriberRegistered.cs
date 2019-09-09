using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusFun.Core
{
    public class SubscriberRegistered : IEvent
    {
        public string SubscriberID { get; set; }
        public string ChannelName { get; set; }
    }
}
