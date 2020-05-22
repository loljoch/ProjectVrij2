using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemInformation", menuName ="Game/ItemInformation", order =1)]
public class ItemInformation : ScriptableObject
{
    public Int_Item itemsById;
}


[System.Serializable]
public class Int_Item : SerializableDictionaryBase<int, Item> { }



