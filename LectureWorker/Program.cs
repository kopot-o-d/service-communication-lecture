using System;
using SharedQueueServices.Interfaces;

namespace LectureWorker
{
    internal class Program
    {
        private static void Main(string[] args)
		{
            var iocContainer = new IoCContainer();
            var messageService = iocContainer.GetService<MessageService>();
            var messageServiceBeta = iocContainer.GetService<MessageServiceBeta>();

            messageService.Run();
            messageServiceBeta.Run();

            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
            messageService.Dispose();
        }
    }
}
