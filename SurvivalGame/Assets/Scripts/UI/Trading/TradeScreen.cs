using Extensions;
using System.Collections;
using UnityEngine;

public class TradeScreen : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TradeOffer tradeOfferPrefab;
    [SerializeField] private DetailedTradeOffer detailedTradeOffer;

    private Trader cTrader;

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    public void DynamicShowHide(Trader trader)
    {
        cTrader = trader;
        if (gameObject.activeSelf)
        {
            Hide();
        } else
        {
            Show(trader);
        }
    }

    public void Show(Trader trader)
    {
        VirtualController.Instance.CancelItemOptionsPerformed += Hide;
        UIManager.Instance.inventory.Hide();
        player.OnLostInteractable += Hide;
        UIManager.State = UIState.TradeScreen;

        var offers = trader.offers;

        gameObject.DestroyChildren();

        TradeOffer firstChild = null;

        for (int i = 0; i < offers.Count; i++)
        {
            var temp = Instantiate(tradeOfferPrefab, transform);
            temp.Initialize(offers[i], ref detailedTradeOffer);
            if (i != 0) continue;
            firstChild = temp;
        }

        gameObject.SetActive(true);

        StartCoroutine(LateSelect(firstChild));
    }

    private IEnumerator LateSelect(TradeOffer offer)
    {
        yield return new WaitForEndOfFrame();
        offer.Select();
    }

    private void Hide()
    {
        if(cTrader != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(cTrader.goodbyeSFX, cTrader.transform.position);
        }
        VirtualController.Instance.CancelItemOptionsPerformed -= Hide;
        player.OnLostInteractable -= Hide;
        gameObject.SetActive(false);
        detailedTradeOffer.gameObject.SetActive(false);
        UIManager.State = UIState.None;
    }

    public void UpdateAllOffers()
    {

    }
}
