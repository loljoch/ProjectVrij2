﻿using Extensions.Generics.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class RightClickMenu : GenericSingleton<RightClickMenu, RightClickMenu>
{
    [SerializeField] private Button equipButton;
    [SerializeField] private Button eatButton;
    [SerializeField] private Button dropButton;

    private int cItemID = -1;

    protected override void Awake()
    {
        base.Awake();
        eatButton.onClick.AddListener(() => PlayerHP.HealingPlayerEvent?.Invoke(((FoodItem)UIManager.Instance.itemInformation.itemsById[cItemID]).healAmount));
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            Hide();
        }
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