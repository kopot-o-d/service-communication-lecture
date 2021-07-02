using System;
using RabbitMQ.Client;

namespace SharedQueueServices.Interfaces
{
	public interface IMessageQueue : IDisposable
	{
		IModel Channel { get; }

		void DeclareExchange(string exchangeName, string exchangeType);

		void BindQueue(string exchangeName, string queueName, string routingKey);
	}
}