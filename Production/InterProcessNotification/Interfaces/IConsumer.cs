using System;

namespace SauaS.Assemblies.InterProcessNotification.Interfaces
{
    public interface IConsumer<T> where T : new()
    {
        event EventHandler<T> NotifyDataChanged;
    }
}
