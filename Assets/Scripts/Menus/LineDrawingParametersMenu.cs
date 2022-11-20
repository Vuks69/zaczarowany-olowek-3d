using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class LineDrawingParametersMenu : ParametersMenu
    {
        private readonly Slider lineThicknessSlider = new Slider(GameObject.Find("Line Thickness Slider"), GameManager.Instance.ActionsData.Selecting);

        public LineDrawingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { lineThicknessSlider };
        }
    }
}