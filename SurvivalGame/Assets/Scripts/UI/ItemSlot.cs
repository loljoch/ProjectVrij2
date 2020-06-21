using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : Selectable, ISubmitHandler, IPointerClickHandler, IEventSystemHandler
{
    [HideInInspector] public int itemID;
    public virtual int Quantity
    {
        get => quantity;
        set
        {
            quantity = value;
            UpdateQuantity();
            if(value == 0)
            {
                RemoveItem();
            }
        }
    }
    private int quantity;

    public int MaxQuantity
    {
        get;
        private set;
    }
    [HideInInspector] public bool isEmpty = true;
    public bool isSelected => (currentSelectionState == SelectionState.Selected);

    [Header("Components: ")]
    [SerializeField] private Image slotBackground;
    [SerializeField] private Image itemSprite;
    [SerializeField] private GameObject quantityBackground;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    //Actions
    public System.Action<int> OnStackEmpty;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public void AssignItem(int itemId, int initialQuantity = 1)
    {
        isEmpty = false;

        Item item = ItemInformation.itemsById[itemId];

        itemID = itemId;
        Quantity = initialQuantity;
        MaxQuantity = item.maxQuantity;

        itemSprite.sprite = item.sprite;

        itemSprite.enabled = true;
    }

    private void RemoveItem()
    {
        isEmpty = true;
        OnStackEmpty?.Invoke(itemID);

        itemID = -1;
        itemSprite.enabled = false;
        quantityBackground.SetActive(false);
    }

    public bool HasRoom(int forAmount = 1) => ((quantity + forAmount) <= MaxQuantity);

    #region Visual UI Functions
    private void UpdateQuantity()
    {
        quantityBackground.SetActive((quantity > 1));
        itemQuantity.text = quantity.ToString();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Select();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        interactable = false;
        interactable = true;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Select();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        if (!isEmpty)
        {
            ToolTip.Instance.Hide();
        }
        base.OnDeselect(eventData);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (isEmpty) return;
        ItemOptionMenu.Instance.Show(itemID, transform.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isEmpty) return;
        ItemOptionMenu.Instance.Show(itemID, transform.position);
    }

    public override void Select()
    {
        DoStateTransition(SelectionState.Selected, true);
        base.Select();
        if (isEmpty) return;
        ToolTip.Instance.Show(itemID, transform.position);
    }
    #endregion
}

