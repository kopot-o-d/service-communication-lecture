using System;
using RabbitMQ.Client.Events;

namespace SharedQueueServices.Interfaces
{
	public interface IMessageConsumer
	{
		event EventHandler<BasicDeliverEventArgs> Received;

		void SetAcknowledge(ulong deliveryTag, bool processed);

		void Connect();
	}
}