using System.Runtime.Serialization;

namespace SauaS.InterProcessNotification.Interfaces
{
    internal interface IProducer<T> where T : new()
    {
        void Update(T data);
    }
}
