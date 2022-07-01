using UnityEngine;

namespace Five
{
    public class PrefabHelper
    {
        //public static GameObject Instantiate(string path)
        //{
        //    return Object.Instantiate(Find(path));
        //}
        public static GameObject Find(string path)
        {
            return Find<GameObject>(path);
        }
        public static T Instantiate<T>(string path)where T:Object
        {
            return Object.Instantiate(Find<T>(path));
        }
        public static T Instantiate<T>(string path,Transform parent) where T : Object
        {
            return Object.Instantiate(Find<T>(path),parent);
        }
        public static T Find<T>(string path)where T:Object
        {
            var prefab = Resources.Load<T>(path);
            if (!prefab)
                throw new PrefabNotFoundException($"Prefab '{path}' with type <{typeof(T)}> not found ");
            return prefab;
        }
    }
}