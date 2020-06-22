using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeResource : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI amountNeeded;
    [SerializeField] private TextMeshProUGUI amountYouHave;

    public void Initialize(int itemId, int amountNeeded)
    {
        var item = ItemInformation.itemsById[itemId];

        itemSprite.sprite = item.sprite;
        itemName.text = item.name;
        this.amountNeeded.text = amountNeeded.ToString();
        amountYouHave.text = UIManager.Instance.inventory.AmountOf(itemId).ToString();
    }
}
