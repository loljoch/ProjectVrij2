using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class TradeScreen : MonoBehaviour
{
    [SerializeField] private Transform offersParent;
    [SerializeField] private TradeOfferDisplay offerDisplayPrefab;

    private List<TradeOfferDisplay> tradeOfferDisplays;

    public void Show(Trader trader)
    {
        var offers = trader.offers;
        tradeOfferDisplays = new List<TradeOfferDisplay>();

        offersParent.gameObject.DestroyChildren();

        for (int i = 0; i < offers.Count; i++)
        {
            TradeOfferDisplay tod = Instantiate(offerDisplayPrefab, offersParent);
            tod.AssignOffer(offers[i]);
            tradeOfferDisplays.Add(tod);
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateAllOffers()
    {
        for (int i = 0; i < tradeOfferDisplays.Count; i++)
        {
            tradeOfferDisplays[i].UpdateOffers();
        }
    }
}
