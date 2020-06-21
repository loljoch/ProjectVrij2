﻿using UnityEngine;

public class FruitPlant : Harvestable
{
    [SerializeField] private Rigidbody[] dropFruits;
    [SerializeField] private GameObject[] cosmeticFruits;

    protected override void StartHarvest()
    {
        
    }

    protected override void EndHarvest()
    {
        base.EndHarvest();
        for (int i = 0; i < dropFruits.Length; i++)
        {
            dropFruits[i].GetComponent<ItemDrop>().enabled = true;
            dropFruits[i].isKinematic = false;
        }

        for (int i = 0; i < cosmeticFruits.Length; i++)
        {
            cosmeticFruits[i].SetActive(false);
        }
    }
}