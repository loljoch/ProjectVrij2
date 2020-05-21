using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{

	[Header("Settings: ")]
	[SerializeField] private float maxHealth = 100f;



	private float currentHealth;

	private void Start()
    {
		currentHealth = maxHealth;
    }

	private void DeathState()
	{
		if(currentHealth <= 0)
		{
			Debug.Log("Player Death");

			Destroy(gameObject);
		}
	}
}