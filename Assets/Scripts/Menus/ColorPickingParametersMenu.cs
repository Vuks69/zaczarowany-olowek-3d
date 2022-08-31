using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Menus
{
	public class ColorPickingParametersMenu : ParametersMenu
	{

		public Dictionary<GameObject, Color> predefinedColors = new Dictionary<GameObject, Color>();
		public GameObject predefinedRedIcon;
		public GameObject predefinedGreenIcon;
		public GameObject predefinedBlueIcon;
		public GameObject predefinedWhiteIcon;
		public GameObject predefinedBlackIcon;
		public GameObject colorPalette;

		// Use this for initialization
		public ColorPickingParametersMenu()
		{
			predefinedColors.Add(predefinedRedIcon, Color.red);
			predefinedColors.Add(predefinedGreenIcon, Color.green);
			predefinedColors.Add(predefinedBlueIcon, Color.blue);
			predefinedColors.Add(predefinedWhiteIcon, Color.white);
			predefinedColors.Add(predefinedBlackIcon, Color.black);
		}

		Color getColorFromPalette(int x, int y)
		{
			Canvas canvas = colorPalette.GetComponent<Canvas>();
			RawImage rawImage = canvas.GetComponent<RawImage>();
			return (rawImage.texture as Texture2D).GetPixel(x, y);
		}
	}
}