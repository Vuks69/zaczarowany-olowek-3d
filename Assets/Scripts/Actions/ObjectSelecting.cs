using Assets.Scripts.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class ObjectSelecting : Action
    {
        private bool selecting = false;
        public HashSet<GameObject> SelectedObjects { get; set; } = new HashSet<GameObject>();
        private readonly HashSet<GameObject> toBeSelected = new HashSet<GameObject>();
        private readonly HashSet<GameObject> toBeRemoved = new HashSet<GameObject>();

        public override void HandleTriggerDown()
        {
            selecting = true;
        }

        public override void HandleTriggerUp()
        {
            selecting = false;
            SelectedObjects.UnionWith(toBeSelected);
            SelectedObjects.ExceptWith(toBeRemoved);
            toBeSelected.Clear();
            toBeRemoved.Clear();
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
                    bool willBeSelected = toBeSelected.Contains(line);
                    bool willBeRemoved = toBeRemoved.Contains(line);
                    if (!(willBeSelected && willBeRemoved))
                    {
                        if (SelectedObjects.Contains(line) && !willBeRemoved)
                        {
                            line.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
                            toBeRemoved.Add(line);
                        }
                        else if (!willBeSelected)
                        {
                            line.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Multiply"));
                            toBeSelected.Add(line);
                        }
                    }
                }
            }
        }

        public void RemoveSelection()
        {
            foreach (var selectedObject in SelectedObjects)
            {
                UnityEditor.Undo.DestroyObjectImmediate(selectedObject);
            }
        }

        public override void Finish()
        {
            // Nothing happens
        }

        public override void Init()
        {
            // Nothing happens
        }
    }
}