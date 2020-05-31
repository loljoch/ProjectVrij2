using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsMouse : MonoBehaviour
{
	public Camera cam;

	void Start()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		//float X = Input.GetAxis("Mouse X") * 10;
		//float Y = Input.GetAxis("Mouse Y") * 10;

		//transform.Rotate(0, X, 0); // Player rotates on Y axis, your Cam is child, then rotates too
	}

	void OnGUI()
	{
		Vector3 point = new Vector3();
		Event currentEvent = Event.current;
		Vector2 mousePos = new Vector2();

		// Get the mouse position from Event.
		// Note that the y position from Event is inverted.
		mousePos.x = currentEvent.mousePosition.x;
		mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

		point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

		GUILayout.BeginArea(new Rect(20, 20, 250, 120));
		GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
		GUILayout.Label("Mouse position: " + mousePos);
		GUILayout.Label("World position: " + point.ToString("F3"));
		GUILayout.EndArea();
	}
}