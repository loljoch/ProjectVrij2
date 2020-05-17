using Extensions.Generics.Singleton;
using TMPro;
using UnityEngine;

public class ToolTip : GenericSingleton<ToolTip, IToolTip>, IToolTip
{
    [SerializeField] private TextMeshProUGUI textField;

    public void Show(Vector3 pos, string text)
    {
        transform.position = pos;
        textField.text = text;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}

public interface IToolTip
{
    void Show(Vector3 pos, string text);
    void Hide();
}
