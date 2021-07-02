using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LectureAPI.Services
{
    public class QueueServiceBeta
    {
        public Task<bool> PostValue(string value)
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5673") };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("BetaExchange", ExchangeType.Direct);

                string message = value;
                var body = Encoding.UTF8.GetBytes(message);

                return Task.Run(() => 
                    {
                        channel.BasicPublish(
                            exchange: "Beta exchange",
                            routingKey: "key",
                            basicProperties: null,
                            body: body);

                        return true;
                    });
            }
        }
    }
}
