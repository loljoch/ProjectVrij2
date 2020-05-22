using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trader : MonoBehaviour
{
    public List<OfferItem> offers;
    public int currentXp = 0;
    public int nextLevelXp = 100;

    private void OnMouseDown()
    {
        OpenShop();
    }

    public void OpenShop()
    {
        UIManager.Instance.ShowTradeScreen(this);
    }
}
