using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MonoscopicMode
{
	public class CameraMovement : MonoBehaviour
	{

		public float sensX;
		public float sensY;

		public Transform orientation;

		float xRotation;
		float yRotation;


		// Use this for initialization
		void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			//transform.rotation = Quaternion.Euler(0, 270f, 0);
		}

		// Update is called once per frame
		void Update()
		{
			//get mouse input
			float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
			float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

			yRotation += mouseX;

			xRotation -= mouseY;
			xRotation = Mathf.Clamp(xRotation, -90f, 90f);

			//rotate cam and orientation
			transform.rotation = Quaternion.Euler(0, yRotation, xRotation);
			orientation.rotation = Quaternion.Euler(0, yRotation, xRotation);
		}
	}
}