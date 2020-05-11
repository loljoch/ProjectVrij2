using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Info: https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{

	[Header("Movement")]
	[SerializeField] private float speed = 6.0F;
	[SerializeField] private float rotateSpeed = 6;
	private float gravity = 9.8f;
	private Vector3 moveDirection = Vector3.zero;

	private CharacterController controller;

	private void Awake()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
		MoveObject();	
	}

	private void MoveObject()
	{
		if (controller.isGrounded)
		{
			moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}