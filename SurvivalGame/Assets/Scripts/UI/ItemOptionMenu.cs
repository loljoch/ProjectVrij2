using UnityEngine;
using UnityEngine.UI;
using Extensions.Generics.Singleton;
using TMPro;

public class ItemOptionMenu : GenericSingleton<ItemOptionMenu, ItemOptionMenu>
{
    [SerializeField] private Button dropItemButton;
    [SerializeField] private Button consumeButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private TextMeshProUGUI equipButtonText;
    [SerializeField] private Button cancelButton;

    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private WeaponItem unarmed;
    private int cItemID = -1;


    protected override void Awake()
    {
        consumeButton.onClick.AddListener(EatItem);
        equipButton.onClick.AddListener(Equip);

        dropItemButton.onClick.AddListener(Hide);
        consumeButton.onClick.AddListener(Hide);
        equipButton.onClick.AddListener(Hide);
        cancelButton.onClick.AddListener(Hide);

        VirtualController.Instance.CancelItemOptionsPerformed += Hide;
    }

    private void EatItem()
    {
        FoodItem food = (FoodItem)ItemInformation.itemsById[cItemID];
        PlayerCombat.HealingPlayerEvent?.Invoke(food.healAmount);
        UIManager.Instance.inventory.RemoveItem(cItemID, 1);
    }

    private void Equip()
    {
        WeaponItem weapon = (WeaponItem)ItemInformation.itemsById[cItemID];
        PlayerCombat.EquipWeaponEvent?.Invoke(weapon);
    }

    public void UnEquip()
    {
        PlayerCombat.EquipWeaponEvent?.Invoke(unarmed);
    }

    public void Show(int itemId, Vector3 pos)
    {
        UIManager.Instance.inventory.SetLastSelected();

        cItemID = itemId;
        transform.position = pos;

        //Deactivate specific buttons
        consumeButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);

        UseCases itemUseCases = ItemInformation.itemsById[itemId].useCases;

        //Processes all possible usecases
        var useCaseValues = System.Enum.GetValues(typeof(UseCases));
        bool exitLoop = false;
        foreach (UseCases value in useCaseValues)
        {
            if ((itemUseCases & value) == value)
            {
                switch (value)
                {
                    case UseCases.Consumable:
                        consumeButton.gameObject.SetActive(true);
                        SetNavigation(dropItemButton, consumeButton, cancelButton);
                        exitLoop = true;
                        break;
                    case UseCases.Weapon:
                        equipButton.gameObject.SetActive(true);
                        if(playerCombat.weaponItem.id == itemId)
                        {
                            equipButton.onClick.RemoveListener(Equip);
                            equipButton.onClick.AddListener(UnEquip);

                            equipButtonText.text = "Unequip";
                        } else
                        {
                            equipButton.onClick.RemoveListener(UnEquip);
                            equipButton.onClick.AddListener(Equip);

                            equipButtonText.text = "Equip";
                        }
                        SetNavigation(dropItemButton, equipButton, cancelButton);
                        exitLoop = true;
                        break;
                    default:
                        break;
                }
            }

            if (exitLoop) break;
        }

        //If loop was not broken it means no buttons were added
        if (!exitLoop) SetNavigation(dropItemButton, cancelButton);

        gameObject.SetActive(true);
        dropItemButton.Select();
    }

    public void Hide()
    {
        if (!gameObject.activeSelf) return;

        gameObject.SetActive(false);
        if (UIManager.Instance.inventory.gameObject.activeSelf)
        {
            UIManager.Instance.inventory.SelectInventory();
        }
    }

    private void SetNavigation(params Selectable[] buttons)
    {
        Navigation navigation = new Navigation();

        navigation.mode = Navigation.Mode.Explicit;

        int buttonLength = buttons.Length;

        for (int i = 0; i < buttonLength; i++)
        {
            //First button
            if(i == 0)
            {
                navigation.selectOnUp = buttons[buttonLength - 1];
            } else
            {
                navigation.selectOnUp = buttons[i - 1];
            }

            //Last button
            if (i == (buttonLength - 1))
            {
                navigation.selectOnDown = buttons[0];
            } else
            {
                navigation.selectOnDown = buttons[i + 1];
            }

            buttons[i].navigation = navigation;
        }
    }
}

