using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus.Icons
{
    public class SelectingIcon : ToolsMenuIcon
    {
        public SelectingIcon(GameObject icon, Action action) : base(icon, action, null) { }
        public override void Select()
        {
            ParametersMenu = MenuManager.Instance.ParametersMenu;
            base.Select();
        }
    }
}