using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickingParametersMenuBehaviour : MonoBehaviour {

	public Dictionary<GameObject, Color> predefinedColors = new Dictionary<GameObject, Color>();
	public GameObject predefinedRed;
	public GameObject predefinedGreen;
	public GameObject predefinedBlue;
	public GameObject predefinedWhite;
	public GameObject predefinedBlack;
	public GameObject colorPalette;

	// Use this for initialization
	void Start () {
		predefinedColors.Add(predefinedRed, Color.red);
		predefinedColors.Add(predefinedGreen, Color.green);
		predefinedColors.Add(predefinedBlue, Color.blue);
		predefinedColors.Add(predefinedWhite, Color.white);
		predefinedColors.Add(predefinedBlack, Color.black);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Color getColorFromPalette(int x, int y)
    {
		Canvas canvas = colorPalette.GetComponent<Canvas>();
		RawImage rawImage = canvas.GetComponent<RawImage>();
		return (rawImage.texture as Texture2D).GetPixel(x, y);
    }
}
