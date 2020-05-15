using UnityEngine;
using UnityEngine.UI;

public class OfferResourceDisplay : ResourceDisplay
{
    [SerializeField] private Image outLine;
    [HideInInspector] public bool meetsRequirement = false;

    public override void ShowResource()
    {
        base.ShowResource();

        UpdateRequirement();
    }

    public void UpdateRequirement()
    {
        if (outLine != null)
        {
            if (resource == null) return;

            Inventory inventory = TradingManager.Instance.inventory;
            if (inventory.itemIds.Contains(resource.item.id))
            {
                int index = inventory.itemIds.IndexOf(resource.item.id);
                meetsRequirement = (inventory.itemSlots[index].resource.Quantity >= resource.Quantity);
                outLine.color = meetsRequirement ? Color.green : Color.red;
            } else
            {
                meetsRequirement = false;
                outLine.color = Color.red;
            }
        }
    }
}
