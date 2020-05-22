using Extensions.Generics.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class UseMenu : GenericSingleton<UseMenu, IUseMenu>, IUseMenu
{
    [SerializeField] private Button equipButton;
    [SerializeField] private Button eatButton;

    public void Show(Vector3 pos, Item item)
    {
        Hide();

        transform.position = pos;
        if(item.useCases == Uses.Equipment)
        {
            equipButton.gameObject.SetActive(true);
        }

        if(item.useCases == Uses.Food)
        {
            eatButton.gameObject.SetActive(true);
        }


        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        eatButton.gameObject.SetActive(false);
    }
}

public interface IUseMenu
{
    void Show(Vector3 pos, Item item);
    void Hide();
}
