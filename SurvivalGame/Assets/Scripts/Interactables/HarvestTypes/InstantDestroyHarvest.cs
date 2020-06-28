using UnityEngine;

public class InstantDestroyHarvest : Harvestable
{
    public override void Interact()
    {
        lootDrop.Give();
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
