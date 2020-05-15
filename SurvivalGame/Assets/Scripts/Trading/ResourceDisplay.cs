using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] private GameObject quantityBgDisplay;
    [SerializeField] private TextMeshProUGUI resourceQuantity;
    [SerializeField] private Image resourceSprite;
    [HideInInspector] public Resource resource = null;

    public void AssignResource(Resource resource)
    {
        this.resource = resource;
        ShowResource();
        resource.OnChangeQuantity += ChangeQuantity;
    }

    public virtual void ShowResource()
    {
        if (resourceQuantity != null)
        {
            ChangeQuantity();
        }

        if (resourceSprite != null)
        {
            resourceSprite.sprite = resource?.item.sprite;
        }
    }

    public void RemoveResource()
    {
        if (resourceQuantity != null)
        {
            quantityBgDisplay.SetActive(false);
            resourceQuantity.text = string.Empty;
        }

        if (resourceSprite != null)
        {
            resourceSprite.sprite = null;
        }

        resource = null;
    }

    private void ChangeQuantity()
    {
        if (resource.Quantity == 1)
        {
            quantityBgDisplay.SetActive(false);
        } else
        {
            quantityBgDisplay.SetActive(true);
            resourceQuantity.text = resource.Quantity.ToString();
        }
    }
}
