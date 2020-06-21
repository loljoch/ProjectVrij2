using TMPro;
using UnityEngine;

public class DetailedTradeOffer : MonoBehaviour
{
    private const string TRADE_TEXT = "The <color=black>*</color> can be traded for:";

    [SerializeField] private TextMeshProUGUI tradeText;
    [SerializeField] private TradeResource[] tradeResources;
    [SerializeField] private Vector3 offset;

    public void SetOffer(ScriptableObjects.TradeOffer tradeOffer, Vector3 pos)
    {
        tradeText.text = TRADE_TEXT.Replace("*", tradeOffer.item.name);

        int neededItemLength = tradeOffer.neededItems.Length;

        for (int i = 0; i < tradeResources.Length; i++)
        {
            if(neededItemLength <= i)
            {
                tradeResources[i].gameObject.SetActive(false);
                continue;
            }

            tradeResources[i].Initialize(tradeOffer.neededItems[i].item.id, tradeOffer.neededItems[i].quantity);
            tradeResources[i].gameObject.SetActive(true);
        }

        transform.position = pos + offset;

        gameObject.SetActive(true);
    }
}
