using SauaS.InterProcessNotification.Classes;
using SauaS.InterProcessNotification.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SauaS.InterProcessNotification.Consumers
{
    public class Consumer<T> : BaseConsumerProducer<T>, IConsumer<T> where T : new()
    {      
        public Consumer(string uniqueDataId) : base (uniqueDataId)
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

                    T data = DeserializeFromMemoryMappedFile();
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
