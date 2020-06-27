using Extensions.Vector;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
	[Header("Audio settings: ")]
	[FMODUnity.EventRef]
	[SerializeField] private string walkingSound;
	private FMOD.Studio.EventInstance walkingSoundEvent;


	private Vector3 movementDirection;

	private bool IsMoving
	{
		set
		{
			walkingSoundEvent.setParameterByName("FADE moving", (value) ? 1 : 0);
			isMoving = value;
		}
	}
	private bool isMoving = true;

	protected override void Awake()
	{
		base.Awake();
		VirtualController.Instance.MovementActionPerformed += TranslateInput;
		walkingSoundEvent = FMODUnity.RuntimeManager.CreateInstance(walkingSound);
		walkingSoundEvent.start();
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