using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class NetList<T>: IEnumerable<T> where T:class
    {
        private ConcurrentDictionary<T, T> clients = new ConcurrentDictionary<T, T>();
        public Action<T> onAdd;
        public int Count { get => clients.Count; }
        public void Remove(T item)
        {
            clients.TryRemove(item, out var c);
        }
        public void Add(T item)
        {
            clients.TryAdd(item, item);
            onAdd?.Invoke(item);
        }
        public void Clear()
        {
            clients.Clear();
        }
        public bool Contains(T item)
        {
            return clients.ContainsKey(item);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return clients.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
