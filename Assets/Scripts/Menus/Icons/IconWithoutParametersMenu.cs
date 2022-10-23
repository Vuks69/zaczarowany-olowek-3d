using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class IconWithoutParametersMenu : ToolsMenuIcon
    {
        public IconWithoutParametersMenu(GameObject icon, Action action) : base(icon, action, null) { }

        public override void Select()
        {
            ParametersMenu = MenuManager.Instance.ParametersMenu;
            base.Select();
        }
    }
}