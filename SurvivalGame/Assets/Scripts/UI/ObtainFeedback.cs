using EasyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainFeedback : MonoBehaviour
{
    [SerializeField] private ObtainMessage messagePrefab;

    private void Awake()
    {
        Inventory.OnObtainItem += SpawnMessage;
    }

    private void OnDestroy()
    {
        Inventory.OnObtainItem -= SpawnMessage;
    }

    [Button]
    public void FooMessage()
    {
        SpawnMessage(1, 2);
    }

    private void SpawnMessage(int itemId, int quantity)
    {
        Item item = ItemInformation.itemsById[itemId];
        ObtainMessage obj = Instantiate(messagePrefab, transform);
        obj.AssignItem(itemId, quantity);
        Destroy(obj.gameObject, 1.2f);
    }
}
