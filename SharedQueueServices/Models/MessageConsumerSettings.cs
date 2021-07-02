using RabbitMQ.Client;

namespace SharedQueueServices.QueueServices
{
	public class MessageConsumerSettings
	{
		/// <summary>
		/// Gets or sets a value indicating whether messages will be received one by one.
		/// </summary>
		public bool SequentialFetch { get; set; } = true;

		public bool AutoAcknowledge { get; set; } = false;

        public IModel Channel { get; set; }

        public string QueueName { get; set; }
	}
}