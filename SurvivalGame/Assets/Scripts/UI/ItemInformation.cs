using UnityEngine;
using System.Collections.Generic;

public class ItemInformation : MonoBehaviour
{
    [SerializeField] private ItemInformationObject itemInfo;
    public static Dictionary<int, Item> itemsById;

    private void Awake()
    {
        itemsById = new Dictionary<int, Item>();
        itemInfo.itemsById.CopyTo(itemsById);
        itemInfo = null;
        Destroy(this);
    }
}
