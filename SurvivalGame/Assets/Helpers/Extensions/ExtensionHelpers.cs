using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExtensionHelpers
{
    public static IEnumerator LerpRectTransformPositions(RectTransform rectTransform, Vector3 targetPosition, float speed)
    {
        //The time
        float time = Time.time;

        //Old localPosition of the RectTransform
        Vector3 rectOldLocalPosition = rectTransform.localPosition;

        //While loop 
        while (rectTransform.localPosition != targetPosition)
        {
            //Lerp from rectTransform to targetPosition with speed
            rectTransform.localPosition = Vector3.Lerp(rectOldLocalPosition, targetPosition, (Time.time - time) * speed);

            yield return null;
        }

        yield return new WaitForSeconds(3f);
    }

    public static IEnumerator LerpTransformPositions(Transform currentTransform, Vector3 targetPosition, float speed)
    {
        if (currentTransform == null) yield return null;
        //The Time
        float time = Time.time;

        //old position
        Vector3 transformOldPosition = currentTransform.position;

        //While loop
        while (currentTransform.position != targetPosition)
        {
            try
            {
                if (currentTransform != null)
                    currentTransform.position = Vector3.Lerp(transformOldPosition, targetPosition, (Time.time - time) * speed);
                else
                    break;
            }
            catch(MissingReferenceException)
            {
                break;
            }

            yield return new WaitForEndOfFrame(); //adjustment

        }

        yield return new WaitForEndOfFrame();
    }

    public static IEnumerator LerpColor(Image currentRenderer, Color targetColor, float speed)
    {
        if (currentRenderer == null) yield return null;

        //The Time
        float time = Time.time;

        //old position
        Color rendererOldColor = currentRenderer.color;

        //While loop
        while (currentRenderer.color != targetColor)
        {
            try
            {
                if (currentRenderer != null)
                    currentRenderer.color = Color.Lerp(rendererOldColor, targetColor, (Time.time - time) * speed);
                else
                    break;
            } catch (MissingReferenceException)
            {
                break;
            }

            yield return new WaitForEndOfFrame(); //adjustment

        }

        yield return new WaitForEndOfFrame();
    }

    public static IEnumerator LerpColor(TextMeshProUGUI currentRenderer, Color targetColor, float speed)
    {
        if (currentRenderer == null) yield return null;

        //The Time
        float time = Time.time;

        //old position
        Color rendererOldColor = currentRenderer.color;

        //While loop
        while (currentRenderer.color != targetColor)
        {
            try
            {
                if (currentRenderer != null)
                    currentRenderer.color = Color.Lerp(rendererOldColor, targetColor, (Time.time - time) * speed);
                else
                    break;
            } catch (MissingReferenceException)
            {
                break;
            }

            yield return new WaitForEndOfFrame(); //adjustment

        }

        yield return new WaitForEndOfFrame();
    }
}
