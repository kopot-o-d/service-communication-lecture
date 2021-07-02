using RabbitMQ.Client;
using SharedQueueServices.Interfaces;
using SharedQueueServices.Models;

namespace SharedQueueServices.QueueServices
{
	public class MessageProducerScopeFactory : IMessageProducerScopeFactory
	{
        private readonly IConnectionFactory _connectionFactory;

        public MessageProducerScopeFactory(IConnectionFactory connectionFactory)
		{
            _connectionFactory = connectionFactory;
		}

		public IMessageProducerScope Open(MessageScopeSettings messageScopeSettings)
		{
			return new MessageProducerScope(_connectionFactory, messageScopeSettings);
		}
    }
}