using SauaS.InterProcessNotification.Classes;
using SauaS.InterProcessNotification.Interfaces;

namespace SauaS.InterProcessNotification.Producers
{
    public class Producer<T> : BaseConsumerProducer<T>, IProducer<T> where T : new()
    {
        public Producer(string uniqueDataId) : base(uniqueDataId)
        {           
        }

        public void Update(T data)
        {
            consumerReadyHandle.WaitOne();
            SerializeToMemoryMappedFile(data);
            dataChangedHandle.Set();
        }
    }
}
