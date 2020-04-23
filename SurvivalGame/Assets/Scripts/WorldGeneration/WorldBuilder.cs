using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private MeshRenderer groundTile;
    [EasyAttributes.Preview] [SerializeField] private Texture2D map;

    [SerializeField] private Vector2Int testCoordForSpawnTile;

    [EasyAttributes.Button]
    private void LoadMap()
    {
        Texture2D asset = Resources.Load("Map") as Texture2D;

        map = new Texture2D(40, 40);
        map = asset;
    }

    [EasyAttributes.Button]
    private void SpawnAllTiles()
    {
        for (int y = 0; y < map.height; y++)
        {
            for (int x = 0; x < map.width; x++)
            {
                SpawnTile(x, y);
            }
        }
    }

    private void SpawnTile(int x, int y)
    {
        Color p = map.GetPixel(x, y);
        var tile = Instantiate(groundTile, transform);
        tile.material.color = p;
        tile.transform.position = new Vector3(x, 0, y);
    }
}
