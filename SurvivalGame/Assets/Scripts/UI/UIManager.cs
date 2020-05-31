using Extensions.Generics.Singleton;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager, UIManager>
{
    public ItemInformation itemInformation;
    public TradeScreen tradeScreen;

    [Header("Player")]
    public Inventory inventory;

}
