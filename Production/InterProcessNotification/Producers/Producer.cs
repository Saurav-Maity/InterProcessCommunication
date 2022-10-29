using SauaS.Assemblies.InterProcessNotification.Classes;
using SauaS.Assemblies.InterProcessNotification.Interfaces;

namespace SauaS.Assemblies.InterProcessNotification.Producers
{
    public class Producer<T> : BaseConsumerProducer<T>, IProducer<T> where T : new()
    {
        public Producer(string uniqueSystemWideName) : base(uniqueSystemWideName)
        {           
        }

        public void Update(T data)
        {
            consumerReadyHandle.WaitOne();
            WriteToMemoryMappedFile(data);
            dataChangedHandle.Set();
        }
    }
}
