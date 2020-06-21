using Extensions.Generics.Singleton;
using TMPro;
using UnityEngine;

public class ToolTip : GenericSingleton<ToolTip, ToolTip>
{
    [SerializeField] private TextMeshProUGUI textField;
    private int cItemID = -1;

    protected override void Awake()
    {
        
    }

    public void Show(int itemId, Vector3 pos)
    {
        if(cItemID != itemId)
        {
            textField.text = ItemInformation.itemsById[itemId].name;
            cItemID = itemId;
        }
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
