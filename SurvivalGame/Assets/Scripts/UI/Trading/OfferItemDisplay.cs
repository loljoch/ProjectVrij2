using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfferItemDisplay : MonoBehaviour
{
    public OfferResourceDisplay[] resourceDisplays;
    [HideInInspector] public OfferItem offerItem;
    [SerializeField] private ItemDisplay itemDisplay;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private Button button;

    private void OnEnable()
    {
        button?.onClick.AddListener(() => UIManager.Instance.tradePopUp.AssignItem(offerItem));

        //Update requirements when inventory changes happen
        Inventory inventory = UIManager.Instance.inventory;
        for (int i = 0; i < resourceDisplays.Length; i++)
        {
            inventory.OnAddItem += resourceDisplays[i].UpdateRequirement;
        }
    }

    private void OnDisable()
    {
        button?.onClick.RemoveListener(() => UIManager.Instance.tradePopUp.AssignItem(offerItem));

        //Update requirements when inventory changes happen
        Inventory inventory = UIManager.Instance.inventory;
        for (int i = 0; i < resourceDisplays.Length; i++)
        {
            inventory.OnAddItem -= resourceDisplays[i].UpdateRequirement;
        }
    }

    public void AssignOfferItem(OfferItem _offerItem)
    {
        //Assigns the itemDisplay
        itemDisplay.AssignItem(_offerItem.item);

        //Assigns the resources
        int amountResources = _offerItem.resources.Length - 1;
        for (int i = 0; i < resourceDisplays.Length; i++)
        {
            if (i > amountResources)
            {
                resourceDisplays[i].gameObject.SetActive(false);
                resourceDisplays[i].RemoveResource();
            } else
            {
                resourceDisplays[i].AssignResource(_offerItem.resources[i]);
                resourceDisplays[i].gameObject.SetActive(true);
            }
        }

        //Sets the EXP text
        xpText.text = $"+{_offerItem.xp} EXP";

        //Caches item
        offerItem = _offerItem;
    }

    //Checks if item is tradeable
    public bool IsItemTradeable()
    {
        for (int i = 0; i < resourceDisplays.Length; i++)
        {
            if (resourceDisplays[i].resource != null && !resourceDisplays[i].meetsRequirement)
            {
                return false;
            }
        }

        return true;
    }
}
