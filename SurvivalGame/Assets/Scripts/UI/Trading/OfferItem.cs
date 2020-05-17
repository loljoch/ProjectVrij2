using UnityEngine;

[CreateAssetMenu(fileName ="NewOffer",menuName ="Game Assets/New Offer")]
public class OfferItem : ScriptableObject
{
    public Item item;
    public Resource[] resources;
    public int xp;
}


