﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class MessageContainer<T>
    {
        protected Dictionary<int, T> container = new Dictionary<int, T>();
        public bool Contains(int code)
        {
            return container.ContainsKey(code);
        }
        public void Add(int code, T value)
        {
            container.Add(code, value);
        }

        public bool TryGetValue(int code, out T item)
        {
            return container.TryGetValue(code, out item);
        }

        public void Clear()
        {
            container.Clear();
        }
    }
}
