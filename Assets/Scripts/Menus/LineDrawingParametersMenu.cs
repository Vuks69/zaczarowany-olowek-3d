using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
	public class LineDrawingParametersMenu : ParametersMenu
	{
		private readonly Slider lineThicknessSlider = new Slider(GameObject.Find("Line Thickness Slider"), GameManager.Instance.ActionsData.Selecting);
		private readonly LineTypeMenuIcon lineRendererIcon = new LineTypeMenuIcon (GameObject.Find ("LineRenderer Icon"), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.LineRenderer);
		private readonly LineTypeMenuIcon cylinderIcon = new LineTypeMenuIcon (GameObject.Find ("Cylinder Line Icon"), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.Cylinder);
		private readonly LineTypeMenuIcon cubeIcon = new LineTypeMenuIcon (GameObject.Find ("Cube Line Icon"), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.Cube);


		public LineDrawingParametersMenu(GameObject gameObject) : base(gameObject)
		{
			icons = new List<MenuIcon> { lineThicknessSlider, lineRendererIcon, cylinderIcon, cubeIcon };
		}
	}
}