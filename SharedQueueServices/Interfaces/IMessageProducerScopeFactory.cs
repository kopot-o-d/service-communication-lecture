using SharedQueueServices.Models;

namespace SharedQueueServices.Interfaces
{
	public interface IMessageProducerScopeFactory
	{
		IMessageProducerScope Open(MessageScopeSettings messageScopeSettings);
	}
}