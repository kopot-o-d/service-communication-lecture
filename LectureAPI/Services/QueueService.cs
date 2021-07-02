using System;
using System.Text;
using System.Threading.Tasks;
using LectureAPI.Hubs;
using LectureAPI.Interfaces;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedQueueServices.Interfaces;
using SharedQueueServices.Models;

namespace LectureAPI.Services
{
	public class QueueService : IQueueService
	{

		private readonly IMessageProducerScope _messageProducerScope;
		private readonly IMessageConsumerScope _messageConsumerScope;

        private readonly IHubContext<LectureHub> _lectureHub;

        public QueueService(
            IMessageProducerScopeFactory messageProducerScopeFactory,
            IMessageConsumerScopeFactory messageConsumerScopeFactory,
            IHubContext<LectureHub> hubContext
            )
		{
            _lectureHub = hubContext;
                
            _messageProducerScope = messageProducerScopeFactory.Open(new MessageScopeSettings
            {
                ExchangeName = "ServerExchange",
                ExchangeType = ExchangeType.Topic,
                QueueName = "SendValueQueue",
                RoutingKey = "topic.queue"
            });

            _messageConsumerScope = messageConsumerScopeFactory.Connect(new MessageScopeSettings
            {
                ExchangeName = "ClientExchange",
                ExchangeType = ExchangeType.Direct,
                QueueName = "SendResponseQueue",
                RoutingKey = "response"                
            });

            _messageConsumerScope.MessageConsumer.Received += GetValue;
        }		

		public async Task<bool> PostValue(string value)
		{
			try
			{
				await _messageProducerScope.MessageProducer.Send(value);
				return true;
			}
			catch(Exception)
			{
				return false;
			}
			
		}

        private void GetValue(object sender, BasicDeliverEventArgs args)
        {
            var value = Encoding.UTF8.GetString(args.Body);

            _lectureHub.Clients.All.SendAsync("GetNotification", value);

            _messageConsumerScope.MessageConsumer.SetAcknowledge(args.DeliveryTag, true);
        }
    }
}