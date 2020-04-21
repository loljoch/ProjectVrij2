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
    private void SpawnTile()
    {
        SpawnTile(testCoordForSpawnTile.x, testCoordForSpawnTile.y);
    }

    [EasyAttributes.Button]
    private void SpawnAllTiles()
    {
        for (int i = 0; i < map.height; i++)
        {
            SpawnXRow();
            testCoordForSpawnTile.y++;
        }
    }

    [EasyAttributes.Button]
    private void DebugAllPixelColors()
    {
        for (int y = 0; y < map.height; y++)
        {
            for (int x = 0; x < map.width; x++)
            {
                Debug.Log(map.GetPixel(x, y));
            }
        }
    }

    [EasyAttributes.Button]
    private void SpawnXRow()
    {
        for (int i = 0; i < map.width; i++)
        {
            SpawnTile(i, testCoordForSpawnTile.y);
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
