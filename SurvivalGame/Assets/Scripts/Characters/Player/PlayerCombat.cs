using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : BaseCombat
{
	public static System.Action<WeaponItem> EquipWeaponEvent;
	public static System.Action<int> HealingPlayerEvent;

	[Header("Player Settings: ")]
	[SerializeField] private Animator anim;
	[SerializeField] private List<Image> heartSprites = new List<Image>();
	[SerializeField] private Sprite fullHeart = null;
	[SerializeField] private Sprite brokenHeart = null;

	[Header("Equipment settings: ")]
	public WeaponItem weaponItem;
	[HideInInspector] public Transform current3dWeapon;
	[SerializeField] private Transform weaponHand;
	private IWeapon weapon;

	protected override float AttackInterval => weapon.AttackInterval;

	protected override void Awake()
	{
		VirtualController.Instance.AttackActionPerformed += () => TryAttack();
		EquipWeaponEvent += Equip;
		base.Awake();
	}

	private void Start()
	{
		EquipWeaponEvent?.Invoke(weaponItem);
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

	private void OnDestroy()
	{
		EquipWeaponEvent -= Equip;
	}

	public void Equip(WeaponItem weapon)
	{
		weaponItem = weapon;

		if (current3dWeapon != null)
		{
			Destroy(current3dWeapon.gameObject);
		}

		current3dWeapon = Instantiate(weapon.weaponPrefab, weaponHand).transform;
		this.weapon = current3dWeapon.GetComponent<IWeapon>();
		this.weapon.PlayerAnim = anim;
	}

	protected override void Attack()
	{
		weapon.DoAttackAnimation();
	}

	public void WeaponAttack()
	{
		weapon.Attack();
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