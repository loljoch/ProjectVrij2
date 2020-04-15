using UnityEngine;

namespace Extensions
{

    public static class TransformExtensions
    {
        /// <summary>
        /// Lerps rectTransform to target position
        /// </summary>
        /// <param name="rectTransform">Rect transform of the object</param>
        /// <param name="targetPosition">End position of the Lerp</param>
        /// <param name="time">Time in seconds</param>
        /// <param name="owner">Class to run the lerp on</param>
        public static RectTransform LerpRectTransform(this RectTransform rectTransform, Vector3 targetPosition, float time, MonoBehaviour owner)
        {
            if (rectTransform == null) return null;

            if (owner != null)
            {
                owner.StartCoroutine(ExtensionHelpers.LerpRectTransformPositions(rectTransform, targetPosition, time));
                return rectTransform;
            } else
            {
                Debug.Log("Our Owner is null");
                return null;
            }
        }

        /// <summary>
        /// Lerps world space position to targeted position within speed (seconds)
        /// </summary>
        /// <param name="currentTransform">Transform with the starting position</param>
        /// <param name="targetPosition">End position of the lerp</param>
        /// <param name="time">Time in seconds</param>
        /// <param name="owner">Class to run the lerp on</param>
        /// <returns></returns>
        public static Transform LerpPosition(this Transform currentTransform, Vector3 targetPosition, float time, MonoBehaviour owner)
        {
            if (currentTransform == null) return null;

            if (owner != null)
            {
                owner.StartCoroutine(ExtensionHelpers.LerpTransformPositions(currentTransform, targetPosition, time));
                return currentTransform;
            } else
            {
                Debug.Log("Our Owner is null");
                return null;
            }
        }
    }
}
