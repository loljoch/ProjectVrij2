using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : BaseCombat
{
	public static System.Action<int> HealingPlayerEvent;

	[Header("Player Settings: ")]
	[SerializeField] private List<Image> heartSprites = new List<Image>();
	[SerializeField] private Sprite fullHeart = null;
	[SerializeField] private Sprite brokenHeart = null;

	protected override void Awake()
	{
		VirtualController.Instance.AttackActionPerformed += () => TryAttack();
		base.Awake();
	}

	private void ChangeSpriteBasedOnLives()
	{
		for (int i = 0; i < heartSprites.Count; i++)
		{
			if (i < currentHealth)
			{
				heartSprites[i].sprite = fullHeart;
			}
			else
			{
				heartSprites[i].sprite = brokenHeart;
			}

			if (i < currentHealth)
			{
				heartSprites[i].enabled = true;
			}

			//Als je de sprites wilt disabelen.
			/* 
			else
			{
				heartSprites[i].enabled = false;
			}
			*/
		}
	}

	#region HealthFunctions
	protected override void ChangeHealth(int _amount)
	{
		if (_amount == 0) return;

		base.ChangeHealth(_amount);
		ChangeSpriteBasedOnLives();
	}

	protected override void OnDeath()
	{
		Debug.Log("Player Death");
		Destroy(gameObject, 1f);
	}
    #endregion

    private void OnEnable()
	{
		HealingPlayerEvent += Heal;
	}

	private void OnDisable()
	{
		HealingPlayerEvent -= Heal;
	}


}