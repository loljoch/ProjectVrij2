using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Settings: ")]
	[SerializeField] private float moveSpeed = 10f;

	private Rigidbody rigid;
	private Vector3 movement;
	private Vector3 mousePos;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		movement.x = Input.GetAxis("Horizontal");
		movement.z = Input.GetAxis("Vertical");
	}

	private void FixedUpdate()
	{
		rigid.MovePosition(rigid.position + movement * moveSpeed * Time.deltaTime);
	}	
}