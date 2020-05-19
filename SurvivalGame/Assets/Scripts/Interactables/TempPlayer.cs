using EasyAttributes;
using Extensions;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [Header("Interact variables")]
    [SerializeField] private Vector3 rayOrigin;
    [SerializeField] private Vector3 rayDirection;
    private IInteractable currentInteractable;

    [Button]
    private void ShowRayOrigin()
    {
        DebugExtensions.DrawBox(transform.position + rayOrigin, Vector3.one * 0.05f, Quaternion.identity, Color.red, 2f);
    }

    [Button]
    private void ShowRay()
    {
        DebugExtensions.DrawArrow(transform.position + rayOrigin, transform.forward + rayDirection, Color.red, 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentInteractable?.Interact();
        }        
    }


    private void FixedUpdate()
    {
        CastRay();
    }

    private void CastRay()
    {
        if (Physics.Raycast(GetRay(), out RaycastHit hit, 2))
        {
            IInteractable obj = hit.collider.GetComponent<IInteractable>();
            currentInteractable = obj;
        }
    }

    private Ray GetRay()
    {
        return new Ray(transform.position + rayOrigin, transform.forward + rayDirection);
    }
}
