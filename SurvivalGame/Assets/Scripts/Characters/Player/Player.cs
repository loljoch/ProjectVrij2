using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Interact variables")]
    [SerializeField] private Vector3 rayOrigin;
    [SerializeField] private Vector3 rayDirection;
    [SerializeField] private float forwardMultiplier;
    [SerializeField] private int rayLength = 2;
    private IInteractable currentInteractable;

    public System.Action<IInteractable> OnFindInteractable;
    public System.Action OnLostInteractable;

    [Header("Item pick-up variables")]
    [SerializeField] private float range = 4f;

    private void Awake()
    {
        VirtualController.Instance.InteractPressActionPerformed += TryInteract;

        OnFindInteractable += (obj) => currentInteractable = obj;

        OnLostInteractable += () => currentInteractable = null;
    }

    private void FixedUpdate()
    {
        CastRay();
        CheckForItemDrops();
    }


    private void TryInteract()
    {
        currentInteractable?.Interact();
    }

    private void CheckForItemDrops()
    {
        //Check proximity for itemdrops
        var hits = Physics.OverlapSphere(transform.position, range, LayerMasks.ItemDrop);

        //Check if it has hit anything
        if (hits.Length == 0) return;

        //Pick up items
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].GetComponent<IItemDrop>().PickUp(transform);
        }
    }

    //Look for interactable objects
    private void CastRay()
    {
        if (Physics.Raycast(GetRay(), out RaycastHit hit, rayLength, LayerMasks.Interactable))
        {
            IInteractable obj = hit.collider.GetComponent<IInteractable>();
            if (obj == currentInteractable) return; //Dont invoke if we found the same interactable

            OnFindInteractable?.Invoke(obj);
        } else
        {
            if (currentInteractable == null) return; //Dont invoke if we never had an interactable

            OnLostInteractable?.Invoke();
        }
    }

    private Ray GetRay()
    {
        return new Ray(transform.position + rayOrigin, (forwardMultiplier * transform.forward) + rayDirection);
    }

    private void OnDrawGizmosSelected()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.yellow;
        style.fontSize = 24;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        UnityEditor.Handles.Label(transform.position + Vector3.up * range, "Item pick-up range", style);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + rayOrigin, ((forwardMultiplier * transform.forward) + rayDirection) * rayLength);
        style.normal.textColor = Color.green;
        style.fontSize = 16;
        UnityEditor.Handles.Label(transform.position + (rayOrigin * 0.5f) + (forwardMultiplier * transform.forward), "Interactable ray", style);
    }
}
