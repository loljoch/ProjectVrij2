using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StackDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public int itemID;
    [HideInInspector] public virtual int Quantity
    {
        get
        {
            return quantity;
        }
        set
        {
            quantity = value;
            UpdateQuantity();
        }
    }
    protected int quantity;

    [SerializeField] protected Image itemSprite;

    [SerializeField] protected GameObject quantityBackground;
    [SerializeField] protected TextMeshProUGUI itemQuantity;

    private void OnEnable()
    {
        itemSprite.raycastTarget = true;
    }

    private void OnDisable()
    {
        itemSprite.raycastTarget = false;
    }

    public virtual void AssignItem(int itemId, int initialQuantity = 1)
    {
        Item item = UIManager.Instance.itemInformation.itemsById[itemId];

        itemID = itemId;
        Quantity = initialQuantity;

        itemSprite.sprite = item.sprite;

        gameObject.SetActive(true);
    }

    public virtual void AssignItem(Item item, int initialQuantity = 1)
    {
        itemID = item.id;
        Quantity = initialQuantity;

        itemSprite.sprite = item.sprite;

        gameObject.SetActive(true);
    }

    protected void UpdateQuantity()
    {
        quantityBackground.SetActive((quantity > 1));
        itemQuantity.text = quantity.ToString();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //Show name on hover
        ToolTip.Instance.Show(itemID, transform.position);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.Instance.Hide();
    }
}