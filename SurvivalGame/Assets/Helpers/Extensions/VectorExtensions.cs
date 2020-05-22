using UnityEngine;
using System.Collections.Generic;

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

        /// <summary>
        /// Returns the 8 neighbouring coordinates
        /// </summary>
        /// <param name="vec">Middle coordinate</param>
        /// <param name="maxSize">Max width and height of grid</param>
        /// <returns></returns>
        public static List<Vector2Int> GetOmniNeighbours(this Vector2Int vec, int maxSize)
        {
            return GetOmniNeighbours(vec, maxSize, maxSize);
        }

        /// <summary>
        /// Returns the 8 neighbouring coordinates
        /// </summary>
        /// <param name="vec">Middle coordinate</param>
        /// <param name="xSize">Max width of grid</param>
        /// <param name="ySize">Max height of grid</param>
        /// <returns></returns>
        public static List<Vector2Int> GetOmniNeighbours(this Vector2Int vec, int xSize, int ySize)
        {
            List<Vector2Int> n = new List<Vector2Int>();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    int cellX = vec.x + x;
                    int cellY = vec.y + y;
                    if (cellX < 0 || cellX >= xSize|| cellY < 0 || cellY >= ySize|| (Mathf.Abs(x) == Mathf.Abs(y) && x == 0))
                    {
                        continue;
                    }

                    Vector2Int coord = new Vector2Int(cellX, cellY);

                    n.Add(coord);
                    
                }
            }

            return n;
        }

        /// <summary>
        /// Returns the 4 neighbouring coordinates
        /// </summary>
        /// <param name="vec">Middle coordinate</param>
        /// <param name="maxSize">Max size of grid</param>
        /// <returns></returns>
        public static List<Vector2Int> GetDirectNeighbours(this Vector2Int vec, int maxSize)
        {
            return GetDirectNeighbours(vec, maxSize, maxSize);
        }

        /// <summary>
        /// Returns the 4 neighbouring coordinates
        /// </summary>
        /// <param name="vec">Middle coordinate</param>
        /// <param name="xSize">Max width of grid</param>
        /// <param name="ySize">Max width of grid</param>
        /// <returns></returns>
        public static List<Vector2Int> GetDirectNeighbours(this Vector2Int vec, int xSize, int ySize)
        {
            List<Vector2Int> n = new List<Vector2Int>();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    int cellX = vec.x + x;
                    int cellY = vec.y + y;
                    if (cellX < 0 || cellX >= xSize || cellY < 0 || cellY >= ySize || (Mathf.Abs(x) == Mathf.Abs(y)))
                    {
                        continue;
                    }

                    Vector2Int coord = new Vector2Int(cellX, cellY);

                    n.Add(coord);

                }
            }

            return n;
        }
    }
}
