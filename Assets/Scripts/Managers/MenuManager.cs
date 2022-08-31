using System.Collections;
using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        readonly private static MenuManager instance;
        public MenuManager Instance { get; set; }

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
        private ParametersMenu parametersMenu;
        public ParametersMenu ParametersMenu
        {
            get { return parametersMenu; }
            set
            {
                InitializeParametersMenu(value);
                parametersMenu = value;
            }
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            // InitializeParametersMenu(new ColorPickingParametersMenu());
            // InitializeToolsMenu(new ToolsMenu());
        }

        private void InitializeParametersMenu(ParametersMenu menu)
        {
            Instantiate(menu.MenuObject, parametersMenu.MenuObject.transform.position, parametersMenu.MenuObject.transform.rotation);
            Destroy(parametersMenu.MenuObject);
        }

        private void InitializeToolsMenu(ToolsMenu menu)
        {
            Instantiate(menu.MenuObject, ToolsMenu.MenuObject.transform.position, ToolsMenu.MenuObject.transform.rotation);
            Destroy(ToolsMenu.MenuObject);
        }
    }
}