using RotaryHeart.Lib.SerializableDictionary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemInformation", menuName ="Game/ItemInformation", order =1)]
public class ItemInformationObject : ScriptableSingleton<ItemInformationObject>
{
    public Int_Item itemsById;
}


[System.Serializable]
public class Int_Item : SerializableDictionaryBase<int, Item> { }



