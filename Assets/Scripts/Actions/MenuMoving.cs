using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Actions
{
    public class MenuMoving : Action
    {
        public override void Finish()
        {
            MenuManager.Instance.ToolsMenu.MenuObject.transform.SetParent(null);
            MenuManager.Instance.ParametersMenu.MenuObject.transform.SetParent(null);
        }

        public override void HandleTriggerDown()
        {
            // Nothing Happens
        }

        public override void HandleTriggerUp()
        {
            // Nothing Happens
        }

        public override void Init()
        {
            MenuManager.Instance.ToolsMenu.MenuObject.transform.SetParent(FlystickManager.Instance.MultiTool.transform);
            MenuManager.Instance.ParametersMenu.MenuObject.transform.SetParent(FlystickManager.Instance.MultiTool.transform);
        }

        public override void Update()
        {
            // Nothing happens
        }
    }
}
