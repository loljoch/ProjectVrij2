using Extensions.Generics.Singleton;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager, UIManager>
{
    public ItemInformation itemInformation;
    public TradeScreen tradeScreen;

    [Header("Player")]
    public Inventory inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventory.Hide();
            tradeScreen.Hide();
        }
    }
}
