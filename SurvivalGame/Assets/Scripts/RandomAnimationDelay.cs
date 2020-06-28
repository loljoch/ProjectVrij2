using UnityEngine;

public class RandomAnimationDelay : MonoBehaviour
{
    float maxOffset = 8f;

    private void OnEnable()
    {
        var anim = GetComponent<Animator>();
        anim.SetFloat("Offset", Random.Range(0.0f, maxOffset));
    }
}
