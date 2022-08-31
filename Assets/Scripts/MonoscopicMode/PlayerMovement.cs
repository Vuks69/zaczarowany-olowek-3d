using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MonoscopicMode
{
	public class PlayerMovement : MonoBehaviour
	{

		[Header("Movement")]
		public float moveSpeed;

		public Transform orientation;

		float horizontalInput;
		float verticalInput;

		Vector3 moveDirection;

		// Use this for initialization
		void Start()
		{
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
			moveDirection = orientation.forward * verticalInput * -1 + orientation.right * horizontalInput * -1;
			transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
		}
	}
}