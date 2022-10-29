using System;

namespace SauaS.InterProcessNotification.Interfaces
{
    internal interface IConsumer<T> where T : new()
    {
        event EventHandler<T> NotifyDataChanged;
    }
}
