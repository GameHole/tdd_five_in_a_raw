using System;
using System.Collections;
using System.Collections.Generic;

namespace Five
{
    public class Container:IEnumerable<object>
    {
        private Dictionary<Type, object> pairs = new Dictionary<Type, object>();
        public int Count { get => pairs.Count; }

        public bool Contain(Type type)
        {
            return pairs.ContainsKey(type);
        }
        
        public T Get<T>() where T : class
        {
            pairs.TryGetValue(typeof(T), out object item);
            return item as T;
        }

        public IEnumerator<object> GetEnumerator()
        {
            return pairs.Values.GetEnumerator();
        }

        public T Set<T>(T v) where T : class
        {
            pairs[typeof(T)] = v;
            return v;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
