using UnityEngine;

public class DropDestroyHarvest : Harvestable
{
    [FMODUnity.EventRef]
    [SerializeField] private string finishHarvestSFX;


    protected override void StartHarvest()
    {
        
    }

    protected override void EndHarvest()
    {
        base.EndHarvest();
        lootDrop.Spawn(transform.position + Vector3.up);
        if(finishHarvestSFX != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(finishHarvestSFX, transform.position);
        }
        Destroy(gameObject);
    }
}
