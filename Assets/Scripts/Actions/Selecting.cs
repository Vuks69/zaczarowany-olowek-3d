using UnityEditor;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Menus;

namespace Assets.Scripts.Actions
{
    public class Selecting : Action
    {
        Ray ray;
        RaycastHit hit;
        GameObject pointer;
        LineRenderer pointerLineRenderer;

        public Selecting()
        {
            pointer = new GameObject();
            pointerLineRenderer = pointer.AddComponent<LineRenderer>();
            pointerLineRenderer.startWidth = 0.1f;
            pointerLineRenderer.endWidth = 0.1f;
        }

        public override void HandleTriggerUp()
        {

        }

        public override void HandleTriggerDown()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            var flystickTransform = FlystickManager.Instance.Flystick.transform;
            var multiToolTransform = FlystickManager.Instance.MultiTool.transform;
            ray = new Ray(multiToolTransform.position, flystickTransform.forward);
            Debug.Log("elo from update");
            if (Physics.Raycast(ray, out hit, 1000))
            {
                pointerLineRenderer.enabled = true;
                pointerLineRenderer.SetPosition(0, multiToolTransform.position);
                pointerLineRenderer.SetPosition(1, hit.point);

                if (hit.transform.gameObject is MenuIcon)
                {
                    
                }
            }
            else
            {
                pointerLineRenderer.enabled = false;
            }
        }
    }
}