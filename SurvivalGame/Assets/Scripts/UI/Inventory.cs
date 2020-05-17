using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ResourceDisplay> itemSlots;
    [HideInInspector] public List<int> itemIds = new List<int>(24);
    public delegate void onAddItem();
    public delegate void onHide();
    public onAddItem OnAddItem;
    public onHide OnHide;

    public bool Active
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            if (value)
            {
                Show();
            } else
            {
                Hide();
            }
        }
    }


    [Header("Test variables")]
    public Item testItem;

    [EasyAttributes.Button]
    public void TestAddItem()
    {
        AddItem(testItem);
    }

    public void AddItem(Item item, int quantity = 1)
    {
        if (itemIds.Contains(item.id))
        {
            ResourceDisplay rd = itemSlots[itemIds.IndexOf(item.id)];
            rd.resource.Quantity += quantity;
        } else
        {
            int index = GetOpenIndex();
            itemIds[index] = item.id;
            itemSlots[index].AssignResource(new Resource(item, quantity));
        }

        OnAddItem?.Invoke();
    }

    public void RemoveItem(int id, int amount)
    {
        int index = itemIds.IndexOf(id);
        ResourceDisplay rd = itemSlots[index];
        int cQuantity = rd.resource.Quantity;

        cQuantity -= amount;

        if (cQuantity < 1)
        {
            rd.RemoveResource();
            itemIds[index] = -1;
        } else
        {
            rd.resource.Quantity -= amount;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        OnHide?.Invoke();
    }

    private int GetOpenIndex()
    {
        int index = itemIds.IndexOf(-1);

        if(index == -1)
        {
            itemIds.Add(-1);
            index = itemIds.Count - 1;
        }

        return index;
    }
}
