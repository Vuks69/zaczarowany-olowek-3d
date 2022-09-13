using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MonoscopicMode
{
	public class PlayerMovement : MonoBehaviour
	{

		[Header("Movement")]
		public float moveSpeed;

		//public Transform orientation;
		public CharacterController controller;

		float horizontalInput;
		float verticalInput;

		Vector3 moveDirection;

		// Use this for initialization
		void Start()
		{
			//controller.detectCollisions = false;
		}

		// Update is called once per frame
		void Update()
		{
			MyInput();
		}

		void FixedUpdate()
		{
			MovePlayer();
		}

		void MyInput()
		{
			horizontalInput = Input.GetAxisRaw("Horizontal");
			verticalInput = Input.GetAxisRaw("Vertical");
		}

		void MovePlayer()
		{
			moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
			controller.Move(moveDirection * moveSpeed * Time.deltaTime);
		}
	}
}