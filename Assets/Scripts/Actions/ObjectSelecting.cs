using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using System.Linq;

namespace Assets.Scripts.Actions
{
    public class ObjectSelecting : Action
    {
        public HashSet<GameObject> SelectedObjects { get; set; } = new HashSet<GameObject>();
        private readonly HashSet<GameObject> toBeSelected = new HashSet<GameObject>();
        private readonly HashSet<GameObject> toBeRemoved = new HashSet<GameObject>();
        private bool selecting = false;
        private bool movingObjects = false;

        public override void HandleTriggerDown()
        {
            if (movingObjects)
            {
                stopMovingObjects();
                movingObjects = false;
                return;
            }
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

        public override void Finish()
        {
            // Nothing happens
        }

        public void RemoveSelection()
        {
            foreach (var selectedObject in SelectedObjects)
            {
                Object.Destroy(selectedObject);
            }
        }

        public void CopySelection()
        {
            var toBeCopied = new HashSet<GameObject>();
            foreach (var line in SelectedObjects)
            {
                var gameObject = new GameObject("line_" + System.Guid.NewGuid().ToString());
                toBeCopied.Add(gameObject);
                gameObject.tag = "Line";
                gameObject.transform.parent = FlystickManager.Instance.MultiTool.transform;

                var copiedLineRenderer = line.GetComponent<LineRenderer>();
                var lineRenderer = gameObject.AddComponent<LineRenderer>();

                lineRenderer.numCapVertices = copiedLineRenderer.numCapVertices;
                lineRenderer.numCornerVertices = copiedLineRenderer.numCornerVertices;
                lineRenderer.positionCount = copiedLineRenderer.positionCount;

                Vector3[] newPos = new Vector3[copiedLineRenderer.positionCount];
                copiedLineRenderer.GetPositions(newPos);
                lineRenderer.SetPositions(newPos);

                lineRenderer.useWorldSpace = false;
                lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
                lineRenderer.startColor = copiedLineRenderer.startColor;
                lineRenderer.endColor = copiedLineRenderer.endColor;
                lineRenderer.startWidth = copiedLineRenderer.startWidth;
                lineRenderer.endWidth = copiedLineRenderer.endWidth;

                // TODO add collider
            }
            SelectedObjects.Clear();
            SelectedObjects.UnionWith(toBeCopied);
            movingObjects = true;
        }

        private void stopMovingObjects()
        {
            foreach (var line in SelectedObjects)
            {
                line.transform.parent = null;
            }
        }
    }
}