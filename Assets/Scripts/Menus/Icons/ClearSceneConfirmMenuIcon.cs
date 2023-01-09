using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class ClearSceneConfirmMenuIcon : MenuIcon
    {
        public ClearSceneConfirmMenuIcon(GameObject icon, Action action) : base(icon, action) { }

        public override void Select()
        {
            SetSelectedColor();
            clearScene();
            getIconsMenu().SelectedIcon = this;
        }

        private void clearScene()
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