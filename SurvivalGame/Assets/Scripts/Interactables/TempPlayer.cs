using EasyAttributes;
using Extensions;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [Header("Interact variables")]
    [SerializeField] private Vector3 rayOrigin;
    [SerializeField] private Vector3 rayDirection;
    private IInteractable currentInteractable;

    [Header("Item pick-up variables")]
    [SerializeField] private float range = 4f;

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
        CheckForItemDrops();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + rayOrigin, transform.forward + rayDirection);
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

    private void CastRay()
    {
        if (Physics.Raycast(GetRay(), out RaycastHit hit, 2))
        {
            IInteractable obj = hit.collider.GetComponent<IInteractable>();
            currentInteractable = obj;
            if(obj != null)
            {
                InteractableText.Instance.Show(obj.UseName);
            }
        } else
        {
            currentInteractable = null;
            InteractableText.Instance.Hide();
        }
    }

    private Ray GetRay()
    {
        return new Ray(transform.position + rayOrigin, transform.forward + rayDirection);
    }
}
