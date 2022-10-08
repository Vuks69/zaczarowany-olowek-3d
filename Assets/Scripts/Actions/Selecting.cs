using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Menus;
using System.Collections.Generic;

namespace Assets.Scripts.Actions
{
    public class Selecting : Action
    {
        Ray ray;
        RaycastHit hit;
        GameObject pointer;
        LineRenderer pointerLineRenderer;
        private MenuIcon highlightedIcon;
        private bool isHighlightedIcon = false;
        private MenuIcon selectedIcon;
        private bool isSelectedIcon = false;

        public Selecting()
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
                if (isSelectedIcon)
                {
                    selectedIcon.SetDefaultColor();
                }
                selectedIcon = highlightedIcon;
                selectedIcon.Select();
                isHighlightedIcon = false;
                isSelectedIcon = true;
            }
        }

        public override void Update()
        {
            var flystickTransform = FlystickManager.Instance.Flystick.transform;
            var multiToolTransform = FlystickManager.Instance.MultiTool.transform;
            ray = new Ray(multiToolTransform.position, flystickTransform.forward);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                pointerLineRenderer.enabled = true;
                pointerLineRenderer.SetPosition(0, multiToolTransform.position);
                pointerLineRenderer.SetPosition(1, hit.point);

                if (isHighlightedIcon)
                {
                    if (hit.collider.transform.gameObject == highlightedIcon.icon)
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
                    if (icon.icon == hit.collider.transform.gameObject && !isSelectedTheSameObject(icon))
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
            if (highlightedIcon == selectedIcon)
            {
                highlightedIcon.SetSelectedColor();
                return;
            }
            highlightedIcon.SetDefaultColor();
        }

        private bool isSelectedTheSameObject(MenuIcon icon)
        {
            return isSelectedIcon && icon.icon == selectedIcon.icon;
        }
    }
}