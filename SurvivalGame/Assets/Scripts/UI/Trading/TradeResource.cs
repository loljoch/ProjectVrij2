using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeResource : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI tradeAmounts;
    [SerializeField] private Image canTrade;

    public bool Initialize(int itemId, int amountNeeded)
    {
        int inventoryAmount = UIManager.Instance.inventory.AmountOf(itemId);
        bool hasAmount = (inventoryAmount >= amountNeeded);
        var item = ItemInformation.itemsById[itemId];

        itemSprite.sprite = item.sprite;
        itemName.text = item.name;
        tradeAmounts.text = $"{inventoryAmount} / {amountNeeded}";
        canTrade.color = (hasAmount) ? Color.blue : Color.red;

        return hasAmount;
    }
}
