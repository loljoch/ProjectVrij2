using Extensions.Generics.Singleton;
using TMPro;
using UnityEngine;

public class InteractableText : GenericSingleton<InteractableText, InteractableText>
{
    [SerializeField] private TextMeshProUGUI textField;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(string name)
    {
        textField.text = $"Hold F to interact with {name}";
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
