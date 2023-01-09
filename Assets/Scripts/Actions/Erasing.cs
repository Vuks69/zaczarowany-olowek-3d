using Assets.Scripts.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Erasing : Action
    {
        private bool erasing = false;
        private GameObject[] gameObjects;

        public override void HandleTriggerDown()
        {
            erasing = true;
            gameObjects = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
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
                var intersectingObjects = from item
                                          in gameObjects
                                          where item != null
                                          where ((item.GetComponent<Collider>() != null) && (multiToolBounds.Intersects(item.GetComponent<Collider>().bounds)))
                                          select item;
                if (intersectingObjects == null)
                {
                    return;
                }

                var objectsToBeDeleted = new HashSet<GameObject>();

                foreach (GameObject obj in intersectingObjects)
                {
                    if (obj.transform.parent != null)
                    {
                        objectsToBeDeleted.Add(obj.transform.parent.gameObject);
                    }
                    else
                    {
                        objectsToBeDeleted.Add(obj);
                    }
                }

                foreach (GameObject obj in objectsToBeDeleted)
                {
                    obj.gameObject.tag = GlobalVars.DeletedObjectsTag;
                    obj.gameObject.SetActive(false);
                    GameManager.Instance.DeletedObjects.Add(new List<GameObject> { obj });
                    //Object.Destroy(objToDelete.transform.parent.gameObject);
                }
                gameObjects = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
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