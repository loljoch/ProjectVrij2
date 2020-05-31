using EasyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryStackDisplay> stackDisplays;

    private List<int> items = new List<int>(24);
    private int occupiedDisplays = 0;

    private void Awake()
    {
        VirtualController.Instance.InventoryActionPerformed += DynamicShowHide;

        Hide();
    }

    public void DynamicShowHide()
    {
        if (gameObject.activeSelf)
        {
            Hide();
        } else
        {
            Show();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //Just an items.Contains shortcut for outside classes
    public bool Contains(int itemId)
    {
        return items.Contains(itemId);
    }

    //Returns the total amount of one item
    public int AmountOf(int itemId)
    {
        int total = 0;

        for (int i = 0; i < stackDisplays.Count; i++)
        {
            if (stackDisplays[i].itemID == itemId)
            {
                total += stackDisplays[i].Quantity;
            }
        }

        return total;
    }

    /// <summary>
    /// Adds item is possible, returns true if item was added succesfully
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns>True if item was added</returns>
    public bool AddItem(int itemId, int quantity = 1)
    {
        if (items.Contains(itemId))
        {
            do
            {
                InventoryStackDisplay sd = GetItemStackWithRoom(itemId);
                if (sd == null)
                {
                    return AddNewItem(itemId, quantity);
                } else if (sd.HasRoom(quantity))
                {
                    sd.Quantity += quantity;
                    break;
                } else
                {
                    int temp = (sd.maxQuantity - sd.Quantity);
                    quantity -= temp;
                    sd.Quantity += temp;
                }
            } while (quantity > 0);
        } else
        {
            return AddNewItem(itemId, quantity);
        }

        return true;
    }

    public void RemoveItem(int itemId, int removalQuantity)
    {
        if (!items.Contains(itemId))
        {
            Debug.LogError($"Tried to remove item ID: {itemId} while it doesn't exist");
        }

        do
        {
            StackDisplay sd = GetItemStack(itemId);
            int sdQuantity = sd.Quantity;

            if(sdQuantity >= removalQuantity)
            {
                sd.Quantity -= removalQuantity;
                removalQuantity -= removalQuantity;
            } else
            {
                removalQuantity -= sdQuantity;
                sd.Quantity -= sdQuantity;
            }

        } while (removalQuantity > 0);
    }

    private bool AddNewItem(int itemId, int quantity)
    {
        if (HasRoom())
        {
            items.Add(itemId);
            InventoryStackDisplay sd = GetEmptyStack();

            sd.AssignItem(itemId, quantity);

            //If for some reason the quantity is bigger than a full stack, fix it
            if(quantity > sd.maxQuantity)
            {
                sd.Quantity = sd.maxQuantity;
                quantity -= sd.maxQuantity;
                return AddItem(itemId, quantity);
            }

            if(sd.OnStackEmpty == null)
            {
                sd.OnStackEmpty += RemoveItemStack;
            }

            occupiedDisplays += 1;
            return true;
        } else
        {
            return false;
        }
    }

    private void RemoveItemStack(int itemId)
    {
        occupiedDisplays -= 1;
        int index = items.IndexOf(itemId);
        items.RemoveAt(index);
    }

    private bool HasRoom()
    {
        return (items.Capacity > occupiedDisplays);
    }

    private InventoryStackDisplay GetItemStackWithRoom(int itemId)
    {
        //Find stackdisplay with the itemID
        for (int i = 0; i < stackDisplays.Count; i++)
        {
            if (stackDisplays[i].itemID == itemId && stackDisplays[i].HasRoom())
            {
                return stackDisplays[i];
            }
        }

        return null;
    }

    private InventoryStackDisplay GetItemStack(int itemId)
    {
        int lowestQuantity = int.MaxValue;
        int index = 0;

        //Find stackdisplay with the itemID
        for (int i = 0; i < stackDisplays.Count; i++)
        {
            if (stackDisplays[i].itemID == itemId && stackDisplays[i].Quantity < lowestQuantity)
            {
                index = i;
            }
        }

        return stackDisplays[index];
    }

    private InventoryStackDisplay GetEmptyStack()
    {
        //Find stackdisplay with the itemID
        for (int i = 0; i < stackDisplays.Count; i++)
        {
            if (stackDisplays[i].isEmpty)
            {
                return stackDisplays[i];
            }
        }

        return null;
    }
}


