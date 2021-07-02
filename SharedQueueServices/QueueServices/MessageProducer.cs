using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using SharedQueueServices.Interfaces;
using SharedQueueServices.Models;

namespace SharedQueueServices.QueueServices
{
	public class MessageProducer : IMessageProducer
	{
        private readonly MessageProducerSettings _messageProducerSettings;
        private readonly IBasicProperties _properties;

		public MessageProducer(MessageProducerSettings messageProducerSettings)
		{
            _messageProducerSettings = messageProducerSettings;

            _properties = _messageProducerSettings.Channel.CreateBasicProperties();
			_properties.Persistent = true;
		}

		public Task Send(string message, string type = null)
		{

			return Task.Run(
				() =>
				{
					if (!string.IsNullOrEmpty(type))
					{
						_properties.Type = type;
					}

					var body = Encoding.UTF8.GetBytes(message);
                    _messageProducerSettings.Channel.BasicPublish(_messageProducerSettings.PublicationAddress, _properties, body);
				});
		}

		public Task SendTyped(Type type, string message)
		{
			return Send(message, type.AssemblyQualifiedName);
		}
	}
}