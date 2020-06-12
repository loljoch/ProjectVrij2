using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour, IInteractable
{
    public List<ScriptableObjects.TradeOffer> offers;
    public int currentEXP = 0;
    public int nextLevelEXP = 100;

    public string UseName => useName;
    [SerializeField] private string useName;

    public float HoldTime => 0;
    public string InteractionType => "trade with ";

    public void Interact()
    {
        UIManager.Instance.tradeScreen.DynamicShowHide(this);
    }
}
