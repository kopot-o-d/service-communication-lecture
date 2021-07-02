using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedQueueServices.Models
{
    public class MessageScopeSettings
    {
        public string ExchangeName { get; set; }

        public string QueueName { get; set; }

        public string RoutingKey { get; set; }

        /// <summary>
        /// Use ExchangeType fields from RabbitMQ.Client
        /// </summary>
        public string ExchangeType { get; set; }
    }
}
