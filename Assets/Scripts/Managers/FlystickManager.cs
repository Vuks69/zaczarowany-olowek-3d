using Assets.Scripts.Actions;
using Assets.Scripts.Menus;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class FlystickManager : MonoBehaviour
    {
        public static FlystickManager Instance;
        public GameObject Flystick;
        public GameObject MultiTool;
        int flystickIdx = 0;

        void Update()
        {
            GameManager.Instance.CurrentAction.Update();
        }

        void Start()
        {
            Instance = this;
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnPress += HandleInputTriggerDown;
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnRelease += HandleInputTriggerUp;
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Button1).OnPress += toggleAction;
        }

        private void HandleInputTriggerDown()
        {
            GameManager.Instance.CurrentAction.HandleTriggerDown();
        }

        private void HandleInputTriggerUp()
        {
            GameManager.Instance.CurrentAction.HandleTriggerUp();
        }

        private void toggleAction()
        {
            ToolsMenu toolsMenu = MenuManager.Instance.ToolsMenu;
            if (toolsMenu.PreviouslySelectedIcon != toolsMenu.selectingIcon && toolsMenu.SelectedIcon != toolsMenu.selectingIcon)
            {
                toolsMenu.PreviouslySelectedIcon = toolsMenu.selectingIcon;
            }
            Debug.Log(GetType().Name + ": deselecting " + toolsMenu.SelectedIcon.gameObject.name + " selecting " + toolsMenu.PreviouslySelectedIcon.gameObject.name);
            var tmp = toolsMenu.PreviouslySelectedIcon;
            toolsMenu.SelectedIcon.Deselect();
            tmp.Select();
        }
    }
}