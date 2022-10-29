using System;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;

using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace SauaS.Assemblies.InterProcessNotification.Classes
{
    public class BaseConsumerProducer<T> : IDisposable
    {
        private MemoryMappedFile memoryMappedFile;
        protected EventWaitHandle dataChangedHandle;
        protected EventWaitHandle consumerReadyHandle;

        protected BaseConsumerProducer(string uniqueSystemWideName)
        {
            //Global is not required if running as Session > 0 (i.e. as Application not as Windows Service)
            string systemWideName = $"{(Process.GetCurrentProcess().SessionId == 0 ? $@"Global\{uniqueSystemWideName}" : uniqueSystemWideName)}";

            dataChangedHandle = new EventWaitHandle(false, EventResetMode.AutoReset, systemWideName);
            consumerReadyHandle = new EventWaitHandle(false, EventResetMode.AutoReset, $"{systemWideName}-ConsumerReady");
            memoryMappedFile = MemoryMappedFile.CreateOrOpen($"{systemWideName}-MemoryFile", short.MaxValue);
        }

        protected bool IsDisposed { get; private set; }

        protected T ReadFromMemoryMappedFile()
        {
            T data = default(T);
            using (MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream())
            {
                string dataAsString = string.Empty;
                BinaryReader binaryReader = new BinaryReader(memoryMappedViewStream);
                dataAsString = binaryReader.ReadString();

                data = JsonConvert.DeserializeObject<T>(dataAsString);
            }

            return data;
        }

        protected void WriteToMemoryMappedFile(T data)
        {
            using (MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryMappedViewStream);
                binaryWriter.Write(JsonConvert.SerializeObject(data));
            }
        }

        public void Dispose()
        {
            IsDisposed = true;
            dataChangedHandle?.Dispose();
            consumerReadyHandle?.Dispose();
            memoryMappedFile?.Dispose();            
        }
    }
}
