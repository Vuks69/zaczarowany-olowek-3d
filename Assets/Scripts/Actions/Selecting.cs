using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Linq;

namespace Assets.Scripts.Actions
{
    public class Selecting : Action
    {
        private GameObject pointer;
        private LineRenderer pointerLineRenderer;
        private MenuIcon highlightedIcon;
        private bool isHighlightedIcon = false;
        public Vector2 PCoord { get; set; }
        private bool moveSlider = false;

        public override void Init()
        {
            pointer = new GameObject("Selecting Pointer");
            pointerLineRenderer = pointer.AddComponent<LineRenderer>();
            pointerLineRenderer.startWidth = 0.1f;
            pointerLineRenderer.endWidth = 0.1f;
        }

        public override void HandleTriggerUp()
        {
            if (moveSlider)
            {
                moveSlider = false;
                pointer.SetActive(true);
            }
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
                if (highlightedIcon.GetType() == typeof(Slider))
                {
                    moveSlider = true;
                    ((Slider)highlightedIcon).PreviousFlystickForward = FlystickManager.Instance.Flystick.transform.forward;
                    pointer.SetActive(false);
                }
            }
        }

        public override void Finish()
        {
            Object.Destroy(pointer);
        }

        public override void Update()
        {
            if (moveSlider)
            {
                if (highlightedIcon.GetType() == typeof(Slider))
                {
                    ((Slider)highlightedIcon).Move();
                }
                return;
            }
            var flystickTransform = FlystickManager.Instance.Flystick.transform;
            var multiToolTransform = FlystickManager.Instance.MultiTool.transform;
            var ray = new Ray(multiToolTransform.position, flystickTransform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                PCoord = hit.textureCoord;
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

                var allMenusIcons = MenuManager.Instance.ToolsMenu.icons.Concat(MenuManager.Instance.ParametersMenu.icons);
                foreach (var icon in allMenusIcons.Where(y => isIconHit(y, hit)))
                {
                    highlightedIcon = icon;
                    isHighlightedIcon = true;
                    highlightedIcon.Highlight();
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
            highlightedIcon.Dehighlight();
        }

        private bool isSelectedTheSameObject(MenuIcon icon)
        {
            return MenuManager.Instance.ToolsMenu.IsSelectedIcon && icon.gameObject == MenuManager.Instance.ToolsMenu.SelectedIcon.gameObject;
        }

        private bool isIconHit(MenuIcon icon, RaycastHit hit)
        {
            return icon.IsGameObjectInIcon(hit.collider.transform.gameObject) && !isSelectedTheSameObject(icon);
        }
    }
}