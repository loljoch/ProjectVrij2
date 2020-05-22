using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemSprite;

    private Item item;

    public void AssignItem(Item item)
    {
        if (itemName != null)
        {
            itemName.text = item.name;
        }

        if (itemDescription != null)
        {
            itemDescription.text = item.description;
        }

        if (itemSprite != null)
        {
            itemSprite.sprite = item.sprite;
        }

        this.item = item;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        GlobalItemDisplay.Instance.Show(item);
    }
}
