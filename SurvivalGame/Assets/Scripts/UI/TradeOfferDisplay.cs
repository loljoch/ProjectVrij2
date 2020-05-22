using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeOfferDisplay : MonoBehaviour
{
    [SerializeField] private StackDisplay mainItem;
    [SerializeField] private TextMeshProUGUI itemNameField;
    [SerializeField] private Image[] neededItemBackgrounds;
    [SerializeField] private StackDisplay[] neededItemDisplays;

    [SerializeField] private Button tradeButton;

    private TradeOffer ctTradeOffer;
    private bool CanTrade
    {
        get => canTrade;
        set
        {
            tradeButton.interactable = value;
            canTrade = value;
        }
    }
    private bool canTrade = false;

    public void AssignOffer(TradeOffer tradeOffer)
    {
        ctTradeOffer = tradeOffer;
        mainItem.AssignItem(tradeOffer.item);
        itemNameField.text = tradeOffer.item.name;

        CanTrade = true;

        UpdateOffers();
    }

    public void UpdateOffers()
    {
        for (int i = 0; i < neededItemDisplays.Length; i++)
        {
            if (i >= ctTradeOffer.neededItems.Length)
            {
                neededItemBackgrounds[i].color = Color.clear;
                neededItemDisplays[i].gameObject.SetActive(false);
            } else
            {
                var neededItem = ctTradeOffer.neededItems[i];
                neededItemDisplays[i].AssignItem(neededItem.item, neededItem.quantity);

                if (UIManager.Instance.inventory.AmountOf(neededItem.item.id) >= neededItem.quantity)
                {
                    neededItemBackgrounds[i].color = Color.green;
                } else
                {
                    neededItemBackgrounds[i].color = Color.red;
                    CanTrade = false;
                }
            }
        }
    }

    public void Trade()
    {
        if (CanTrade == false) return;

        var inventory = UIManager.Instance.inventory;

        for (int i = 0; i < ctTradeOffer.neededItems.Length; i++)
        {
            var item = ctTradeOffer.neededItems[i];
            inventory.RemoveItem(item.item.id, item.quantity);
        }

        inventory.AddItem(ctTradeOffer.item.id);

        UIManager.Instance.tradeScreen.UpdateAllOffers();
    }

}
