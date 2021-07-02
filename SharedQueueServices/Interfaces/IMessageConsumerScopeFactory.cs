using SharedQueueServices.Models;

namespace SharedQueueServices.Interfaces
{
	public interface IMessageConsumerScopeFactory
	{
		IMessageConsumerScope Open(MessageScopeSettings messageScopeSettings);

		IMessageConsumerScope Connect(MessageScopeSettings messageScopeSettings);
    }
}