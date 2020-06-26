using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
	[Header("Development Only")]
	[SerializeField] private bool isIntroActive = true;

	[Header("Variables")]
	[SerializeField] private Animator anim = null;
	[SerializeField] private GameObject optionsCanvas = null;
	[SerializeField] private GameObject menuCanvas = null;
	[SerializeField] private GameObject subMenu = null;

	private bool isGraphicsOptionsActive = false;

	private void Awake()
	{
		optionsCanvas.SetActive(false);
		subMenu.SetActive(false);

		if (isIntroActive)
		{
			anim.SetTrigger("PlayIntroCamAnimation");
		}
		else
		{
			anim.SetTrigger("PlayIdleCamAnimation");
		}
	}

	public void NewGame(int _SceneIndex)
	{
		SceneManager.LoadScene(_SceneIndex);
		//LoadsceneASync with loadingScreen
	}

	public void PlayGame()
	{
		menuCanvas.SetActive(false);
		subMenu.SetActive(true);
	}

	public void Options()
	{
		optionsCanvas.SetActive(true);
		anim.SetTrigger("PlaySwitchCanvasAnimation");
	}

	public void SubMenuToMenu()
	{
		subMenu.SetActive(false);
		menuCanvas.SetActive(true);
	}

	public void BackToMenu()
	{
		anim.SetTrigger("PlaySwitchCanvasAnimation");
	}

	public void GraphicsOptions(GameObject _graphicsOptions)
	{
		isGraphicsOptionsActive = !isGraphicsOptionsActive;
		_graphicsOptions.SetActive(isGraphicsOptionsActive);
	}

	public void Quit()
	{
		Application.Quit();
	}
}