using Assets.Scripts.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

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
                    if (!willBeSelected && !willBeRemoved)
                    {
                        if (SelectedObjects.Contains(line))
                        {
                            line.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
                            toBeRemoved.Add(line);
                        }
                        else
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
                UnityEditor.Undo.DestroyObjectImmediate(selectedObject);
            }
            SelectedObjects.Clear();
        }

        public void CopySelection()
        {
            Undo.SetCurrentGroupName("Copy Selection");
            int group = Undo.GetCurrentGroup();
            var toBeCopied = new HashSet<GameObject>();
            foreach (var oldLine in SelectedObjects)
            {
                var newLine = new GameObject("line_" + System.Guid.NewGuid().ToString());
                toBeCopied.Add(newLine);
                newLine.tag = "Line";
                newLine.transform.position = oldLine.transform.position;
                newLine.transform.parent = FlystickManager.Instance.MultiTool.transform;

                var oldLineRenderer = oldLine.GetComponent<LineRenderer>();
                var newLineRenderer = newLine.AddComponent<LineRenderer>();

                newLineRenderer.numCapVertices = oldLineRenderer.numCapVertices;
                newLineRenderer.numCornerVertices = oldLineRenderer.numCornerVertices;
                newLineRenderer.positionCount = oldLineRenderer.positionCount;

                Vector3[] newPos = new Vector3[oldLineRenderer.positionCount];
                oldLineRenderer.GetPositions(newPos);
                newLineRenderer.SetPositions(newPos);

                newLineRenderer.useWorldSpace = false;
                newLineRenderer.material = oldLineRenderer.material;
                oldLineRenderer.material = new Material(Shader.Find("Particles/Additive"));
                newLineRenderer.startColor = oldLineRenderer.startColor;
                newLineRenderer.endColor = oldLineRenderer.endColor;
                newLineRenderer.startWidth = oldLineRenderer.startWidth;
                newLineRenderer.endWidth = oldLineRenderer.endWidth;

                newLine.AddComponent<MeshCollider>();
                newLine.GetComponent<MeshCollider>().sharedMesh = oldLineRenderer.GetComponent<MeshCollider>().sharedMesh;

                Undo.RegisterCreatedObjectUndo(newLine, "Create Copied Line");
            }
            Undo.CollapseUndoOperations(group);
            SelectedObjects.Clear();
            SelectedObjects.UnionWith(toBeCopied);
            movingObjects = true;
        }

        private void stopMovingObjects()
        {
            foreach (var line in SelectedObjects)
            {
                line.transform.parent = null;
                line.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
            }
            SelectedObjects.Clear();
        }
    }
}