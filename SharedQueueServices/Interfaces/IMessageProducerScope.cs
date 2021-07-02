using System;

namespace SharedQueueServices.Interfaces
{
	public interface IMessageProducerScope : IDisposable
	{
		IMessageProducer MessageProducer { get; }
	}
}