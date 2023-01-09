using Assets.Scripts.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class ObjectSelecting : Action
    {
        private static HashSet<GameObject> SelectedObjects { get; set; } = new HashSet<GameObject>();
        private readonly HashSet<GameObject> toBeSelected = new HashSet<GameObject>();
        private readonly HashSet<GameObject> toBeRemoved = new HashSet<GameObject>();
        private GameObject[] gameObjects;
        private SelectionState CurrentState = SelectionState.STANDBY;
        private SelectionState ToolState = SelectionState.SELECTING;

        private enum SelectionState
        {
            STANDBY,
            SELECTING,
            COPYING,
            MOVING
        }

        public void SetStateCopying()
        {
            ToolState = SelectionState.COPYING;
        }

        public void SetStateMoving()
        {
            ToolState = SelectionState.MOVING;
        }

        public void SetStateChangeColor()
        {
            ChangeSelectionColor();
            ToolState = SelectionState.STANDBY;
        }

        public override void HandleTriggerDown()
        {
            switch (ToolState)
            {
                case SelectionState.SELECTING: // select objects
                    CurrentState = SelectionState.SELECTING;
                    gameObjects = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
                    break;
                case SelectionState.COPYING: // copy selected objects relative to flystick position
                    CopySelection();
                    CurrentState = SelectionState.MOVING;
                    MoveObjects();
                    break;
                case SelectionState.MOVING: // move selected objects relative to flystick position
                    CurrentState = SelectionState.MOVING;
                    MoveObjects();
                    break;
                default: // catching bugs
                    ToolState = SelectionState.SELECTING;
                    break;
            }
        }

        public override void HandleTriggerUp()
        {
            switch (CurrentState)
            {
                case SelectionState.SELECTING:
                    ToolState = SelectionState.SELECTING;
                    CurrentState = SelectionState.STANDBY;
                    SelectedObjects.UnionWith(toBeSelected);
                    SelectedObjects.ExceptWith(toBeRemoved);
                    toBeSelected.Clear();
                    toBeRemoved.Clear();
                    break;

                case SelectionState.MOVING:
                    ToolState = SelectionState.SELECTING;
                    CurrentState = SelectionState.STANDBY;
                    StopMovingObjects(deselect: true);
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
                                          where ((item.GetComponent<Collider>() != null) && (multiToolBounds.Intersects(item.GetComponent<Collider>().bounds))) //temporary solution, TODO: handle 3D lines (have no collider, but childer have colliders
                                          select item;
                foreach (GameObject item in intersectingObjects)
                {
                    GameObject intersectingObject;
                    if (item.transform.parent != null)
                    {
                        intersectingObject = item.transform.parent.gameObject;
                    }
                    else
                    {
                        intersectingObject = item;
                    }
                    bool willBeSelected = toBeSelected.Contains(intersectingObject);
                    bool willBeRemoved = toBeRemoved.Contains(intersectingObject);
                    if (!willBeSelected && !willBeRemoved)
                    {
                        if (SelectedObjects.Contains(intersectingObject))
                        {
                            changeColorToDefault(intersectingObject);
                            toBeRemoved.Add(intersectingObject);
                        }
                        else
                        {
                            changeColorToSelected(intersectingObject);
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

        public void DeleteSelection()
        {
            GameManager.Instance.DeletedObjects.Add(SelectedObjects.ToList());
            foreach (var selectedObject in SelectedObjects)
            {
                // Object.Destroy(selectedObject);
                selectedObject.tag = GlobalVars.DeletedObjectsTag;
                selectedObject.SetActive(false);
            }
            SelectedObjects.Clear();
        }

        public void CopySelection()
        {
            var toBeCopied = new HashSet<GameObject>();
            foreach (var oldObj in SelectedObjects)
            {
                GameObject newObj;
                if (oldObj.transform.GetComponent<LineRenderer>() != null)
                {
                    newObj = new GameObject
                    {
                        name = GlobalVars.LineName,
                        tag = GlobalVars.UniversalTag
                    };

                    newObj.transform.position = oldObj.transform.position;
                    newObj.transform.rotation = oldObj.transform.rotation;

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
                }
                else
                {
                    newObj = Object.Instantiate(oldObj);
                    newObj.name = oldObj.name;
                }
                toBeCopied.Add(newObj);
            }
            DeselectAll();
            SelectedObjects.UnionWith(toBeCopied);
        }

        internal void MoveObjects()
        {
            foreach (var obj in SelectedObjects)
            {
                obj.transform.parent = FlystickManager.Instance.MultiTool.transform;
            }
        }

        private void StopMovingObjects(bool deselect = true)
        {
            foreach (var obj in SelectedObjects)
            {
                obj.transform.parent = null;
            }
            if (deselect) DeselectAll();
        }

        public static void DeselectAll()
        {
            foreach (var obj in SelectedObjects)
            {
                changeColorToDefault(obj);
            }
            SelectedObjects.Clear();
        }

        public void ChangeSelectionColor()
        {
            foreach (var obj in SelectedObjects)
            {
                if (obj.GetComponent<LineRenderer>() != null)
                {
                    obj.GetComponent<LineRenderer>().startColor = GameManager.Instance.CurrentColor;
                    obj.GetComponent<LineRenderer>().endColor = GameManager.Instance.CurrentColor;
                }
                else if (obj.GetComponent<Renderer>() != null)
                {
                    obj.GetComponent<Renderer>().material.color = GameManager.Instance.CurrentColor;
                }
                else
                {
                    foreach (Transform child in obj.transform)
                    {
                        child.gameObject.GetComponent<Renderer>().material.color = GameManager.Instance.CurrentColor;
                    }
                }
            }
            SelectedObjects.Clear();
        }

        public void ChangeSelectionScale(Vector3 scale)
        {
            foreach (var obj in SelectedObjects)
            {
                obj.transform.localScale = scale;
            }
        }

        private static void changeColorToDefault(GameObject obj)
        {
            if (obj.GetComponent<LineRenderer>() != null)
            {
                obj.GetComponent<LineRenderer>().startColor += new Color(0f, 0f, 0f, 0.7f);
                obj.GetComponent<LineRenderer>().endColor += new Color(0f, 0f, 0f, 0.7f);
            }
            else if (obj.GetComponent<Renderer>() != null)
            {
                obj.GetComponent<Renderer>().material.color -= new Color(0f, 0f, 5f, 0f);
            }
            else
            {
                foreach (Transform child in obj.transform)
                {
                    child.gameObject.GetComponent<Renderer>().material.color -= new Color(0f, 0f, 5f, 0f);
                }
            }
        }

        private void changeColorToSelected(GameObject obj)
        {
            if (obj.GetComponent<LineRenderer>() != null)
            {
                obj.GetComponent<LineRenderer>().startColor -= new Color(0f, 0f, 0f, 0.7f);
                obj.GetComponent<LineRenderer>().endColor -= new Color(0f, 0f, 0f, 0.7f);
            }
            else if (obj.GetComponent<Renderer>() != null)
            {
                obj.GetComponent<Renderer>().material.color += new Color(0f, 0f, 5f, 0f);
            }
            else
            {
                foreach (Transform child in obj.transform)
                {
                    child.gameObject.GetComponent<Renderer>().material.color += new Color(0f, 0f, 5f, 0f);
                }
            }
        }
    }
}