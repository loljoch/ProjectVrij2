using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName ="Game Assets/New Item")]

public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public int id;
}
