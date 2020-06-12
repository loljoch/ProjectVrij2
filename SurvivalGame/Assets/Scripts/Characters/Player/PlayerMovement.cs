using Extensions.Vector;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
	private Vector3 movementDirection;

	protected override void Awake()
	{
		base.Awake();
		VirtualController.Instance.MovementActionPerformed += TranslateInput;
	}

	private void FixedUpdate()
	{
		if (movementDirection != Vector3.zero)
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