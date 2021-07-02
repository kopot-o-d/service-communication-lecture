using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedQueueServices.Models
{
    public class MessageProducerSettings
    {
        public IModel Channel { get; set; }

        public PublicationAddress PublicationAddress { get; set; }
    }
}
