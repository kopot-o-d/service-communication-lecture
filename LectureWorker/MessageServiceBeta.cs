using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace LectureWorker
{
    public class MessageServiceBeta
    {
        public void Run()
        {
            Console.WriteLine("Service Beta run");
            Configure();            
        }

        private void Configure()
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5673") };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("BetaExchange", ExchangeType.Direct);

                channel.QueueDeclare(queue: "BetaQueue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false);

                channel.QueueBind("BetaQueue", "BetaExchange", "key");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += MessageReceived;

                channel.BasicConsume(queue: "BetaQueue",
                                     autoAck: true,
                                     consumer: consumer);                
            }
        }

        private void MessageReceived(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body;
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received {message}");
        }
    }
}
