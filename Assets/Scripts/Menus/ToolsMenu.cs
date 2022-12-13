using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ToolsMenu : Menu
    {
        public MenuIcon colorPickingIcon { get; set; } = new ToolsMenuIcon(GameObject.Find("Color Picking"), GameManager.Instance.ActionsData.Selecting, MenuManager.Instance.ParametersMenusData.ColorPickingParametersMenu);
        public MenuIcon selectingIcon { get; set; } = new IconWithoutParametersMenu(GameObject.Find("Selecting"), GameManager.Instance.ActionsData.Selecting);
        public MenuIcon lineDrawingIcon { get; set; } = new ToolsMenuIcon(GameObject.Find("Line Drawing"), GameManager.Instance.ActionsData.LineDrawing, MenuManager.Instance.ParametersMenusData.LineDrawingParametersMenu);
        public MenuIcon erasingIcon { get; set; } = new IconWithoutParametersMenu(GameObject.Find("Erasing"), GameManager.Instance.ActionsData.Erasing);
        public MenuIcon objectSelectingIcon { get; set; } = new ToolsMenuIcon(GameObject.Find("Object Selecting"), GameManager.Instance.ActionsData.ObjectSelecting, MenuManager.Instance.ParametersMenusData.ObjectSelectingParametersMenu);
        public MenuIcon savingLoadingIcon { get; set; } = new ToolsMenuIcon(GameObject.Find("Saving Loading"), GameManager.Instance.ActionsData.Selecting, MenuManager.Instance.ParametersMenusData.SavingLoadingParametersMenu);
        public MenuIcon clearSceneIcon { get; set; } = new ClearSceneIcon(GameObject.Find("Clear Scene"), GameManager.Instance.ActionsData.Selecting);
		public MenuIcon objestAddingIcon { get; set;} = new ToolsMenuIcon(GameObject.Find("Object Adding"), GameManager.Instance.ActionsData.ObjectAdding, MenuManager.Instance.ParametersMenusData.ObjectAddingParametersMenu);
		public MenuIcon skyboxSelectingIcon { get; set; } = new ToolsMenuIcon(GameObject.Find("Skybox Selecting"), GameManager.Instance.ActionsData.Selecting, MenuManager.Instance.ParametersMenusData.SkyboxSelectingParametersMenu);

        public ToolsMenu()
        {
            icons = new List<MenuIcon> {
                colorPickingIcon,
                selectingIcon,
                lineDrawingIcon,
                erasingIcon,
                objectSelectingIcon,
                savingLoadingIcon,
                clearSceneIcon,
				objestAddingIcon,
                skyboxSelectingIcon
            };
        }
    }
}