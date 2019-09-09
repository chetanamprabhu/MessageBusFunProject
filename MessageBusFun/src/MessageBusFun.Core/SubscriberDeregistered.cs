using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusFun.Core
{
    public class SubscriberDeregistered : IEvent
    {
        public string SubscriberName { get; set; }
    }
}
