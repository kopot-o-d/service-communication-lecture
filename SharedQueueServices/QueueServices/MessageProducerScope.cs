﻿using System;
using RabbitMQ.Client;
using SharedQueueServices.Interfaces;
using SharedQueueServices.Models;

namespace SharedQueueServices.QueueServices
{
	public class MessageProducerScope : IMessageProducerScope
	{
        private readonly Lazy<IMessageQueue> _messageQueueLazy;
		private readonly Lazy<IMessageProducer> _messageProducerLazy;

		private readonly MessageScopeSettings _messageScopeSettings;
		private readonly IConnectionFactory _connectionFactory;

        public MessageProducerScope(IConnectionFactory connectionFactory, MessageScopeSettings messageScopeSettings)
		{
            _connectionFactory = connectionFactory;
            _messageScopeSettings = messageScopeSettings;

            _messageQueueLazy = new Lazy<IMessageQueue>(CreateMessageQueue);
			_messageProducerLazy = new Lazy<IMessageProducer>(CreateMessageProducer);
		}

		public IMessageProducer MessageProducer => _messageProducerLazy.Value;

		private IMessageQueue MessageQueue => _messageQueueLazy.Value;

		private IMessageQueue CreateMessageQueue()
		{
			return new MessageQueue(_connectionFactory, _messageScopeSettings);
		}

		private IMessageProducer CreateMessageProducer()
		{
			return new MessageProducer(new MessageProducerSettings
            {
                Channel = MessageQueue.Channel,
                PublicationAddress = new PublicationAddress(
                    _messageScopeSettings.ExchangeType,
                    _messageScopeSettings.ExchangeName,
                    _messageScopeSettings.RoutingKey)
            });
		}

		public void Dispose()
		{
            MessageQueue?.Dispose();
        }
    }
}