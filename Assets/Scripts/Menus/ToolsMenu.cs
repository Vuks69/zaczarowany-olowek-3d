using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus
{
    public class ToolsMenu : Menu
    {
        public MenuIcon colorPickingIcon = new MenuIcon(GameObject.Find("Color Picking"), GameManager.Instance.ActionsData.Selecting);
        public MenuIcon selectingIcon = new MenuIcon(GameObject.Find("Selecting"), GameManager.Instance.ActionsData.Selecting);
        public MenuIcon lineDrawingIcon = new MenuIcon(GameObject.Find("Line Drawing"), GameManager.Instance.ActionsData.LineDrawing);

        public ToolsMenu()
        {
            icons = new List<MenuIcon> { colorPickingIcon, selectingIcon, lineDrawingIcon };
        }
    }
}