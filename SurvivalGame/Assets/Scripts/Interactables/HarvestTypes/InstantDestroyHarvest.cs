using UnityEngine;

public class InstantDestroyHarvest : Harvestable
{

    [FMODUnity.EventRef]
    [SerializeField] private string endHarvestSFX;

    public override void Interact()
    {
        lootDrop.Give();
        FMODUnity.RuntimeManager.PlayOneShot(endHarvestSFX, transform.position);
        Destroy(gameObject);
    }

    protected override void StartHarvest()
    {
        throw new System.NotImplementedException();
    }

    private void OnValidate()
    {
        interactTime = 0;
    }
}
