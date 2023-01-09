using Assets.Scripts.Actions;
using Assets.Scripts.Menus;
using System;
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
        int flystickIdx = 1;

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
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Button3).OnPress += Undo;
            Lzwp.input.flysticks[flystickIdx].GetButton(LzwpInput.Flystick.ButtonID.Button4).OnPress += resetMenuPosition;
        }

        private void resetMenuPosition()
        {
            MenuManager.Instance.ParametersMenu.MenuObject.transform.parent = MenuManager.Instance.ToolsMenu.MenuObject.transform;
            MenuManager.Instance.ToolsMenu.MenuObject.transform.position = Vector3.zero;
            MenuManager.Instance.ToolsMenu.MenuObject.transform.rotation = Quaternion.identity;
            MenuManager.Instance.ParametersMenu.MenuObject.transform.SetParent(null);
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

        public static void Undo()
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
                    Destroy(obj);
                }
                GameManager.Instance.DeletedObjects.RemoveAt(0);
            }
        }
    }
}