using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public abstract class AClient:INotifier
    {
        public int Id { get; internal set; }
        public event Action onClose;
        public abstract void Send(Message message);
        public virtual void Close()
        {
            onClose?.Invoke();
        }
    }
}
