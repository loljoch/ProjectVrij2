using UnityEngine;

public class CoralPlant : Harvestable
{
    protected override void StartHarvest()
    {
        
    }

    protected override void EndHarvest()
    {
        base.EndHarvest();
        Instantiate(dropPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
