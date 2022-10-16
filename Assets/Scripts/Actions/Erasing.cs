using UnityEngine;
using Assets.Scripts.Managers;
using System.Linq;

namespace Assets.Scripts.Actions
{
    public class Erasing : Action
    {
        private bool erasing = false;

        public override void HandleTriggerDown()
        {
            erasing = true;
        }

        public override void HandleTriggerUp()
        {
            erasing = false;
        }

        public override void Update()
        {
            if (erasing)
            {
                Bounds multiToolBounds = FlystickManager.Instance.MultiTool.GetComponent<Collider>().bounds;
                var lines = GameObject.FindGameObjectsWithTag("Line");
                var intersectingLines = from line in lines where multiToolBounds.Intersects(line.GetComponent<Collider>().bounds) select line;
                foreach (var line in intersectingLines)
                {
                    Object.Destroy(line);
                }
            }
        }

        public override void Init()
        {
            // Nothing happens
        }

        public override void Finish()
        {
            // Nothing happens
        }
    }
}