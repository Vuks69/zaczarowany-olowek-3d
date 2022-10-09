using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Actions
{
    public abstract class Action
    {
        public static Action Instance;
        public ParametersMenu ParametersMenu { get; set; }

        public void HandleLeftButton()
        {
            // left button is always Undo so no need to override this
            Undo.PerformUndo();
        }

        public void HandleRightButton()
        {
            Undo.PerformRedo();
        }

        public abstract void Init();
        public abstract void HandleTriggerDown();
        public abstract void HandleTriggerUp();
        public abstract void Update();
        public abstract void Finish();
    }
}