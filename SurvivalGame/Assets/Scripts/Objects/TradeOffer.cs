using UnityEngine;

[CreateAssetMenu(fileName = "New Offer", menuName = "Game/New Offer")]
public class TradeOffer : ScriptableObject
{
    public Item item;
    public ItemWithQuantity[] neededItems;
    public int xp;
}

[System.Serializable]
public class ItemWithQuantity
{
    public Item item;
    public int quantity = 1;
}
