using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementActions : MonoBehaviour
{
	public void OnMove(InputAction.CallbackContext _context)
	{
		Debug.Log("Moving!");
	}

	public void OnFire(InputAction.CallbackContext _context)
	{
		Debug.Log("Firing!");
	}
}