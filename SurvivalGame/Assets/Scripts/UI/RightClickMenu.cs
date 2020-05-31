using Extensions.Generics.Singleton;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class RightClickMenu : GenericSingleton<RightClickMenu, RightClickMenu>
{
    [SerializeField] private Button equipButton;
    [SerializeField] private Button eatButton;
    [SerializeField] private Button dropButton;

    private int cItemID = -1;

    private void Start()
    {
        eatButton.onClick.AddListener(EatItem);

        eatButton.onClick.AddListener(Hide);
        equipButton.onClick.AddListener(Hide);
        dropButton.onClick.AddListener(Hide);
    }

    private void EatItem()
    {
        FoodItem food = (FoodItem)UIManager.Instance.itemInformation.itemsById[cItemID];
        PlayerCombat.HealingPlayerEvent?.Invoke(food.healAmount);
        UIManager.Instance.inventory.RemoveItem(cItemID, 1);
    }

    public void Show(int itemId, Vector3 pos)
    {
        if (cItemID == itemId)
        {
            gameObject.SetActive(true);
            return;
        }

        cItemID = itemId;
        transform.position = pos;

        //Deactivate all buttons
        dropButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        eatButton.gameObject.SetActive(false);

        UseCases itemUseCases = UIManager.Instance.itemInformation.itemsById[itemId].useCases;

        //Processes all possible usecases
        var useCaseValues = System.Enum.GetValues(typeof(UseCases));
        foreach (UseCases value in useCaseValues)
        {
            if ((itemUseCases & value) == value)
            {
                switch (value)
                {
                    case UseCases.Droppable:
                        dropButton.gameObject.SetActive(false);
                        break;
                    case UseCases.Equipable:
                        equipButton.gameObject.SetActive(true);
                        break;
                    case UseCases.Eatable:
                        eatButton.gameObject.SetActive(true);
                        break;
                }
            }
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
