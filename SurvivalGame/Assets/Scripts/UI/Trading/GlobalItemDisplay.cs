using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalItemDisplay : ItemDisplay, IGlobalItemDisplay
{
    private static IGlobalItemDisplay instance;
    public static IGlobalItemDisplay Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GlobalItemDisplay>();
            }
            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as IGlobalItemDisplay;
        }

        gameObject.SetActive(false);
    }

    public void Show(Item item)
    {
        AssignItem(item);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        
    }
}

public interface IGlobalItemDisplay
{
    void Show(Item item);
    void Hide();
}
