using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour, IInteractable
{
    public List<TradeOffer> offers;
    public int currentEXP = 0;
    public int nextLevelEXP = 100;

    public string UseName => useName;
    [SerializeField] private string useName;

    public void Interact()
    {
        UIManager.Instance.tradeScreen.Show(this);
    }
}
