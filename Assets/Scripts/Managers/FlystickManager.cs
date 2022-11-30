using Assets.Scripts.Menus;
using UnityEditor;
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
                case "button1":
                    //Undo.PerformRedo();
                    break;
                case "button2":
                    //Undo.Perform//Undo();
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
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnPress += HandleInput;
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Fire).OnRelease += GameManager.Instance.CurrentAction.HandleTriggerUp;
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Button3).OnPress += action3;
        }
    }
}