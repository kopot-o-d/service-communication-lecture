using System;
using System.Threading.Tasks;

namespace SharedQueueServices.Interfaces
{
	public interface IMessageProducer
	{
		Task Send(string message, string type = null);

		Task SendTyped(Type type, string message);
    }
}