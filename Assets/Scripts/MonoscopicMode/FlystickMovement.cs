using UnityEngine;

namespace Assets.Scripts.MonoscopicMode
{
    public class FlystickMovement : MonoBehaviour
    {
        public float sens;
        private float mouseX;
        private float mouseY;
        private float mouseZ;

        void Update()
        {
            if (Input.GetButton("Move Flystick"))
            {
                GetMouseInput();
                MoveFlystick();
            }
        }

        void GetMouseInput()
        {
            mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
            mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;
            mouseZ = Input.GetAxisRaw("Mouse ScrollWheel") * Time.deltaTime * sens;
        }

        void MoveFlystick()
        {
            var camTransform = GameObject.FindWithTag("Player").transform;
            Vector3 forwardMovement = camTransform.forward * mouseZ;
            Vector3 horizontalMovement = camTransform.right * mouseX;
            Vector3 verticalMovement = camTransform.up * mouseY;

            Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement + verticalMovement, 1);
            transform.Translate(movement, Space.World);
        }
    }
}