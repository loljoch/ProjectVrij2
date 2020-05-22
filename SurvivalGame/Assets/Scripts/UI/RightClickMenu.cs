using Extensions.Generics.Singleton;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : GenericSingleton<RightClickMenu, RightClickMenu>, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button equipButton;
    [SerializeField] private Button eatButton;
    [SerializeField] private Button dropButton;

    private bool isPointerOnMe = false;
    private int cItemID = -1;

    private void Start()
    {
        eatButton.onClick.AddListener(EatItem);

        eatButton.onClick.AddListener(Hide);
        equipButton.onClick.AddListener(Hide);
        dropButton.onClick.AddListener(Hide);
    }

    private void Update()
    {
        if (isPointerOnMe) return;

        if (Input.GetMouseButtonDown(0))
        {
            Hide();
            isPointerOnMe = false;
        }
    }

    private void EatItem()
    {
        FoodItem food = (FoodItem)UIManager.Instance.itemInformation.itemsById[cItemID];
        PlayerHP.HealingPlayerEvent?.Invoke(food.healAmount);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOnMe = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOnMe = false;
    }
}
