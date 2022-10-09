using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus.Icons
{
    public class MenuIcon
    {
        public Color DefaultColor { get; set; } = Color.white;
        public Color SelectedColor { get; set; } = Color.green;
        public Color DisabledColor { get; set; } = Color.grey;
        public Color HighlightedColor { get; set; } = Color.blue;
        protected Color currentColor;
        public Action action;
        public GameObject gameObject;

        public MenuIcon(GameObject icon, Action action)
        {
            this.gameObject = icon;
            this.action = action;

            currentColor = DefaultColor;
        }

        public virtual void Select()
        {
            GameManager.Instance.changeCurrentAction(action);
            SetColor(SelectedColor);
        }

        public void Deselect()
        {
            SetColor(DefaultColor);
        }

        public void UpdateColor()
        {
            var renderer = gameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", currentColor);
        }

        public void SetColor(Color color)
        {
            currentColor = color;
            UpdateColor();
        }

        public void Highlight()
        {
            SetColor(HighlightedColor);
        }

        public void SetDefaultColor()
        {
            SetColor(DefaultColor);
        }

        public void SetSelectedColor()
        {
            SetColor(SelectedColor);
        }
    }
}