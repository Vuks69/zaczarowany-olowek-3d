using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ParametersMenusData
    {
        public ColorPickingParametersMenu ColorPicking { get; set; } = new ColorPickingParametersMenu { MenuObject = GameObject.Find("Color Picking Parameters Menu") };
    }
}