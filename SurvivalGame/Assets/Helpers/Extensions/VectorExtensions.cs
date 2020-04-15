using UnityEngine;

namespace Extensions
{

    public static class VectorExtensions
    {
        /// <summary>
        /// Return the normalized direction from this Vector3 to lookAt
        /// </summary>
        /// <returns>Normalized direction</returns>
        public static Vector3 GetDirectionTo(this Vector3 from, Vector3 lookAt)
        {
            return (lookAt - from).normalized;
        }

        /// <summary>
        /// Returns Vector3(x, 0, y) from Vector2(x, y)
        /// </summary>
        /// <param name="vec"></param>
        /// <returns>Vector3(x, 0, y)</returns>
        public static Vector3 ToVector3XZ(this Vector2 vec)
        {
            return new Vector3(vec.x, 0, vec.y);
        }
    }
}
