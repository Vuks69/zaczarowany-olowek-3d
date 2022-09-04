using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus
{
    public class MenuIcon
    {
        public static Color DEFAULT_COLOR = Color.white;
        public static Color SELECTED_COLOR = Color.green;
        public static Color DISABLED_COLOR = Color.grey;
        public static Color HIGHLIGHTED_COLOR = Color.blue;
        protected Color currentColor = DEFAULT_COLOR;
        public Action Action;
        public GameObject icon;

        public MenuIcon(GameObject icon, Action action)
        {
            this.icon = icon;
            this.Action = action;
        }

        public void Select()
        {
            GameManager.Instance.CurrentAction = Action;
        }

        public virtual void UpdateColor()
        {
            var renderer = icon.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", currentColor);
        }

        public void SetColor(Color color)
        {
            currentColor = color;
            UpdateColor();
        }
    }
}