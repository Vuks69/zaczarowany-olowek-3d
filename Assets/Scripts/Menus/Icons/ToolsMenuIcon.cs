using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class ToolsMenuIcon : MenuIcon
    {
        public ParametersMenu ParametersMenu { get; set; }

        public ToolsMenuIcon(GameObject icon, Action action, ParametersMenu parametersMenu) : base(icon, action)
        {
            ParametersMenu = parametersMenu;
        }

        public override void Select()
        {
            base.Select();
            ParametersMenu.MenuObject.transform.position = MenuManager.Instance.ParametersMenu.MenuObject.transform.position;
            ParametersMenu.MenuObject.transform.rotation = MenuManager.Instance.ParametersMenu.MenuObject.transform.rotation;
            ParametersMenu.MenuObject.SetActive(true);
            MenuManager.Instance.ParametersMenu = ParametersMenu;
        }
    }
}