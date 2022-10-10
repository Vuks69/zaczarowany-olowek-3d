using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus
{
    public class LineDrawingParametersMenu : ParametersMenu
    {
        private readonly MenuIcon lineThicknessSlider = new MenuIcon(GameObject.Find("Red"), GameManager.Instance.ActionsData.LineDrawing);

        public LineDrawingParametersMenu()
        {
            icons = new List<MenuIcon> { lineThicknessSlider };
        }
    }
}