using Assets.Scripts.Managers;
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
                                          where multiToolBounds.Intersects(item.GetComponent<Collider>().bounds)
                                          select item;
                foreach (GameObject objToDelete in intersectingObjects)
                {
                    Object.Destroy(objToDelete);
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