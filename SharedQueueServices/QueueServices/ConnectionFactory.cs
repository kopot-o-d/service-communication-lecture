using System;
using RabbitMQ.Client;

namespace SharedQueueServices.QueueServices
{
	public class ConnectionFactory : RabbitMQ.Client.ConnectionFactory
	{

		public ConnectionFactory()
		{
			Uri = new Uri("amqp://guest:guest@localhost:5673");
			RequestedConnectionTimeout = 30000;
			NetworkRecoveryInterval = TimeSpan.FromSeconds(30);
			AutomaticRecoveryEnabled = true;
			TopologyRecoveryEnabled = true;
			RequestedHeartbeat = 60;
		}

		public override IConnection CreateConnection()
		{
			// here we can configure our connection
			// var connection = base.CreateConnection();
			// connection.
			
			return base.CreateConnection();
		}
	}
}