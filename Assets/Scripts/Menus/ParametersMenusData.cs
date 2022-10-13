using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ParametersMenusData
    {
        public ColorPickingParametersMenu ColorPickingParametersMenu { get; set; } = new ColorPickingParametersMenu { MenuObject = GameObject.Find("Color Picking Parameters Menu") };
        public LineDrawingParametersMenu LineDrawingParametersMenu { get; set; } = new LineDrawingParametersMenu { MenuObject = GameObject.Find("Line Drawing Parameters Menu") };
    }
}