using System;

namespace SharedQueueServices.Interfaces
{
	public interface IMessageConsumerScope : IDisposable
	{
		IMessageConsumer MessageConsumer { get; }
    }
}