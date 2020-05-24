using TMPro;
using UnityEngine;

public class InteractableText : MonoBehaviour
{
    private const string PRESS_SUFFIX = "Press ";
    private const string HOLD_SUFFIX = "Press and hold ";
    private const string INTERACT_TEXT = "F to ";
    
    [SerializeField] private TextMeshProUGUI textField;

    private void Awake()
    {
        Player p = FindObjectOfType<Player>();
        p.OnFindInteractable += Show;
        p.OnLostInteractable += Hide;

        gameObject.SetActive(false);
    }

    public void Show(IInteractable obj)
    {
        string suffix = (obj.HoldTime > 0) ? HOLD_SUFFIX : PRESS_SUFFIX;
        textField.text = suffix + INTERACT_TEXT + obj.InteractionType + obj.UseName;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
