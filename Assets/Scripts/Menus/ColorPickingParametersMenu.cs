using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus
{
	public class ColorPickingParametersMenu : ParametersMenu
	{

		public Dictionary<MenuIcon, Color> predefinedColors = new Dictionary<MenuIcon, Color>();
		private readonly MenuIcon predefinedRedIcon = new MenuIcon(GameObject.Find("Red"), GameManager.Instance.CurrentAction) { DefaultColor = Color.red};
		private readonly MenuIcon predefinedGreenIcon = new MenuIcon(GameObject.Find("Green"), GameManager.Instance.CurrentAction) { DefaultColor = Color.green };
		private readonly MenuIcon predefinedBlueIcon = new MenuIcon(GameObject.Find("Blue"), GameManager.Instance.CurrentAction) { DefaultColor = Color.blue };
		private readonly MenuIcon predefinedWhiteIcon = new MenuIcon(GameObject.Find("White"), GameManager.Instance.CurrentAction) { DefaultColor = Color.white };
		private readonly MenuIcon predefinedBlackIcon = new MenuIcon(GameObject.Find("Black"), GameManager.Instance.CurrentAction) { DefaultColor = Color.black };
		private GameObject colorPalette = GameObject.Find("Color Palette Icon");

		public ColorPickingParametersMenu()
		{
            predefinedColors.Add(predefinedRedIcon, Color.red);
            predefinedColors.Add(predefinedGreenIcon, Color.green);
            predefinedColors.Add(predefinedBlueIcon, Color.blue);
            predefinedColors.Add(predefinedWhiteIcon, Color.white);
            predefinedColors.Add(predefinedBlackIcon, Color.black);

			this.icons.Add(predefinedRedIcon);
			this.icons.Add(predefinedGreenIcon);
			this.icons.Add(predefinedBlueIcon);
			this.icons.Add(predefinedWhiteIcon);
			this.icons.Add(predefinedBlackIcon);
        }

		Color getColorFromPalette(int x, int y)
		{
			Canvas canvas = colorPalette.GetComponent<Canvas>();
			RawImage rawImage = canvas.GetComponent<RawImage>();
			return (rawImage.texture as Texture2D).GetPixel(x, y);
		}
	}
}