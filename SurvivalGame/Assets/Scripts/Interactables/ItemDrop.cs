using System.Collections;
using UnityEngine;

public class ItemDrop : MonoBehaviour, IItemDrop
{
    private const float DROP_TIMER = 1.5f;
    private const float DISAPPEAR_TIMER = 300;
    private const ushort PICK_UP_SPEED = 10;

    [SerializeField] private int itemID;
    private bool CanPickUp 
    {
        get => (!pickedUp && currentTime >= DROP_TIMER && UIManager.Instance.inventory.AddItem(itemID));
    }
    private bool pickedUp = false;
    private float currentTime = 0;

    [FMODUnity.EventRef]
    [SerializeField] private string itemDropSFX;

    private void Awake()
    {
        FMODUnity.RuntimeManager.PlayOneShot(itemDropSFX, transform.position);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > DISAPPEAR_TIMER)
        {
            Destroy(gameObject);
        }
    }

    public void PickUp(Transform player)
    {
        if (!CanPickUp) return;

        pickedUp = true;
        StartCoroutine(GoToPlayer(player));
    }

    private IEnumerator GoToPlayer(Transform player)
    {
        float time = Time.time;

        Vector3 oldPos = transform.position;

        while (Vector3.Distance(transform.position, player.position) > 1)
        {
            transform.position = Vector3.Lerp(oldPos, player.position, (Time.time - time) * PICK_UP_SPEED);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}

public interface IItemDrop
{
    void PickUp(Transform player);
}
