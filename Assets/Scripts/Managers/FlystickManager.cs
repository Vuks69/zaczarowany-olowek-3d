using Assets.Scripts.Menus;
using UnityEngine;
using Assets.Scripts.Actions;

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
                    action3();
                    break;
                case "save_state":
                    DataManager.Instance.SaveWorld();
                    break;
                case "load_state":
                    DataManager.Instance.LoadWorld();
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

            if (Input.GetKeyDown(KeyCode.R))
            {
                input = "button1";
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                input = "button2";
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                input = "button3";
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
            {
                input = "save_state";
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
            {
                input = "load_state";
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

        void HandleInputTriggerDown()
        {
            //HandleInput("trigger_down");
            if (GameManager.Instance.CurrentAction is Selecting)
            {
                GameManager.Instance.ActionsData.Selecting.HandleTriggerDown();
            }
            else if (GameManager.Instance.CurrentAction is LineDrawing)
            {
                GameManager.Instance.ActionsData.LineDrawing.HandleTriggerDown();
            }
        }

        void HandleInputTriggerUp()
        {
            //HandleInput("trigger_up");
            if (GameManager.Instance.CurrentAction is Selecting)
            {
                GameManager.Instance.ActionsData.Selecting.HandleTriggerUp();
            }
            else if (GameManager.Instance.CurrentAction is LineDrawing)
            {
                GameManager.Instance.ActionsData.LineDrawing.HandleTriggerUp();
            }
        }
    
    }
}