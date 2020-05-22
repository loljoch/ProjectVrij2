using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Game/New Item", order = 2)]
public class Item : ScriptableObject
{
    public int id;
    public new string name;
    public Sprite sprite;

    public UseCases useCases;

    public int maxQuantity = 64;

}

[System.Flags]
public enum UseCases
{
    Droppable = 0,
    Equipable = 1,
    Eatable = 2
}
