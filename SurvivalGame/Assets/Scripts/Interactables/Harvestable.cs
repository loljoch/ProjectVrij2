using UnityEngine;

public abstract class Harvestable : MonoBehaviour, IInteractable
{
    [SerializeField] protected GameObject dropPrefab;
    [SerializeField] protected float harvestTime = 2f;
    private float cTime = 0;
    private bool isHarvesting = false;

    public string UseName => useName;
    [SerializeField] protected string useName;

    public void Interact()
    {
        StartHarvesting();
    }

    private void Update()
    {
        if (!isHarvesting) return;

        if (Input.GetKeyUp(KeyCode.F))
        {
            isHarvesting = false;
            return;
        }


        if(cTime < harvestTime)
        {
            cTime += Time.deltaTime;
        } else
        {
            cTime = 0;
            isHarvesting = false;
            EndHarvest();
        }
    }

    protected virtual void StartHarvesting()
    {
        isHarvesting = true;
    }

    protected abstract void EndHarvest();
}
