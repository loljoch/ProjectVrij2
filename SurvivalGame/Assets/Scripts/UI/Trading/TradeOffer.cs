using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradeOffer : Selectable, ISubmitHandler, IPointerClickHandler, IEventSystemHandler
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemName;
    private ScriptableObjects.TradeOffer offer;

    private Action<ScriptableObjects.TradeOffer, Vector3> OnSelectAction;

    public void Initialize(ScriptableObjects.TradeOffer tradeOffer, ref DetailedTradeOffer detailedTrade)
    {
        offer = tradeOffer;

        var item = ItemInformation.itemsById[tradeOffer.item.id];

        itemSprite.sprite = item.sprite;
        itemName.text = item.name;

        OnSelectAction += detailedTrade.SetOffer;
    }

    private void Trade()
    {
        UIManager.Instance.inventory.AddItem(offer.item.id);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Select();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        interactable = false;
        interactable = true;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Select();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Trade();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Trade();
    }

    public override void Select()
    {
        DoStateTransition(SelectionState.Selected, true);
        OnSelectAction?.Invoke(offer, transform.position);
        base.Select();

    }

    protected override void OnDestroy()
    {
        OnSelectAction = null;
    }
} 
