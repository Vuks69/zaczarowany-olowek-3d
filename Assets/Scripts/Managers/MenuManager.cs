using System.Collections;
using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        private ToolsMenu toolsMenu;
        public ToolsMenu ToolsMenu
        {
            get { return toolsMenu; }
            set
            {
                InitializeToolsMenu(value);
                toolsMenu = value;
            }
        }
        private ParametersMenu parametersMenu = new ParametersMenu();
        public ParametersMenu ParametersMenu
        {
            get { return parametersMenu; }
            set
            {
                InitializeParametersMenu(value);
                parametersMenu = value;
            }
        }

        public ParametersMenusData ParametersMenusData { get; set; }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            // InitializeParametersMenu(new ColorPickingParametersMenu());
            // InitializeToolsMenu(new ToolsMenu());
            // parametersMenu.MenuObject = pMenu;
            ParametersMenusData = new ParametersMenusData();
            GameManager.Instance.ActionsData.AssignParametrsMenus();
            ParametersMenu = ParametersMenusData.ColorPickingParametersMenu;
            ToolsMenu = new ToolsMenu();
            ToolsMenu.SelectedIcon = ToolsMenu.selectingIcon;
            ToolsMenu.SelectedIcon.SetSelectedColor();
        }

        private void InitializeParametersMenu(ParametersMenu menu)
        {
            //Instantiate(menu.MenuObject, parametersMenu.MenuObject.transform.position, parametersMenu.MenuObject.transform.rotation);
            //Destroy(parametersMenu.MenuObject);
        }

        private void InitializeToolsMenu(ToolsMenu menu)
        {
            // Instantiate(menu.MenuObject, ToolsMenu.MenuObject.transform.position, ToolsMenu.MenuObject.transform.rotation);
            // Destroy(ToolsMenu.MenuObject);
        }
    }
}