using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class LineDrawingParametersMenu : ParametersMenu
    {
        private readonly LineThicknessSlider lineThicknessSlider =
            new LineThicknessSlider(GameObject.Find("Line Thickness Slider"), GameManager.Instance.ActionsData.Selecting);
        private readonly LineTypeMenuIcon rectangleLineIcon =
            new LineTypeMenuIcon(GameObject.Find("Rectangle Line Icon"), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.Rectangle);
        private readonly LineTypeMenuIcon triangleLineIcon =
            new LineTypeMenuIcon(GameObject.Find("Triangle Line Icon"), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.Triangle);
        private readonly LineTypeMenuIcon starLineIcon =
            new LineTypeMenuIcon(GameObject.Find("Star Line Icon"), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.Star);
        private readonly LineTypeMenuIcon circleLineIcon =
            new LineTypeMenuIcon(GameObject.Find("Circle Line Icon"), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.Circle);
        private readonly LineTypeMenuIcon plusLineIcon =
            new LineTypeMenuIcon(GameObject.Find("Plus Line Icon"), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.Plus);

        public LineDrawingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { lineThicknessSlider, rectangleLineIcon, circleLineIcon, starLineIcon, triangleLineIcon, plusLineIcon };
        }
    }
}