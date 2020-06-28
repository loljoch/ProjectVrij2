using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Interact variables")]
    [SerializeField] private Vector3 boxOrigin;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private float forwardMultiplier;
    private IInteractable currentInteractable;

    public System.Action<IInteractable> OnFindInteractable;
    public System.Action OnLostInteractable;

    [Header("Item pick-up variables")]
    [SerializeField] private float range = 4f;

    private void Awake()
    {
        VirtualController.Instance.InteractPressActionPerformed += TryInteract;

        OnFindInteractable += FoundInteractable;
        OnLostInteractable += LostInteractable;
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
        var colliders = Physics.OverlapBox((transform.forward * forwardMultiplier) + boxOrigin + transform.position, boxSize, Quaternion.identity, LayerMasks.Interactable);

        if(colliders.Length > 0)
        {
            IInteractable obj = colliders[0].GetComponent<IInteractable>();

            if (obj == currentInteractable) return;

            OnFindInteractable?.Invoke(obj);
        } else
        {
            if (currentInteractable == null) return; //Dont invoke if we never had an interactable

            OnLostInteractable?.Invoke();
        }
    }

    private void FoundInteractable(IInteractable obj)
    {
        if (currentInteractable != null && currentInteractable != obj) LostInteractable();
        currentInteractable = obj;
        currentInteractable.HighLighted = true;
    }

    private void LostInteractable()
    {
        currentInteractable.HighLighted = false;
        currentInteractable = null;
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
        Gizmos.DrawWireCube((transform.forward * forwardMultiplier) + boxOrigin + transform.position, boxSize);
        style.normal.textColor = Color.green;
        style.fontSize = 16;
        UnityEditor.Handles.Label((transform.forward * forwardMultiplier) + boxOrigin + transform.position, "Interactable cast", style);

    }
}
