using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemSprite;

    public void AssignItem(Item item)
    {
        if (itemName != null)
        {
            itemName.text = item.name;
        }

        if (itemDescription != null)
        {
            itemDescription.text = item.description;
        }

        if (itemSprite != null)
        {
            itemSprite.sprite = item.sprite;
        }
    }
}
