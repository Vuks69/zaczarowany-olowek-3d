using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus
{
    public class ToolsMenu : Menu
    {
        public MenuIcon colorPickingIcon { get; set; } = new ToolsMenuIcon(GameObject.Find("Color Picking"), GameManager.Instance.ActionsData.Selecting, MenuManager.Instance.ParametersMenusData.ColorPickingParametersMenu);
        public MenuIcon selectingIcon { get; set; } = new IconWithoutParametersMenu(GameObject.Find("Selecting"), GameManager.Instance.ActionsData.Selecting);
        public MenuIcon lineDrawingIcon { get; set; } = new ToolsMenuIcon(GameObject.Find("Line Drawing"), GameManager.Instance.ActionsData.LineDrawing, MenuManager.Instance.ParametersMenusData.LineDrawingParametersMenu);
        public MenuIcon erasingIcon { get; set; } = new IconWithoutParametersMenu(GameObject.Find("Erasing"), GameManager.Instance.ActionsData.Erasing);
        public MenuIcon objectSelecting { get; set; } = new ToolsMenuIcon(GameObject.Find("Object Selecting"), GameManager.Instance.ActionsData.ObjectSelecting, MenuManager.Instance.ParametersMenusData.ObjectSelectingParametersMenu);

        public ToolsMenu()
        {
            icons = new List<MenuIcon> { colorPickingIcon, selectingIcon, lineDrawingIcon, erasingIcon, objectSelecting };
        }
    }
}