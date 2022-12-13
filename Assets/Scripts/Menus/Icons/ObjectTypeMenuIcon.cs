using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class ObjectTypeMenuIcon : MenuIcon
    {
		private PrimitiveType objectType;

        public ObjectTypeMenuIcon(GameObject icon, Action action, PrimitiveType objectType) : base(icon, action)
        {
			this.objectType = objectType;
        }

        public override void Select()
        {
			GameManager.Instance.ActionsData.ObjectAdding.SetObjectType (objectType);
            SetSelectedColor();
            getIconsMenu().SelectedIcon = this;
        }
    }
}