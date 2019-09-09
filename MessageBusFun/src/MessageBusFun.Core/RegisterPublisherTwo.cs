using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusFun.Core
{
    public class RegisterPublisherTwo : ICommand
    {
        public string PublisherID { get; set; }
    } 

    
}
