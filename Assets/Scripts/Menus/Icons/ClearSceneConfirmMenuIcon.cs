using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using System.Linq;
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
            GameManager.Instance.DeletedObjects.Add(objectList.ToList());
            foreach (var item in objectList)
            {
                item.tag = GlobalVars.DeletedObjectsTag;
                item.SetActive(false);
                // Object.Destroy(item);
            }
        }
    }
}