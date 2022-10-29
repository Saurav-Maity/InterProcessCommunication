using System;
using System.Diagnostics;
using System.Threading;

namespace SauaS.InterProcessNotification.Classes
{
    public class BaseConsumerProducer<T> : IDisposable
    {
        protected string uniqueDataId;
        protected EventWaitHandle dataChangedHandle;
        protected EventWaitHandle consumerReadyHandle;

        protected BaseConsumerProducer(string uniqueDataId)
        {
            this.uniqueDataId = uniqueDataId;
            int sessionId = Process.GetCurrentProcess().SessionId;

            dataChangedHandle = new EventWaitHandle(false, EventResetMode.AutoReset, $"{(sessionId == 0 ? $@"Global\{uniqueDataId}" : uniqueDataId)}");
            consumerReadyHandle = new EventWaitHandle(false, EventResetMode.AutoReset, $"{(sessionId == 0 ? $@"Global\{uniqueDataId}" : uniqueDataId)}-ConsumerReady");
        }

        protected bool IsDisposed { get; private set; }

        protected T DeserializeFromMemoryMappedFile()
        {
            return default(T);
        }

        protected bool SerializeToMemoryMappedFile(T data)
        {
            bool result = false;

            return result;
        }

        public void Dispose()
        {
            dataChangedHandle?.Dispose();
            consumerReadyHandle?.Dispose();
            IsDisposed = true;
        }
    }
}
