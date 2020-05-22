using UnityEngine;

public class CoralPlant : Harvestable
{
    protected override void EndHarvest()
    {
        Instantiate(dropPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
