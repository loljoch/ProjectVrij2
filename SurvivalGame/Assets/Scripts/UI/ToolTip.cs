using Extensions.Generics.Singleton;
using TMPro;
using UnityEngine;

public class ToolTip : GenericSingleton<ToolTip, ToolTip>
{
    [SerializeField] private TextMeshProUGUI textField;
    private int cItemID;

    public void Show(int itemId, Vector3 pos)
    {
        if(cItemID != itemId)
        {
            textField.text = UIManager.Instance.itemInformation.itemsById[itemId].name;
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
