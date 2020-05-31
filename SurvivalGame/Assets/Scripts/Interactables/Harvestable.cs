using UnityEngine;


public abstract class Harvestable : MonoBehaviour, IInteractable
{
    public string UseName => useName;
    [SerializeField] protected string useName;

    public float HoldTime => interactTime;
    [SerializeField] protected float interactTime = 2f;

    public string InteractionType => interactionType;
    [SerializeField] private string interactionType = "harvest the ";

    [SerializeField] protected GameObject dropPrefab;

    public void Interact()
    {
        VirtualController.Instance.InteractHoldActionPerformed += EndHarvest;
        VirtualController.Instance.InteractHoldActionCanceled += CancelHarvest;

        StartHarvest();
    }

    protected abstract void StartHarvest();

    protected virtual void EndHarvest()
    {
        CancelHarvest();
    }

    protected virtual void CancelHarvest()
    {
        VirtualController.Instance.InteractHoldActionPerformed -= EndHarvest;
        VirtualController.Instance.InteractHoldActionCanceled -= CancelHarvest;
    }
}
