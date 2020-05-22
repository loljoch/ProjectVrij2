using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector] public Resource resource = null;
    [SerializeField] private GameObject quantityBgDisplay;
    [SerializeField] private TextMeshProUGUI resourceQuantity;
    [SerializeField] private Image resourceSprite;

    private bool isActive = false;

    private void OnEnable()
    {
        if (isActive) return;

        RemoveResource();
    }

    public void AssignResource(Resource resource)
    {
        this.resource = resource;
        ShowResource();
        resource.OnChangeQuantity += ChangeQuantity;

        isActive = true;
    }

    public virtual void ShowResource()
    {
        if (resourceQuantity != null)
        {
            ChangeQuantity();
        }

        if (resourceSprite != null)
        {
            resourceSprite.enabled = true;
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
            resourceSprite.enabled = false;
        }

        if (isActive)
        {
            resource.OnChangeQuantity -= ChangeQuantity;
        }

        resource = null;
        isActive = false;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTip.Instance.Show(transform.position, resource.item.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.Instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GlobalItemDisplay.Instance.Show(resource.item);
        } else if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseMenu.Instance.Show(transform.position, resource.item);
        }
    }
}
