using System.Collections.Generic;
using UnityEngine;

namespace Extensions.Generics.ItemPool
{
    /// <summary>
    /// Please save me as IItemPool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericPool<T> : IItemPool<T> where T : MonoBehaviour
    {
        private readonly T prefab;
        private List<T> poolList;

        /// <summary>
        /// GenericPool constructor
        /// </summary>
        /// <param name="prefab">Object of the item this pool will pool</param>
        public GenericPool(T prefab)
        {
            this.prefab = prefab;

            poolList = new List<T>();
            poolList.Add(prefab);
        }

        /// <summary>
        /// Gets you the pool item
        /// </summary>
        /// <returns>Item that has been pooled</returns>
        public T Get()
        {
            for (int i = 0; i < poolList.Count; i++)
            {
                if (!poolList[i].enabled)
                {
                    poolList[i].enabled = true;
                    return poolList[i];
                }
            }

            return CreateNew();
        }

        private T CreateNew()
        {
            T obj = Object.Instantiate(prefab);
            obj.enabled = false;
            poolList.Add(obj);
            return obj;
        }
    }

    public interface IItemPool<T>
    {
        /// <summary>
        /// Gets you the pool item
        /// </summary>
        /// <returns>Item that has been pooled</returns>
        T Get();
    }
}
