using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class ClearSceneIcon : MenuIcon
    {
        public ClearSceneIcon(GameObject icon, Action action) : base(icon, action) { }

        public override void Select()
        {
            SetSelectedColor();
            ClearScene();
        }

        private void ClearScene()
        {
            GameObject[] objectList = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
            Debug.Log("Destroying " + objectList.Length + " objects.");
            foreach (var item in objectList)
            {
                Object.Destroy(item);
            }
        }
    }
}