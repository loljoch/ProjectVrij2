using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour, IInteractable
{
    public List<TradeOffer> offers;
    public int currentEXP = 0;
    public int nextLevelEXP = 100;

    public void Interact()
    {
        UIManager.Instance.tradeScreen.Show(this);
    }
}
