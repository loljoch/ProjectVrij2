using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseMovement : MonoBehaviour
{
	[Header("Settings: ")]
	[SerializeField] protected float moveSpeed = 10f;
	[SerializeField] protected float smoothDampRotation = 0.15f;

	protected Rigidbody rigid;

	protected virtual void Awake()
	{
		rigid = GetComponent<Rigidbody>();
	}

	public virtual void LerpLookDirection(Vector3 wantedDirection)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(wantedDirection), smoothDampRotation);
	}

	public virtual void Move(Vector3 direction)
	{
		rigid.velocity = direction * moveSpeed;
	}
}
