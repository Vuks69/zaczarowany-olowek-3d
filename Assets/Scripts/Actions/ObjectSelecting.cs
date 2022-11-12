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
        private GameObject[] gameObjects;
        protected SelectionState CurrentState = SelectionState.STANDBY;

        protected enum SelectionState
        {
            STANDBY,
            SELECTING,
            COPYING
        }

        public override void HandleTriggerDown()
        {
            switch (CurrentState)
            {
                case SelectionState.COPYING:
                    StopMovingObjects();
                    CurrentState = SelectionState.STANDBY;
                    break;
                default:
                    CurrentState = SelectionState.SELECTING;
                    gameObjects = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
                    break;
            }
        }

        public override void HandleTriggerUp()
        {
            switch(CurrentState)
            {
                case SelectionState.SELECTING:
                    CurrentState = SelectionState.STANDBY;
                    SelectedObjects.UnionWith(toBeSelected);
                    SelectedObjects.ExceptWith(toBeRemoved);
                    toBeSelected.Clear();
                    toBeRemoved.Clear();
                    break;
                default:
                    break;
            }
            
        }

        public override void Init()
        {
            // Nothing happens
        }

        public override void Update()
        {
            if (CurrentState == SelectionState.SELECTING)
            {
                Bounds multiToolBounds = FlystickManager.Instance.MultiTool.GetComponent<Collider>().bounds;
                var intersectingObjects = from item
                                          in gameObjects
                                          where multiToolBounds.Intersects(item.GetComponent<Collider>().bounds)
                                          select item;
                foreach (GameObject intersectingObject in intersectingObjects)
                {
                    bool willBeSelected = toBeSelected.Contains(intersectingObject);
                    bool willBeRemoved = toBeRemoved.Contains(intersectingObject);
                    if (!willBeSelected && !willBeRemoved)
                    {
                        if (SelectedObjects.Contains(intersectingObject))
                        {
                            intersectingObject.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
                            toBeRemoved.Add(intersectingObject);
                        }
                        else
                        {
                            intersectingObject.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Multiply"));
                            toBeSelected.Add(intersectingObject);
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
            foreach (var oldObj in SelectedObjects)
            {

                GameObject newObj = new GameObject
                {
                    name = GlobalVars.LineName,
                    tag = GlobalVars.UniversalTag
                };
                newObj.transform.position = oldObj.transform.position;
                newObj.transform.rotation = oldObj.transform.rotation;
                newObj.transform.parent = FlystickManager.Instance.MultiTool.transform;

                var oldLineRenderer = oldObj.GetComponent<LineRenderer>();
                var newLineRenderer = newObj.AddComponent<LineRenderer>();

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

                newObj.AddComponent<MeshCollider>();
                newObj.GetComponent<MeshCollider>().sharedMesh = oldLineRenderer.GetComponent<MeshCollider>().sharedMesh;

                Undo.RegisterCreatedObjectUndo(newObj, "Create Copied Object");
                toBeCopied.Add(newObj);
            }
            Undo.CollapseUndoOperations(group);
            SelectedObjects.Clear();
            SelectedObjects.UnionWith(toBeCopied);
            CurrentState = SelectionState.COPYING;
        }

        private void StopMovingObjects()
        {
            foreach (var obj in SelectedObjects)
            {
                obj.transform.parent = null;
                obj.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
            }
            SelectedObjects.Clear();
        }
    }
}