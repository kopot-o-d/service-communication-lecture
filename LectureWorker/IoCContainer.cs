using System.Runtime.InteropServices.ComTypes;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SharedQueueServices.Interfaces;
using SharedQueueServices.QueueServices;
using ConnectionFactory = SharedQueueServices.QueueServices.ConnectionFactory;

namespace LectureWorker
{
	public class IoCContainer
	{
		private readonly ServiceProvider ServiceProvider;
		private readonly IServiceCollection ServiceCollection;

		public IoCContainer()
		{
            ServiceCollection = new ServiceCollection()
            .AddTransient<MessageService>()
            .AddTransient<MessageServiceBeta>()
            .AddTransient<IMessageQueue, MessageQueue>()
            .AddSingleton<IConnectionFactory, ConnectionFactory>()

            .AddTransient<IMessageProducer, MessageProducer>()
            .AddTransient<IMessageProducerScope, MessageProducerScope>()
            .AddSingleton<IMessageProducerScopeFactory, MessageProducerScopeFactory>()

            .AddTransient<IMessageConsumer, MessageConsumer>()
            .AddTransient<IMessageConsumerScope, MessageConsumerScope>()
            .AddSingleton<IMessageConsumerScopeFactory, MessageConsumerScopeFactory>();

            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

		public T GetService<T>()
		{
			return ServiceProvider.GetService<T>();
		}
    }
}