using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradePopUp : MonoBehaviour
{
    [SerializeField] private OfferItemDisplay offerItemDisplay;
    [SerializeField] private TMP_InputField quantityField;
    [SerializeField] private Slider currentExpSlider, addedExpSlider;

    public void AssignItem(OfferItem offerItem)
    {
        gameObject.SetActive(true);
        offerItemDisplay.AssignOfferItem(offerItem);

        UpdateExpBar();

        quantityField.text = "1";
    }

    private void UpdateExpBar()
    {
        int maxExp = TradingManager.Instance.currentTrader.nextLevelXp;
        int cExp = TradingManager.Instance.currentTrader.currentXp;

        currentExpSlider.maxValue = maxExp;
        currentExpSlider.value = cExp;
        addedExpSlider.maxValue = maxExp;
        addedExpSlider.value = cExp + offerItemDisplay.offerItem.xp;
    }

    public void Trade()
    {
        int quantity = int.Parse(quantityField.text);

        for (int q = 0; q < quantity; q++)
        {
            //Check if we can trade
            if (!offerItemDisplay.IsItemTradeable())
            {
                Debug.Log("Not enough resources!");
                return;
            }

            OfferResourceDisplay[] rds = offerItemDisplay.resourceDisplays;
            Inventory inventory = TradingManager.Instance.inventory;

            //Remove items from inventory
            for (int i = 0; i < rds.Length; i++)
            {
                if (rds[i].resource == null) continue;
                inventory.RemoveItem(rds[i].resource.item.id, rds[i].resource.Quantity);
                rds[i].UpdateRequirement();
            }

            //Add item to inventory
            inventory.AddItem(offerItemDisplay.offerItem.item);

            //Add EXP
            TradingManager.Instance.currentTrader.currentXp += offerItemDisplay.offerItem.xp;
        }

        UpdateExpBar();
    }
}
