using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using Assets.Scripts.Serialization;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class SavingMenuIcon : MenuIcon
    {
        public SavingMenuIcon(GameObject icon, Action action) : base(icon, action) { }

        public override void Select()
        {
            SetSelectedColor();
            getIconsMenu().SelectedIcon = this;
            save();
        }

        private void save()
        {
            // Get all objects created by user
            GameObject[] objectList = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
            if (objectList.Length != 0)
            {
                // Parse the objects into serializable versions
                var serializableArray = new SerializableObjectArrayWrapper();
                foreach (GameObject item in objectList)
                {
                    switch (item.name)
                    {
                        case GlobalVars.LineName:
                            serializableArray.lines.Add(Serializator.SerializeLine(item));
                            break;
                        default:
                            Debug.LogError("Attempted serialization of unsupported GameObject [" + item.name + "]");
                            break;
                    }
                }

                // Write new save
                Debug.Log("Saving world to file: " + GameManager.Instance.PathToSaveFile + "\nLines: " + serializableArray.lines.Count);
                using (StreamWriter sw = File.CreateText(GameManager.Instance.PathToSaveFile)) // overwrites old save
                {
                    sw.Write(JsonUtility.ToJson(serializableArray));
                }
            }
        }
    }
}