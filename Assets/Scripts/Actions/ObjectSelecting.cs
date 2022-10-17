using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using System.Linq;

namespace Assets.Scripts.Actions
{
    public class ObjectSelecting : Action
    {
        private bool selecting = false;
        public List<GameObject> SelectedObjects { get; set; } = new List<GameObject>();

        public override void HandleTriggerDown()
        {
            selecting = true;
        }

        public override void HandleTriggerUp()
        {
            selecting = false;
        }

        public override void Init()
        {
            // Nothing happens
        }

        public override void Update()
        {
            if (selecting)
            {
                Bounds multiToolBounds = FlystickManager.Instance.MultiTool.GetComponent<Collider>().bounds;
                var lines = GameObject.FindGameObjectsWithTag("Line");
                var intersectingLines = from line in lines where multiToolBounds.Intersects(line.GetComponent<Collider>().bounds) select line;
                foreach (var line in intersectingLines)
                {
                    SelectedObjects.Add(line);
                    line.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Multiply"));
                }
            }
        }

        public override void Finish()
        {
            // Nothing happens
        }
    }
}