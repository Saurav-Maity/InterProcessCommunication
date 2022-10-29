using SauaS.Assemblies.InterProcessNotification.Consumers;
using SauaS.Assemblies.InterProcessNotification.Interfaces;
using SauaS.Assemblies.InterProcessNotification.Producers;
using SauaS.InterProcessNotification.ProducerProcess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterProcessNotification.ProducerProcess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IProducer<EventInfo> producer = new Producer<EventInfo>("408C3A80-FE97-4FD4-9E2F-AB5179C47332");

            foreach(EventInfo eventInfo in GetEventInfoList())
            {
                producer.Update(eventInfo);
            }

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }

        private static IList<EventInfo> GetEventInfoList()
        {
            return new List<EventInfo>()
            { 
                new EventInfo {EventId="1001", Computer="L808", Source = "Windows Error Reporting"},
                new EventInfo {EventId="4799", Computer="L808", Source = "Security-Auditing"},
                new EventInfo {EventId="1001", Computer="R807", Source = "Windows Error Reporting"},
                new EventInfo {EventId="4799", Computer="R807", Source = "Security-Auditing"}
            };
        }
    }
}
