using EasyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : BaseCombat
{
	public static System.Action<WeaponItem> EquipWeaponEvent;
	public static System.Action<int> HealingPlayerEvent;
	public static System.Action<int> OnChangeHpPlayerEvent;

	[Header("Audio settings: ")]
	[FMODUnity.EventRef]
	[SerializeField] private string attackGruntSound;
	[FMODUnity.EventRef]
	[SerializeField] private string healSound;
	[FMODUnity.EventRef]
	[SerializeField] private string onhitSound;

	[Header("Player Settings: ")]
	[SerializeField] private Animator anim;
	[SerializeField] private List<Image> heartSprites = new List<Image>();
	[SerializeField] private Sprite fullHeart = null;
	[SerializeField] private Sprite halfHeart = null;
	[SerializeField] private Sprite emptyHeart = null;

	[Header("Equipment settings: ")]
	public WeaponItem weaponItem;
	[HideInInspector] public Transform current3dWeapon;
	[SerializeField] private Transform weaponHand;
	private IWeapon weapon;

	protected override float AttackInterval => weapon.AttackInterval;

	protected override void Awake()
	{
		OnChangeHpPlayerEvent += x => ChangeSpriteBasedOnLives();
		VirtualController.Instance.AttackActionPerformed += () => TryAttack();
		EquipWeaponEvent += Equip;
		base.Awake();
		ChangeSpriteBasedOnLives();
	}

	[Button]
	private void FooTakeDamage()
	{
		base.TakeDamage(1);
	}

	private void Start()
	{
		EquipWeaponEvent?.Invoke(weaponItem);
	}

	private void ChangeSpriteBasedOnLives()
	{
		int fullHearts = currentHealth / 2;
		int halfHearts = currentHealth % 2;

		int index = 0;

		for (int i = 0; i < fullHearts; i++)
		{
			heartSprites[i].sprite = fullHeart;
			index++;
		}

		if (halfHearts == 1)
		{
			heartSprites[index].sprite = halfHeart;
			index++;
		}

		for (int i = index; i < heartSprites.Count; i++)
		{
			heartSprites[i].sprite = emptyHeart;
		}
	}

	private void OnDestroy()
	{
		OnChangeHpPlayerEvent -= x => ChangeSpriteBasedOnLives();
		EquipWeaponEvent -= Equip;
	}

    #region EquipmentFunctions
	[EasyAttributes.Button]
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

    #endregion


    #region CombatFunctions
    protected override void Attack()
	{
		FMODUnity.RuntimeManager.PlayOneShot(attackGruntSound, transform.position);
		weapon.DoAttackAnimation();
	}

	public void WeaponAttack()
	{
		weapon.Attack();
	}
    #endregion

    #region HealthFunctions
    protected override void ChangeHealth(int _amount)
	{
		if (_amount == 0) return;

		base.ChangeHealth(_amount);
		FMODUnity.RuntimeManager.PlayOneShot((_amount < 0) ? attackGruntSound : healSound, transform.position);
		
		OnChangeHpPlayerEvent?.Invoke(_amount);
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