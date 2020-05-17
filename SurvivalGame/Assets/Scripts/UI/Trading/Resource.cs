using UnityEngine;

[System.Serializable]
public class Resource
{
    public Item item;
    [SerializeField] private int quantity;
    public int Quantity
    {
        get
        {
            return quantity;
        }
        set
        {
            quantity = value;
            OnChangeQuantity();
        }
    }
    public delegate void onChangeQuantity();
    public onChangeQuantity OnChangeQuantity;

    public Resource()
    {
        quantity = 1;
    }

    public Resource(Item item)
    {
        this.item = item;
        quantity = 1;
    }

    public Resource(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
