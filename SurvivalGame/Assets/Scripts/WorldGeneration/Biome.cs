using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DefaultBiome", menuName ="Game Assets/New Biome", order =1)]
public class Biome : ScriptableObject
{
    [Tooltip("Color of the ground")] public Color groundColor;
    [Tooltip("Walkable decoration (rocks, grass, etc.)")] public List<ItemSpawn> groundDecoration;
    [Tooltip("Like trees in a forest biome")] public List<ItemSpawn> mainResource;

}

[System.Serializable]
public class ItemSpawn
{
    public GameObject prefab;
    [Range(0f, 1f)]
    public float chance;
}
