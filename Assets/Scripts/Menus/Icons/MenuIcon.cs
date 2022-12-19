using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class MenuIcon
    {
        public Color DefaultColor { get; set; } = Color.white;
        public Color SelectedColor { get; set; } = Color.green;
        public Color DisabledColor { get; set; } = Color.grey;
        public Color HighlightedColor { get; set; } = Color.cyan;
        protected Color currentColor;
        public Action action;
        public GameObject gameObject;

        public MenuIcon(GameObject icon, Action action)
        {
            gameObject = icon;
            this.action = action;

            currentColor = DefaultColor;
        }

        public virtual void Select()
        {
            GameManager.Instance.changeCurrentAction(action);
            SetColor(SelectedColor);
            MenuManager.Instance.ParametersMenu.MenuObject.SetActive(false);
            if (MenuManager.Instance.ParametersMenu.SelectedIcon != null)
            {
                MenuManager.Instance.ParametersMenu.SelectedIcon.Deselect();
            }
            MenuManager.Instance.ParametersMenu.IsSelectedIcon = false;
            getIconsMenu().SelectedIcon = this;
        }

        public virtual void Deselect()
        {
            SetDefaultColor();
            getIconsMenu().PreviouslySelectedIcon = MenuManager.Instance.ToolsMenu.SelectedIcon;
        }

        public virtual void UpdateColor()
        {
            var renderer = gameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", currentColor);
        }

        public void SetColor(Color color)
        {
            currentColor = color;
            UpdateColor();
        }

        public virtual void Highlight()
        {
            SetColor(HighlightedColor);
        }

        public virtual void Dehighlight()
        {
            SetDefaultColor();
        }

        public void SetDefaultColor()
        {
            SetColor(DefaultColor);
        }

        public virtual void SetSelectedColor()
        {
            SetColor(SelectedColor);
        }

        public virtual bool IsGameObjectInIcon(GameObject gameObject)
        {
            return this.gameObject == gameObject;
        }

        public bool IsIconInToolsMenu()
        {
            return GetType() == typeof(ToolsMenuIcon) || GetType() == typeof(IconWithoutParametersMenu);
        }

        public Menu getIconsMenu()
        {
            if (IsIconInToolsMenu())
            {
                return MenuManager.Instance.ToolsMenu;
            }
            return MenuManager.Instance.ParametersMenu;
        }
    }
}