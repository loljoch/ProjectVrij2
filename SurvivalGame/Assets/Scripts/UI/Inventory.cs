using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EasyAttributes;
using System;

public class Inventory : ItemStorage
{
    /// <summary>
    /// Gives the player an item (ItemID, Quantity), returns if succesfully added
    /// </summary>
    public static Action<int, int> OnObtainItem;

    [Header("Foo settings: ")]
    public int itemId = 0;
    public int itemAmount = 1;
    private ItemSlot lastSelected;

    [Button]
    private void FooAddItem()
    {
        AddItem(itemId, itemAmount);
    }

    protected override void Awake()
    {
        base.Awake();

        VirtualController.Instance.InventoryActionPerformed += DynamicShowHide;

        lastSelected = itemSlots[0];
    }

    private void OnDestroy()
    {
        VirtualController.Instance.InventoryActionPerformed += DynamicShowHide;

    }

    public override void Show()
    {
        //Only open inventory when nothing else is open
        if (UIManager.State != UIState.None) return;

        base.Show();
        lastSelected.Select();
        UIManager.State = UIState.Inventory;
    }

    public override void Hide()
    {
        lastSelected = GetLastSelected();
        lastSelected.OnDeselect(null);
        base.Hide();
        ItemOptionMenu.Instance.Hide();
        UIManager.State = UIState.None;
    }

    public override bool AddItem(int itemId, int quantity = 1)
    {
        bool success = base.AddItem(itemId, quantity);
        if (success)
        {
            OnObtainItem?.Invoke(itemId, quantity);
        }

        return success;
    }

    public void SelectInventory()
    {
        lastSelected.Select();
    }

    public void SetLastSelected()
    {
        lastSelected = GetLastSelected();
    }

    private ItemSlot GetLastSelected()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].isSelected) return itemSlots[i];
        }

        return itemSlots[0];
    }
}

