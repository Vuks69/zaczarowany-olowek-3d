using Assets.Scripts.Managers;
using Assets.Scripts.Menus;
using Assets.Scripts.Menus.Icons;
using System.Linq;
using UnityEngine;

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
            pointerLineRenderer.startWidth = 0.03f;
            pointerLineRenderer.endWidth = 0.01f;
            pointerLineRenderer.enabled = true;
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
                var rightMenu = getRightSelectedIconMenu();
                var rightSelectedIcon = rightMenu.SelectedIcon;
                if (rightMenu.IsSelectedIcon)
                {
                    rightSelectedIcon.Deselect();
                }
                rightSelectedIcon = highlightedIcon;
                rightSelectedIcon.Select();
                rightMenu.IsSelectedIcon = true;
                isHighlightedIcon = false;
                if (isObjectSelectingIcon(highlightedIcon))
                {
                    ObjectSelecting.DeselectAll();
                    MenuManager.Instance.ParametersMenusData.ObjectSelectingParametersMenu.SetSelectionSliderToDefaultPosition();
                }
                if (highlightedIcon is Slider)
                {
                    moveSlider = true;
                    (highlightedIcon as Slider).PreviousFlystickForward = FlystickManager.Instance.Flystick.transform.forward;
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
                if (highlightedIcon is Slider)
                {
                    (highlightedIcon as Slider).Move();
                }
                return;
            }
            var multiToolTransform = FlystickManager.Instance.MultiTool.transform;
            var ray = new Ray(multiToolTransform.position, multiToolTransform.forward);
            pointerLineRenderer.SetPosition(0, multiToolTransform.position);
            pointerLineRenderer.SetPosition(1, multiToolTransform.position + multiToolTransform.forward * 10.0f);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                pointerLineRenderer.SetPosition(1, hit.point);
                PCoord = hit.textureCoord;

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
                    //if (!isSelectedTheSameObject(icon))
                    //{
                    highlightedIcon = icon;
                    isHighlightedIcon = true;
                    highlightedIcon.Highlight();
                    //}
                }
            }
            else
            {
                if (isHighlightedIcon)
                {
                    changeHighlightedIconsColor();
                }
            }
        }

		public void UpdatePointerColor()
		{
			pointerLineRenderer.startColor = GameManager.Instance.CurrentColor;
			pointerLineRenderer.endColor = GameManager.Instance.CurrentColor;
		}

        private void changeHighlightedIconsColor()
        {
            isHighlightedIcon = false;
            if (highlightedIcon == getRightSelectedIcon())
            {
                highlightedIcon.SetSelectedColor();
                return;
            }
            highlightedIcon.Dehighlight();
        }

        private bool isIconHit(MenuIcon icon, RaycastHit hit)
        {
            return icon.IsGameObjectInIcon(hit.collider.transform.gameObject);
        }

        private bool isSelectedTheSameObject(MenuIcon icon)
        {
            var rightMenu = getRightSelectedIconMenu(icon);
            return rightMenu.IsSelectedIcon && icon.gameObject == rightMenu.SelectedIcon.gameObject;
        }

        private MenuIcon getRightSelectedIcon()
        {
            return getRightSelectedIconMenu().SelectedIcon;
        }

        private Menu getRightSelectedIconMenu()
        {
            return getRightSelectedIconMenu(highlightedIcon);
        }

        private Menu getRightSelectedIconMenu(MenuIcon icon)
        {
            if (icon != null && !icon.IsIconInToolsMenu())
            {
                return MenuManager.Instance.ParametersMenu;
            }
            return MenuManager.Instance.ToolsMenu;
        }

        private bool isObjectSelectingIcon(MenuIcon icon)
        {
            return icon.GetType() != typeof(ObjectSelectingMenuIcon) && icon.GetType() != typeof(SelectionScaleSlider) && icon.gameObject.name != "Object Selecting";
        }
    }
}