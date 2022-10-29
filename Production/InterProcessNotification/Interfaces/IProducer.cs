namespace SauaS.Assemblies.InterProcessNotification.Interfaces
{
    public interface IProducer<T> where T : new()
    {
        void Update(T data);
    }
}
