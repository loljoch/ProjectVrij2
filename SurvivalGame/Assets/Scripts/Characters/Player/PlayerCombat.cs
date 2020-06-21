using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Equipment))]
public class PlayerCombat : BaseCombat
{
	public static System.Action<int> HealingPlayerEvent;

	[Header("Player Settings: ")]
	[SerializeField] private List<Image> heartSprites = new List<Image>();
	[SerializeField] private Sprite fullHeart = null;
	[SerializeField] private Sprite brokenHeart = null;

	private Equipment equipment;

	protected override int Damage => equipment.weapon.damage;
	protected override float AttackRange => equipment.weapon.attackRange;
	protected override float AttackInterval => equipment.weapon.attackInterval;
	protected override Transform AttackFrom => equipment.current3dWeapon.transform;

	protected override void Awake()
	{
		equipment = GetComponent<Equipment>();
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

	protected override void OnDrawGizmosSelected()
	{
		if(equipment == null)
		{
			equipment = GetComponent<Equipment>();
		}
		base.OnDrawGizmosSelected();
	}

	private void OnEnable()
	{
		HealingPlayerEvent += Heal;
	}

	private void OnDisable()
	{
		HealingPlayerEvent -= Heal;
	}


}