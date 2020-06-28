using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class MapInterior : MonoBehaviour, IInteractable
{
	[SerializeField] private HighlightEffect highlightEffect;
	[SerializeField] private string useName;
	[SerializeField] private string interactionType;
	[SerializeField] private float holdTime;
	[SerializeField] private GameObject canvasMap;
	[SerializeField] private Player player;

	public string UseName => useName;

	public string InteractionType => interactionType;

	public float HoldTime => holdTime;

	public bool HighLighted { set => highlightEffect.highlighted = value; }

	public void Awake()
	{
		player = FindObjectOfType<Player>();
	}

	public void Interact()
	{
		player.OnLostInteractable += OnLost;
		canvasMap.SetActive(true);
	}

	private void OnLost()
	{
		player.OnLostInteractable -= OnLost;
		canvasMap.SetActive(false);
	}
}