using UnityEngine;

namespace Extensions
{

    public static class GameObjectExtensions
    {
        /// <summary>
        /// Return true if object has typeof(Component)
        /// </summary>
        /// <typeparam name="T">typeof(Component)</typeparam>
        /// <param name="obj">Object toe search from</param>
        /// <returns></returns>
        public static bool HasComponent<T>(this Component obj)
        {
            return obj.GetComponent(typeof(T)) != null;
        }

        /// <summary>
        /// Return true if object has typeof(Component)
        /// </summary>
        /// <typeparam name="T">typeof(Component)</typeparam>
        /// <param name="obj">Object toe search from</param>
        /// <returns></returns>
        public static bool HasComponent<T>(this GameObject obj)
        {
            return obj.GetComponent(typeof(T)) != null;
        }


        /// <summary>
        /// Destroys all children of object
        /// </summary>
        /// <param name="obj">Object</param>
        public static void DestroyChildren(this GameObject obj)
        {
            Transform[] children = obj.GetComponentsInChildren<Transform>();
            int cLength = children.Length;

            for (int i = 0; i < cLength; i++)
            {
                if (children[i] != obj.transform)
                {
                    Object.Destroy(children[i].gameObject);
                }
            }
        }
    }
}
