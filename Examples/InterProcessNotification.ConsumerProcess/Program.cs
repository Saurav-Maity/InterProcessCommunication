using System;

using SauaS.Assemblies.InterProcessNotification.Consumers;
using SauaS.Assemblies.InterProcessNotification.Interfaces;
using SauaS.InterProcessNotification.ProducerProcess.Model;

namespace InterProcessNotification.ConsumerProcess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConsumer<EventInfo> consumer = new Consumer<EventInfo>("408C3A80-FE97-4FD4-9E2F-AB5179C47332");
            consumer.NotifyDataChanged += (obj, eventInfo) =>
            {
                Console.WriteLine($"Received data : [{eventInfo}]");
            };

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
    }    
}
