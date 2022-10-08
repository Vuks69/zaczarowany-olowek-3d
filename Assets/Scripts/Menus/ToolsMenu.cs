using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Menus
{
    public class ToolsMenu : Menu
    {
        private readonly MenuIcon colorPickingIcon = new MenuIcon(GameObject.Find("Color Picking"), new Selecting());
        private readonly MenuIcon selectingIcon = new MenuIcon(GameObject.Find("Selecting"), new Selecting());
        private readonly MenuIcon lineDrawingIcon = new MenuIcon(GameObject.Find("Line Drawing"), new LineDrawing());

        public ToolsMenu()
        {
            icons = new List<MenuIcon> { colorPickingIcon, selectingIcon, lineDrawingIcon };
        }
    }
}