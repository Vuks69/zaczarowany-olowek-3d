using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class ColorPickingMenuIcon : MenuIcon
    {
        private Color predefinedColor;

        public ColorPickingMenuIcon(GameObject icon, Action action, Color predefinedColor) : base(icon, action)
        {
            this.predefinedColor = predefinedColor;
        }

        public override void Select()
        {
            SetColor(SelectedColor);
            GameManager.Instance.CurrentColor = predefinedColor;
        }
    }
}