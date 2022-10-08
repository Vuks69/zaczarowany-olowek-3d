using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus
{
    public class MenuIcon
    {
        public Color DefaultColor { get; set; } = Color.white;
        public Color SelectedColor { get; set; } = Color.green;
        public Color DisabledColor { get; set; } = Color.grey;
        public Color HighlightedColor { get; set; } = Color.blue;
        private Color currentColor;
        public Action action;
        public GameObject icon;

        public MenuIcon(GameObject icon, Action action)
        {
            this.icon = icon;
            this.action = action;

            currentColor = DefaultColor;
        }

        public void Select()
        {
            GameManager.Instance.CurrentAction = action;
            SetColor(SelectedColor);
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