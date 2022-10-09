using UnityEngine;
using Assets.Scripts.Managers;
using System.Collections.Generic;
using Assets.Scripts.Menus.Icons;

namespace Assets.Scripts.Actions
{
    public class Selecting : Action
    {
        private GameObject pointer;
        private LineRenderer pointerLineRenderer;
        private MenuIcon highlightedIcon;
        private bool isHighlightedIcon = false;

        public override void Init()
        {
            pointer = new GameObject("Selecting Pointer");
            pointerLineRenderer = pointer.AddComponent<LineRenderer>();
            pointerLineRenderer.startWidth = 0.1f;
            pointerLineRenderer.endWidth = 0.1f;
        }

        public override void HandleTriggerUp()
        {
            // Nothing happens
        }

        public override void HandleTriggerDown()
        {
            if (isHighlightedIcon)
            {
                var selectedIcon = MenuManager.Instance.ToolsMenu.SelectedIcon;
                if (MenuManager.Instance.ToolsMenu.IsSelectedIcon)
                {
                    selectedIcon.Deselect();
                }
                selectedIcon = highlightedIcon;
                selectedIcon.Select();
                MenuManager.Instance.ToolsMenu.SelectedIcon = selectedIcon;
                isHighlightedIcon = false;
                MenuManager.Instance.ToolsMenu.IsSelectedIcon = true;
            }
        }

        public override void Finish()
        {
            Object.Destroy(pointer);
        }

        public override void Update()
        {
            var flystickTransform = FlystickManager.Instance.Flystick.transform;
            var multiToolTransform = FlystickManager.Instance.MultiTool.transform;
            var ray = new Ray(multiToolTransform.position, flystickTransform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                pointerLineRenderer.enabled = true;
                pointerLineRenderer.SetPosition(0, multiToolTransform.position);
                pointerLineRenderer.SetPosition(1, hit.point);

                if (isHighlightedIcon)
                {
                    if (hit.collider.transform.gameObject == highlightedIcon.gameObject)
                    {
                        return;
                    }
                    changeHighlightedIconsColor();
                }

                var allMenusIcons = new List<MenuIcon>();
                allMenusIcons.AddRange(MenuManager.Instance.ToolsMenu.icons);
                allMenusIcons.AddRange(MenuManager.Instance.ParametersMenu.icons);

                foreach (MenuIcon icon in allMenusIcons)
                {
                    if (icon.gameObject == hit.collider.transform.gameObject && !isSelectedTheSameObject(icon))
                    {
                        highlightedIcon = icon;
                        isHighlightedIcon = true;
                        highlightedIcon.Highlight();
                    }
                }
            }
            else
            {
                pointerLineRenderer.enabled = false;
                if (isHighlightedIcon)
                {
                    changeHighlightedIconsColor();
                }
            }
        }

        private void changeHighlightedIconsColor()
        {
            isHighlightedIcon = false;
            if (highlightedIcon == MenuManager.Instance.ToolsMenu.SelectedIcon)
            {
                highlightedIcon.SetSelectedColor();
                return;
            }
            highlightedIcon.SetDefaultColor();
        }

        private bool isSelectedTheSameObject(MenuIcon icon)
        {
            return MenuManager.Instance.ToolsMenu.IsSelectedIcon && icon.gameObject == MenuManager.Instance.ToolsMenu.SelectedIcon.gameObject;
        }
    }
}