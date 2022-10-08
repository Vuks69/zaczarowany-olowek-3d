using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Actions
{
    public class ColorPicking : Action
    {
        public override void HandleTriggerUp()
        {
            // perform action associated with trigger
            // when trigger is pressed AND flystick points at ColorPalette OR PredefinedColorIcon
            // then set CurrentColor
            GameManager.Instance.CurrentColor = Color.red;
        }

        public override void Finish()
        {
            throw new System.NotImplementedException();
        }

        public override void HandleTriggerDown()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}