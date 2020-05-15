using Extensions.Generics.Singleton;

public class TradingManager : GenericSingleton<TradingManager, TradingManager>
{
    public TradePopUp tradePopUp;
    public OfferMenu offerMenu;
    public Inventory inventory;
    public Trader currentTrader;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    public void InitializeTradeScreen(Trader trader)
    {
        gameObject.SetActive(true);
        offerMenu.SpawnOffers(trader.offers);
        currentTrader = trader;
    }
}
