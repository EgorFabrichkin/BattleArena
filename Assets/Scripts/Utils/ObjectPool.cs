using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ObjectPool<T> where T : Behaviour
    {
        private readonly T prefab;
        private readonly int count;
        private readonly List<T> list;

        public ObjectPool(T prefab, int count)
        {
            this.prefab = prefab;
            this.count = count;
            list = new List<T>(count);

            for (var i = 0; i < count; i++)
            {
                ReturnToPool(Object.Instantiate(prefab));
            }
        }

        public T TryGetObject()
        {
            var index = list.Count - 1;
            T poolObject;

            if (index >= 0)
            {
                poolObject = list[index];
                list.RemoveAt(index);
            }
            else
            {
                poolObject = Object.Instantiate(prefab);
            }
            poolObject.gameObject.SetActive(true);

            return poolObject;
        }

        public void ReturnToPool(T poolObject)
        {
            poolObject.gameObject.SetActive(false);
            list.Add(poolObject);
        }
    }
}