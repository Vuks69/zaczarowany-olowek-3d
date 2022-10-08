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
            ParametersMenusData = new ParametersMenusData();
        }

        void Start()
        {
            // InitializeParametersMenu(new ColorPickingParametersMenu());
            // InitializeToolsMenu(new ToolsMenu());
            // parametersMenu.MenuObject = pMenu;
            ParametersMenu = ParametersMenusData.ColorPicking;
            ToolsMenu = new ToolsMenu();
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