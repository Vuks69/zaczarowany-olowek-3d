using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlystickMovement : MonoBehaviour {

	public float sens;
	Transform camTransform;

	float mouseX;
	float mouseY;
	float mouseZ;
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Move Flystick")) {
			GetMouseInput ();
			MoveFlystick ();
		}
		
	}

	void GetMouseInput(){
		mouseX = Input.GetAxisRaw ("Mouse X")  * Time.deltaTime * sens;
		mouseY = Input.GetAxisRaw ("Mouse Y")  * Time.deltaTime * sens;
		mouseZ = Input.GetAxisRaw ("Mouse ScrollWheel")  * Time.deltaTime * sens;
	}

	void MoveFlystick(){
		//Vector3 camPosition = new Vector3 (camTransform.position.x, camTransform.position.y, camTransform.position.z);
		//Vector3 directionForward = (transform.position - camPosition).normalized;
		camTransform = GameObject.FindWithTag("Player").transform;
		Vector3 forwardMovement = camTransform.forward * mouseZ;
		Vector3 horizontalMovement = camTransform.right * mouseX;
		Vector3 verticalMovement = camTransform.up * mouseY;

		/*Vector3 forwardMovement = transform.forward * mouseZ;
		Vector3 horizontalMovement = transform.right * mouseX;
		Vector3 verticalMovement = transform.up * mouseY;*/
		Vector3 movement = Vector3.ClampMagnitude (forwardMovement + horizontalMovement + verticalMovement, 1);
		transform.Translate (movement, Space.World);
	}



}
