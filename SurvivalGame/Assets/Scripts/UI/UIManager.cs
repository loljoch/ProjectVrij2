using Extensions.Generics.Singleton;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager, UIManager>
{
    [Header("Player")]
    public Inventory inventory;

    [Header("Trading Screen")]
    public GameObject tradeScreenPanel;
    public TradePopUp tradePopUp;
    public OfferMenu offerMenu;
    public Trader currentTrader;

    //Disables to ability for the player to close the inventory
    private bool forceInventoryDisplayed = false;

    protected override void Awake()
    {
        base.Awake();

        inventory.OnHide += GlobalItemDisplay.Instance.Hide;
        inventory.OnHide += HideTradeScreen;
    }

    private void OnDisable()
    {
        inventory.OnHide -= GlobalItemDisplay.Instance.Hide;
        inventory.OnHide -= HideTradeScreen;
    }

    public void ShowTradeScreen(Trader trader)
    {
        tradeScreenPanel.SetActive(true);
        inventory.Show();
        offerMenu.SpawnOffers(trader.offers);
        currentTrader = trader;
    }

    public void HideTradeScreen()
    {
        tradeScreenPanel.SetActive(false);
        if (inventory.Active)
        {
            inventory.Hide();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.Active = !inventory.Active;
        }
    }
}
