using Extensions.Vector;
using System;
using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New LootDrop", menuName = "Game/New LootTable", order = 7)]
    public class LootDrop : ScriptableObject
    {
        public ChanceDrop[] chanceDrops;
        public GuaranteedDrop[] guaranteedDrops;

        public bool HasChanceDrops => chanceDrops.Length > 0;

        public void Spawn(Vector3 position)
        {
            for (int i = 0; i < guaranteedDrops.Length; i++)
            {
                var drop = guaranteedDrops[i];
                for (int l = 0; l < drop.dropAmount; l++)
                {
                    Instantiate(drop.itemPrefab, RandomSpawnPos(position), Quaternion.identity);
                }
            }

            if (!HasChanceDrops) return;

            for (int i = 0; i < chanceDrops.Length; i++)
            {
                var drop = chanceDrops[i];
                int randomNumber = UnityEngine.Random.Range(0, 100);
                if (randomNumber > drop.dropChance) continue;
                for (int l = 0; l < drop.dropAmount; l++)
                {
                    Instantiate(drop.itemPrefab, RandomSpawnPos(position), Quaternion.identity);
                }
            }
        }

        private Vector3 RandomSpawnPos(Vector3 fromPosition)
        {
            Vector3 pos = UnityEngine.Random.insideUnitSphere;
            pos.y = 0;
            pos += fromPosition;
            return pos;
        }

        public void Give()
        {
            for (int i = 0; i < guaranteedDrops.Length; i++)
            {
                var drop = guaranteedDrops[i];
                UIManager.Instance.inventory.AddItem(drop.itemID, drop.dropAmount);
            }

            if (!HasChanceDrops) return;

            for (int i = 0; i < chanceDrops.Length; i++)
            {
                var drop = chanceDrops[i];
                int randomNumber = UnityEngine.Random.Range(0, 100);
                if (randomNumber > drop.dropChance) continue;
                UIManager.Instance.inventory.AddItem(drop.itemID, drop.dropAmount);

            }
        }
    }

    [Serializable]
    public class ChanceDrop
    {
        public int itemID;
        public GameObject itemPrefab;
        [Range(0, 100)]
        public int dropChance;
        public int dropAmount;
    }

    [Serializable]
    public class GuaranteedDrop
    {
        public int itemID;
        public GameObject itemPrefab;
        public int dropAmount;
    }
}
