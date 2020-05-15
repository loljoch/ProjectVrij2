using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class OfferMenu : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private OfferItemDisplay offerItemDisplayExample;


    public void SpawnOffers(List<OfferItem> offerItems)
    {
        Inventory inventory = TradingManager.Instance.inventory;

        for (int i = 0; i < offerItems.Count; i++)
        {
            OfferItemDisplay obj = Instantiate(offerItemDisplayExample, content);
            obj.AssignOfferItem(offerItems[i]);
            obj.gameObject.SetActive(true);
        }
    }
}
