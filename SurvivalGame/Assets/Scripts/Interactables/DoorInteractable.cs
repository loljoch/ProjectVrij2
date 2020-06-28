using UnityEngine;
using System;
using System.Collections;
using HighlightPlus;
using UnityEngine.SceneManagement;

public class DoorInteractable : MonoBehaviour, IInteractable
{
	[SerializeField] private HighlightEffect highlightEffect;
	[SerializeField] private string useName;
	[SerializeField] private string interactionType;
	[SerializeField] private float holdTime;

	[Header("SceneManagment")]
	[SerializeField] private int sceneIndex;


	public string UseName => useName;

	public string InteractionType => interactionType;

	public float HoldTime => holdTime;

	public bool HighLighted { set => highlightEffect.highlighted = value; }

	public void Interact()
	{
		// Use a coroutine to load the Scene in the background
		StartCoroutine(LoadYourAsyncScene());
	}

	IEnumerator LoadYourAsyncScene()
	{
		// The Application loads the Scene in the background as the current Scene runs.
		// This is particularly good for creating loading screens.
		// You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
		// a sceneBuildIndex of 1 as shown in Build Settings.

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}