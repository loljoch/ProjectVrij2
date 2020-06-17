using Extensions.Generics.Singleton;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager, UIManager>
{
    public static UIState State
    {
        get => state;
        set
        {
            switch (value)
            {
                case UIState.None:
                    VirtualController.Instance.ListenForMovement = true;
                    break;
                default:
                    VirtualController.Instance.ListenForMovement = false;
                    break;
            }
            state = value;
        }
    }
    private static UIState state = UIState.None;

    public TradeScreen tradeScreen;

    [Header("Player")]
    public Inventory inventory;

    protected override void Awake()
    {
        
    }

}

public enum UIState
{
    None = 0,
    Inventory,
    TradeScreen
}
