using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObtainMessage : ItemSlot
{
    [SerializeField] private TextMeshProUGUI itemName;

    public override void AssignItem(int itemId, int initialQuantity = 1)
    {
        base.AssignItem(itemId, initialQuantity);
        itemName.text = ItemInformation.itemsById[itemId].name;
        itemQuantity.text = 'x' + itemQuantity.text;
    }
}
