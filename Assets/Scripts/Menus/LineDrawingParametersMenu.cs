using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus
{
    public class LineDrawingParametersMenu : ParametersMenu
    {
        private readonly Slider lineThicknessSlider = new Slider(GameObject.Find("Line Thickness Slider"), GameManager.Instance.ActionsData.LineDrawing);

        public LineDrawingParametersMenu()
        {
            icons = new List<MenuIcon> { lineThicknessSlider };
        }
    }
}