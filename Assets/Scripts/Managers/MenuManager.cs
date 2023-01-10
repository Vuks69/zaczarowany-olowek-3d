using Assets.Scripts.Menus;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        public ToolsMenu ToolsMenu { get; set; }
        public ParametersMenu ParametersMenu { get; set; }
        public ParametersMenusData ParametersMenusData { get; set; }
        public Vector3 StartToolsMenuTransformPosition { get; set; }
        public Quaternion StartToolsMenuTransformRotation { get; set; }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            ParametersMenusData = new ParametersMenusData();
            ParametersMenu = ParametersMenusData.ColorPickingParametersMenu;
            ToolsMenu = new ToolsMenu(GameObject.Find("Tools Menu"));
            StartToolsMenuTransformPosition = ToolsMenu.MenuObject.transform.position;
            StartToolsMenuTransformRotation = ToolsMenu.MenuObject.transform.rotation;
            ToolsMenu.SelectedIcon = ToolsMenu.selectingIcon;
            ToolsMenu.SelectedIcon.SetSelectedColor();
            ToolsMenu.PreviouslySelectedIcon = ToolsMenu.selectingIcon;
            ToolsMenu.IsSelectedIcon = true;
        }
    }
}