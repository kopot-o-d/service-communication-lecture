using System;
using System.IO;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedQueueServices.Interfaces;
using SharedQueueServices.Models;

namespace LectureWorker
{
	public class MessageService : IDisposable
	{
		private readonly IMessageConsumerScope _messageConsumerScope;
		private readonly IMessageProducerScope _messageProducerScope;

        public MessageService(IMessageConsumerScopeFactory messageConsumerScopeFactory,
            IMessageProducerScopeFactory messageProducerScopeFactory)
		{
			_messageConsumerScope = messageConsumerScopeFactory.Connect(new MessageScopeSettings
            {
                ExchangeName = "ServerExchange",
                ExchangeType = ExchangeType.Topic,
                QueueName = "SendValueQueue",
                RoutingKey = "*.queue.#"
            });

			_messageConsumerScope.MessageConsumer.Received += MessageReceived;

            _messageProducerScope = messageProducerScopeFactory.Open(new MessageScopeSettings
            {
                ExchangeName = "ClientExchange",
                ExchangeType = ExchangeType.Direct,
                QueueName = "SendResponseQueue",
                RoutingKey = "response"
            });

        }

        public void Run()
        {
            Console.WriteLine("Service run");
        }

		private void MessageReceived(object sender, BasicDeliverEventArgs args)
		{
            var processed = false;
            try
            {
                var value = Encoding.UTF8.GetString(args.Body);
                File.WriteAllText("LectureLog.txt", value);
                Console.WriteLine($"Received {value}");
                SendSuccessfulState(value);
                processed = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                processed = false;
            }
            finally
            {
                _messageConsumerScope.MessageConsumer.SetAcknowledge(args.DeliveryTag, processed);
            }
        }

        private void SendSuccessfulState(string receivedValue)
        {
            _messageProducerScope.MessageProducer.Send($"Received '{receivedValue}'");
        }

        public void Dispose()
        {
            _messageConsumerScope.Dispose();
            _messageProducerScope.Dispose();
        }

    }
}