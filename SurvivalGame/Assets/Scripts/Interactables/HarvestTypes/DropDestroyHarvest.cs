using UnityEngine;

public class DropDestroyHarvest : Harvestable
{
    protected override void StartHarvest()
    {
        
    }

    protected override void EndHarvest()
    {
        base.EndHarvest();
        lootDrop.Spawn(transform.position + Vector3.up);
        Destroy(gameObject);
    }
}
