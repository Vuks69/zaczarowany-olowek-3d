using UnityEngine;

namespace Assets.Scripts.MonoscopicMode
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed;
        public CharacterController controller;
        private float horizontalInput;
        private float verticalInput;

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
            var moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}