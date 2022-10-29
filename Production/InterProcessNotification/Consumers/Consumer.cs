using System;
using System.Threading.Tasks;
using SauaS.Assemblies.InterProcessNotification.Classes;
using SauaS.Assemblies.InterProcessNotification.Interfaces;

namespace SauaS.Assemblies.InterProcessNotification.Consumers
{
    public class Consumer<T> : BaseConsumerProducer<T>, IConsumer<T> where T : new()
    {      
        public Consumer(string uniqueSystemWideName) : base (uniqueSystemWideName)
        {
            CheckDataChangedAsync();
        }

        public event EventHandler<T> NotifyDataChanged;

        private Task CheckDataChangedAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                while(!IsDisposed)
                {
                    consumerReadyHandle.Set();
                    dataChangedHandle.WaitOne();

                    T data = ReadFromMemoryMappedFile();
                    OnNotifyChange(data);
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void OnNotifyChange(T data)
        {
            NotifyDataChanged?.Invoke(this, data);
        }        
    }
}
