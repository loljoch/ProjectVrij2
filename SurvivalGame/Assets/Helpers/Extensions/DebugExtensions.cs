using UnityEngine;

namespace Extensions
{

    public static class DebugExtensions
    {
        public static float arrowDistance = 0.1f;

        /// <summary>
        /// Draws a box from origin extending to origin + halfExtents
        /// </summary>
        /// <param name="center">The center where the box will be drawn around</param>
        /// <param name="halfExtents">Half the maximum width of the box</param>
        /// <param name="orientation">The rotation of the box</param>
        /// <param name="color">Color of the drawn box</param>
        /// <param name="duration">How long the box will be visible(in seconds)</param>
        public static void DrawBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color, float duration)
        {
            Box box = new Box(center, halfExtents, orientation);

            Debug.DrawLine(box.frontTopLeft, box.frontTopRight, color, duration);
            Debug.DrawLine(box.frontTopRight, box.frontBottomRight, color, duration);
            Debug.DrawLine(box.frontBottomRight, box.frontBottomLeft, color, duration);
            Debug.DrawLine(box.frontBottomLeft, box.frontTopLeft, color, duration);

            Debug.DrawLine(box.backTopLeft, box.backTopRight, color, duration);
            Debug.DrawLine(box.backTopRight, box.backBottomRight, color, duration);
            Debug.DrawLine(box.backBottomRight, box.backBottomLeft, color, duration);
            Debug.DrawLine(box.backBottomLeft, box.backTopLeft, color, duration);

            Debug.DrawLine(box.frontTopLeft, box.backTopLeft, color, duration);
            Debug.DrawLine(box.frontTopRight, box.backTopRight, color, duration);
            Debug.DrawLine(box.frontBottomRight, box.backBottomRight, color, duration);
            Debug.DrawLine(box.frontBottomLeft, box.backBottomLeft, color, duration);
        }

        /// <summary>
        /// Draws an arrow from start to start + dir in world coordinates
        /// </summary>
        /// <param name="start">Point in world space where the arrow should start</param>
        /// <param name="dir">Direction and length of arrow</param>
        /// <param name="color">Color of drawn arrow</param>
        /// <param name="duration">How long the arrow will be visible(in seconds)</param>
        public static void DrawArrow(Vector3 start, Vector3 dir, Color color, float duration)
        {
            Vector3 rayEnd = start + dir;
            Vector3 arrowHeight = rayEnd - dir.normalized * 0.2f;

            //main ray
            Debug.DrawRay(start, dir, color, duration);

            //4 arrows
            Debug.DrawLine(rayEnd, arrowHeight + Vector3.right * arrowDistance, color, duration);
            Debug.DrawLine(rayEnd, arrowHeight + Vector3.left * arrowDistance, color, duration);
            Debug.DrawLine(rayEnd, arrowHeight + Vector3.forward * arrowDistance, color, duration);
            Debug.DrawLine(rayEnd, arrowHeight + Vector3.back * arrowDistance, color, duration);

            //connection between arrows
            Debug.DrawLine(arrowHeight + Vector3.right * arrowDistance, arrowHeight + Vector3.left * arrowDistance, color, duration);
            Debug.DrawLine(arrowHeight + Vector3.forward * arrowDistance, arrowHeight + Vector3.back * arrowDistance, color, duration);
        }

        private struct Box
        {
            public Vector3 localFrontTopLeft { get; private set; }
            public Vector3 localFrontTopRight { get; private set; }
            public Vector3 localFrontBottomLeft { get; private set; }
            public Vector3 localFrontBottomRight { get; private set; }
            public Vector3 localBackTopLeft { get { return -localFrontBottomRight; } }
            public Vector3 localBackTopRight { get { return -localFrontBottomLeft; } }
            public Vector3 localBackBottomLeft { get { return -localFrontTopRight; } }
            public Vector3 localBackBottomRight { get { return -localFrontTopLeft; } }

            public Vector3 frontTopLeft { get { return localFrontTopLeft + origin; } }
            public Vector3 frontTopRight { get { return localFrontTopRight + origin; } }
            public Vector3 frontBottomLeft { get { return localFrontBottomLeft + origin; } }
            public Vector3 frontBottomRight { get { return localFrontBottomRight + origin; } }
            public Vector3 backTopLeft { get { return localBackTopLeft + origin; } }
            public Vector3 backTopRight { get { return localBackTopRight + origin; } }
            public Vector3 backBottomLeft { get { return localBackBottomLeft + origin; } }
            public Vector3 backBottomRight { get { return localBackBottomRight + origin; } }

            public Vector3 origin { get; private set; }

            public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents)
            {
                Rotate(orientation);
            }
            public Box(Vector3 origin, Vector3 halfExtents)
            {
                this.localFrontTopLeft = new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
                this.localFrontTopRight = new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
                this.localFrontBottomLeft = new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
                this.localFrontBottomRight = new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);

                this.origin = origin;
            }


            public void Rotate(Quaternion orientation)
            {
                localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.zero, orientation);
                localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.zero, orientation);
                localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.zero, orientation);
                localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.zero, orientation);
            }
        }

        private static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
        {
            Vector3 direction = point - pivot;
            return pivot + rotation * direction;
        }
    }
}
