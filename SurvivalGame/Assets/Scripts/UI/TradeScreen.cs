using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class TradeScreen : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Transform offersParent;
    [SerializeField] private TradeOfferDisplay offerDisplayPrefab;

    private List<TradeOfferDisplay> tradeOfferDisplays;


    public void DynamicShowHide(Trader trader)
    {
        if (gameObject.activeSelf)
        {
            Hide();
        } else
        {
            Show(trader);
        }
    }

    private void Show(Trader trader)
    {
        player.OnLostInteractable += Hide;

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

    private void Hide()
    {
        player.OnLostInteractable -= Hide;
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
