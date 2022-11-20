using Assets.Scripts.Managers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Actions
{
    public class Manipulation : Action
    {
        private GameObject pointer;
        private GameObject lastHit;
        private Color lastColor;
        private LineRenderer pointerLineRenderer;

        public override void HandleTriggerDown()
        {
            // nothing
        }

        public override void HandleTriggerUp()
        {
            // nothing
        }

        public override void Init()
        {
            pointer = new GameObject("Selecting Pointer");
            pointerLineRenderer = pointer.AddComponent<LineRenderer>();
            pointerLineRenderer.startWidth = 0.1f;
            pointerLineRenderer.endWidth = 0.01f;
            pointerLineRenderer.enabled = true;
        }

        public override void Update()
        {
            var flystickTransform = FlystickManager.Instance.Flystick.transform;
            var multiToolTransform = FlystickManager.Instance.MultiTool.transform;
            var ray = new Ray(multiToolTransform.position, flystickTransform.forward);
            pointerLineRenderer.enabled = true;
            pointerLineRenderer.SetPosition(0, multiToolTransform.position);
            pointerLineRenderer.SetPosition(1, flystickTransform.forward * 100);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000) && hit.collider.transform.gameObject != lastHit &&
                    hit.collider.transform.gameObject.CompareTag(GlobalVars.UniversalTag))
            {
                //we got a hit on new object
                Unhighlight();
                lastHit = hit.collider.transform.gameObject;
                Highlight();
            }
            else
            {
                Unhighlight();
            }
        }

        public override void Finish()
        {
            Unhighlight();
            Object.Destroy(pointer);
        }

        private void Highlight()
        {
            var lr = lastHit.GetComponent<LineRenderer>();
            lastColor = lr.startColor;
            lr.startColor = Color.cyan;
            lr.endColor = Color.cyan;
        }

        private void Unhighlight()
        {
            if (lastHit != null)
            {
                lastHit.GetComponent<LineRenderer>().startColor = lastColor;
                lastHit.GetComponent<LineRenderer>().endColor = lastColor;
                lastHit = null;
            }
        }
    }
}
