using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;

	public Color CurrentColor { get; set; }
	public GameObject currentParametersMenu;
	// public GameObject RightFlystick
	// public GameObject LeftFlystick
	// public GameObject CurrentSelection

	void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}