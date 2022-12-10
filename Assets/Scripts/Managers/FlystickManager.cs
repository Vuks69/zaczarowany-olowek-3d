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

        // przykladowe
        void HandleInput(string input)
        {
            switch (input)
            {
                case "trigger_down":
                    GameManager.Instance.CurrentAction.HandleTriggerDown();
                    break;
                case "trigger_up":
                    GameManager.Instance.CurrentAction.HandleTriggerUp();
                    break;
                case "button3":
                    toggleAction();
                    break;
                default:
                    break;
            }
        }


        void action3()
        {
            ToolsMenu toolsMenu = MenuManager.Instance.ToolsMenu;
            toolsMenu.selectingIcon.Select();
            toolsMenu.SelectedIcon.Deselect();
            toolsMenu.SelectedIcon = toolsMenu.selectingIcon;
        }

        void Update()
        {
            string input = "";
            //for monoscopic mode
            if (Input.GetButtonDown("Trigger"))
            {
                input = "trigger_down";
            }

            if (Input.GetButtonUp("Trigger"))
            {
                input = "trigger_up";
            }

            if (Input.GetButtonDown("Redo Button"))
            {
                input = "button1";
            }

            if (Input.GetButtonDown("Undo Button"))
            {
                input = "button2";
            }

            if (Input.GetButtonDown("Selecting Mode Button"))
            {
                input = "button3";
            }

            HandleInput(input);
            GameManager.Instance.CurrentAction.Update();
        }

        void Awake()
        {

        }

        void Start()
        {
            Instance = this;
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnPress += HandleInputTriggerDown;
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnRelease += HandleInputTriggerUp;
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Button3).OnPress += action3;
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