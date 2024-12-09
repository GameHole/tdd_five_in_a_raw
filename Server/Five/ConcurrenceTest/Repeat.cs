using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrenceTest
{
    public static class Repeat
    {
        public static async Task RepeatAsync(int count, Action<int> action)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < count; i++)
            {
                int idx = i;
                var task = Task.Factory.StartNew(()=>action(idx));
                tasks.Add(task);
            }
            await Task.WhenAll(tasks.ToArray());
        }
        public static async Task RepeatAsync<T>(List<T> list, Action<T> action)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var task = Task.Factory.StartNew(()=>
                {
                    action(item);
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks.ToArray());
        }
    }
}
