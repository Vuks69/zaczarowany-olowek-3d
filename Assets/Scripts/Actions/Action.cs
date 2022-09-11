using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Actions
{

    public abstract class Action
    {
        private ParametersMenu parametersMenu;

        public void HandleLeftButton()
        {
            // left button is always Undo so no need to override this
            Undo.PerformUndo();
        }

        public void HandleRightButton()
        {
            Undo.PerformRedo();
        }

        public abstract void HandleTriggerDown();
        public abstract void HandleTriggerUp();
        public abstract void Update();
    }
}