using System.Collections;
using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        public ToolsMenu ToolsMenu { get; set; }
        public ParametersMenu ParametersMenu { get; set; }

        public ParametersMenusData ParametersMenusData { get; set; }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            ParametersMenusData = new ParametersMenusData();
            ParametersMenu = ParametersMenusData.ColorPickingParametersMenu;
            ToolsMenu = new ToolsMenu();
            ToolsMenu.SelectedIcon = ToolsMenu.selectingIcon;
            ToolsMenu.SelectedIcon.SetSelectedColor();
        }
    }
}