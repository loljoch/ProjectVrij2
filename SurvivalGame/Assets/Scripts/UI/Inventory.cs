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

    [Header("Sound settings: ")]
    [FMODUnity.EventRef]
    [SerializeField] private string itemPickUpSFX;
    private ItemSlot lastSelected;

    protected override void Awake()
    {
        base.Awake();

        VirtualController.Instance.InventoryActionPerformed += DynamicShowHide;

        lastSelected = itemSlots[0];
    }

    private void OnDestroy()
    {
        if(VirtualController.Instance != null)
        {
            VirtualController.Instance.InventoryActionPerformed -= DynamicShowHide;
        }
    }

    public override void Show()
    {
        //Only open inventory when nothing else is open
        if (UIManager.State != UIState.None) return;

        base.Show();
        lastSelected.Select();
        UIManager.State = UIState.Inventory;
    }

    [Button]
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
            FMODUnity.RuntimeManager.PlayOneShot(itemPickUpSFX, transform.position);
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

