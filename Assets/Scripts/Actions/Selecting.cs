using UnityEditor;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Actions
{
    public class Selecting : Action
    {
        Ray ray;
        RaycastHit hit;
        GameObject pointer;
        LineRenderer pointerLineRenderer;
        MenuIcon highlightedIcon;

        public Selecting()
        {
            pointer = new GameObject();
            pointerLineRenderer = pointer.AddComponent<LineRenderer>();
            pointerLineRenderer.startWidth = 0.1f;
            pointerLineRenderer.endWidth = 0.1f;
        }

        public override void HandleTriggerUp()
        {

        }

        public override void HandleTriggerDown()
        {
            if (highlightedIcon != null)
            {
                GameManager.Instance.CurrentAction = highlightedIcon.Action;
                highlightedIcon.SetColor(MenuIcon.SELECTED_COLOR);
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

                if (highlightedIcon != null)
                {
                    if (hit.transform.gameObject == highlightedIcon.icon)
                    {
                        return;
                    }
                    highlightedIcon.SetColor(MenuIcon.DEFAULT_COLOR);
                    highlightedIcon = null;
                }

                foreach (MenuIcon icon in MenuManager.Instance.ToolsMenu.icons)
                {
                    if (icon.icon == hit.transform.gameObject)
                    {
                        highlightedIcon = icon;
                        icon.SetColor(MenuIcon.HIGHLIGHTED_COLOR);
                    }
                }
            }
            else
            {
                pointerLineRenderer.enabled = false;
                if (highlightedIcon != null)
                {
                    highlightedIcon.SetColor(MenuIcon.DEFAULT_COLOR);
                }
            }
        }
    }
}