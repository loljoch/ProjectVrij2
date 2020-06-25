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

	private bool IsMoving
	{
		set
		{
			if(isMoving != value)
			{
				FMODUnity.RuntimeManager.PlayOneShot((value) ? walkingSound : idleSound);
				isMoving = value;
			}
		}
	}
	private bool isMoving = true;

	protected override void Awake()
	{
		base.Awake();
		VirtualController.Instance.MovementActionPerformed += TranslateInput;
	}

	private void FixedUpdate()
	{
		IsMoving = (movementDirection != Vector3.zero);
		if (isMoving)
		{
			LerpLookDirection(movementDirection);
		}
	}

	private void TranslateInput(Vector2 input)
	{
		movementDirection = input.ToVector3XZ();
		base.Move(movementDirection);
	}
}