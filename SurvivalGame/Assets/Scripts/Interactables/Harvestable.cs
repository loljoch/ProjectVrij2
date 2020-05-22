using System.Collections;
using UnityEngine;

public abstract class Harvestable : MonoBehaviour, IInteractable
{
    [SerializeField] protected GameObject dropPrefab;
    [SerializeField] protected float harvestTime = 2f;

    public void Interact()
    {
        StartHarvesting();
    }

    protected virtual void StartHarvesting()
    {
        StartCoroutine(StartHarvest());
    }

    protected abstract void EndHarvest();

    private IEnumerator StartHarvest()
    {
        float cTime = 0;

        while (cTime < harvestTime )
        {
            cTime += Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.F)) return null;
        }

        EndHarvest();

        return null;
    }
}
