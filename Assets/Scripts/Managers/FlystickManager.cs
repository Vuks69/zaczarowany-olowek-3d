using UnityEngine;
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
            if (input == "trigger_down")
            {
                GameManager.Instance.CurrentAction.HandleTriggerDown();
            }
            else if (input == "trigger_up")
            {
                GameManager.Instance.CurrentAction.HandleTriggerUp();
            }
            else if (input == "button3")
            {
                ToolsMenu toolsMenu = MenuManager.Instance.ToolsMenu;
                toolsMenu.selectingIcon.Select();
                toolsMenu.SelectedIcon.Deselect();
                toolsMenu.SelectedIcon = toolsMenu.selectingIcon;
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

        //test
        void Start()
        {
            //GameManager.Instance.CurrentAction = new LineDrawing();
        }
    }
}