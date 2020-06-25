using Extensions.Vector;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
	[Header("Audio settings: ")]
	[FMODUnity.EventRef]
	[SerializeField] private string walkingSound;
	[FMODUnity.EventRef]
	[SerializeField] private string idleSound;


	private Vector3 movementDirection;

	private bool IsMoving => (movementDirection != Vector3.zero);

	protected override void Awake()
	{
		base.Awake();
		VirtualController.Instance.MovementActionPerformed += TranslateInput;
	}

	private void Start()
	{
		InvokeRepeating("CallFootsteps", 0, (moveSpeed/30));
	}

	private void FixedUpdate()
	{
		if (IsMoving)
		{
			LerpLookDirection(movementDirection);
		}
	}

	private void TranslateInput(Vector2 input)
	{
		movementDirection = input.ToVector3XZ();
		base.Move(movementDirection);
	}

	private void CallFootsteps()
	{
		if (IsMoving)
		{
			FMODUnity.RuntimeManager.PlayOneShot(walkingSound, transform.position);
		} else
		{
			FMODUnity.RuntimeManager.PlayOneShot(idleSound, transform.position);
		}
	}
}