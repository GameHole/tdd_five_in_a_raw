using System;
using System.Collections.Generic;

namespace Five
{
    public class GameFlow
    {
        abstract class AFlowList
        {
            public abstract void Add(object obj);
            public abstract bool Contain(Type type);
        }
        class FlowList<T>:AFlowList where T:class,IFlow
        {
            public List<T> list = new List<T>();
            public override void Add(object obj)
            {
                list.Add(obj as T);
            }
            public override bool Contain(Type type)
            {
                return list.FindIndex((item) => item.GetType() == type) >= 0;
            }
        }
        private Dictionary<Type, AFlowList> flowsDic = new Dictionary<Type, AFlowList>();
        public int Count { get => flowsDic.Count; }

        public bool Contain(Type type)
        {
            foreach (var item in flowsDic.Values)
            {
                if (item.Contain(type))
                    return true;
            }
            return false;
        }

        public GameFlow()
        {
            AddLists<IGameStart>();
            AddLists<IPlayerPlay>();
            AddLists<IPlayerTurn>();
            AddLists<IGameFinish>();
        }

        private void AddLists<T>() where T : class, IFlow 
        {
            flowsDic.Add(typeof(T), new FlowList<T>());
        }

        public List<T> GetFlowList<T>()where T:class,IFlow
        {
            flowsDic.TryGetValue(typeof(T), out var flows);
            return (flows as FlowList<T>).list;
        }
        public void AddFlow(IFlow obj)
        {
            foreach (var item in flowsDic)
            {
                if (item.Key.IsAssignableFrom(obj.GetType()))
                {
                    item.Value.Add(obj);
                }
            }
        }
    }
}
