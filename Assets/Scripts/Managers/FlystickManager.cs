using UnityEngine;
using UnityEditor;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Managers
{
    public class FlystickManager : MonoBehaviour
    {
        public static FlystickManager Instance;
        public GameObject Flystick;
        public GameObject MultiTool;

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
                case "button1":
                    Undo.PerformRedo();
                    break;
                case "button2":
                    Undo.PerformUndo();
                    break;
                case "button3":
                    ToolsMenu toolsMenu = MenuManager.Instance.ToolsMenu;
                    toolsMenu.selectingIcon.Select();
                    toolsMenu.SelectedIcon.Deselect();
                    toolsMenu.SelectedIcon = toolsMenu.selectingIcon;
                    break;
                default:
                    break;
            }
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

            if (Input.GetKeyDown(KeyCode.U))
            {
                input = "button1";
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                input = "button2";
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                input = "button3";
            }

            HandleInput(input);
            GameManager.Instance.CurrentAction.Update();
        }

        void Awake()
        {
            Instance = this;
        }
    }
}