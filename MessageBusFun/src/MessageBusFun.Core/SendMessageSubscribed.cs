using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusFun.Core
{
    public class SendMessageSubscribed: IEvent
    {
        public Guid MessageID { get; set; }
        public string ChannelName { get; set; }
    }
}
