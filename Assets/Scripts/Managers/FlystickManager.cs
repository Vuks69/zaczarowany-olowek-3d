using Assets.Scripts.Menus;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
                case "button3":
                    toggleAction();
                    break;
                case "button2":
                    undo();
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
            Instance = this;
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

        private void undo()
        {
            //GameManager.Instance.DeletedObject.tag = GlobalVars.UniversalTag;
            //GameManager.Instance.DeletedObject.SetActive(true);

            if (GameManager.Instance.DeletedObjects.Count == 0)
            {
                return;
            }

            foreach (GameObject obj in GameManager.Instance.DeletedObjects.Last())
            {
                obj.tag = GlobalVars.UniversalTag;
                obj.SetActive(true);
                Debug.Log("instantiate deleted object: " + obj.name);
            }
            GameManager.Instance.DeletedObjects.RemoveAt(GameManager.Instance.DeletedObjects.Count - 1);
            if (GameManager.Instance.DeletedObjects.Count > 20)
            {
                foreach(GameObject obj in GameManager.Instance.DeletedObjects[0])
                {
                    Object.Destroy(obj);
                }
                GameManager.Instance.DeletedObjects.RemoveAt(0);
            }
        }
    }
}