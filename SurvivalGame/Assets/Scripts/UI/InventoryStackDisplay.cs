using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryStackDisplay : StackDisplay, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector]
    public override int Quantity
    {
        get => base.Quantity;
        set
        {
            if (value == 0)
            {
                RemoveItem();
            }
            base.Quantity = value;
        }
    }
    [HideInInspector] public bool isEmpty = true;
    public int maxQuantity
    {
        get;
        private set;
    }

    public delegate void onEmpty(int itemId);
    public onEmpty OnStackEmpty;

    public override void AssignItem(int itemId, int initialQuantity = 1)
    {
        isEmpty = false;

        Item item = UIManager.Instance.itemInformation.itemsById[itemId];

        itemID = itemId;
        Quantity = initialQuantity;
        maxQuantity = item.maxQuantity;

        itemSprite.sprite = item.sprite;

        gameObject.SetActive(true);
    }

    public bool HasRoom(int forAmount = 1)
    {
        return ((quantity + forAmount) <= maxQuantity);
    }

    private void RemoveItem()
    {
        isEmpty = true;
        OnStackEmpty?.Invoke(itemID);

        itemID = -1;
        Quantity = 1;
        itemSprite.sprite = null;

        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            RightClickMenu.Instance.Show(itemID, transform.position);
        }
    }
}
