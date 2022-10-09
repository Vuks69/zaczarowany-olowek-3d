using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.Actions;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;

namespace Assets.Scripts.Menus
{
    public class ColorPickingParametersMenu : ParametersMenu
    {
        private readonly ColorPickingMenuIcon predefinedRedIcon     = new ColorPickingMenuIcon(GameObject.Find("Red"), GameManager.Instance.ActionsData.Selecting, Color.red) { DefaultColor = Color.red };
        private readonly ColorPickingMenuIcon predefinedGreenIcon   = new ColorPickingMenuIcon(GameObject.Find("Green"), GameManager.Instance.ActionsData.Selecting, Color.green) { DefaultColor = Color.green };
        private readonly ColorPickingMenuIcon predefinedBlueIcon    = new ColorPickingMenuIcon(GameObject.Find("Blue"), GameManager.Instance.ActionsData.Selecting, Color.blue) { DefaultColor = Color.blue };
        private readonly ColorPickingMenuIcon predefinedWhiteIcon   = new ColorPickingMenuIcon(GameObject.Find("White"), GameManager.Instance.ActionsData.Selecting, Color.white) { DefaultColor = Color.white };
        private readonly ColorPickingMenuIcon predefinedBlackIcon   = new ColorPickingMenuIcon(GameObject.Find("Black"), GameManager.Instance.ActionsData.Selecting, Color.black) { DefaultColor = Color.black };
        private MenuIcon colorPalette = new MenuIcon(GameObject.Find("Color Palette Icon"), GameManager.Instance.ActionsData.Selecting);

        public ColorPickingParametersMenu()
        {
            icons = new List<MenuIcon> { predefinedRedIcon, predefinedGreenIcon, predefinedBlueIcon, predefinedWhiteIcon, predefinedBlackIcon };
        }

        Color getColorFromPalette(int x, int y)
        {
            Canvas canvas = colorPalette.gameObject.GetComponent<Canvas>();
            RawImage rawImage = canvas.GetComponent<RawImage>();
            return (rawImage.texture as Texture2D).GetPixel(x, y);
        }
    }
}