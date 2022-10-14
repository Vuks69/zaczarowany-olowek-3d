using UnityEngine;

namespace Assets.Scripts.MonoscopicMode
{
	public class CameraMovement : MonoBehaviour
	{
		public float sensX;
		public float sensY;
		public Transform orientation;
		private float xRotation;
		private float yRotation;

		void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		void Update()
		{
			if (!Input.GetButton("Move Flystick")) {
				//get mouse input
				float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
				float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

				yRotation += mouseX;
				xRotation -= mouseY;
				xRotation = Mathf.Clamp(xRotation, -90f, 90f);

				//rotate
				transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
			}
		}
	}
}