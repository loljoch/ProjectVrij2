using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EasyAttributes;

public class Inventory : ItemStorage
{
    private ItemSlot lastSelected;

    [Header("Foo settings: ")]
    public int itemId = 0;
    public int itemAmount = 1;

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

