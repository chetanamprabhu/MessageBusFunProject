using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusFun.Core
{
    public class DeregisterSubscriber : ICommand
    {
        public string DeregistrationID { get; set; }
       
    }
}
