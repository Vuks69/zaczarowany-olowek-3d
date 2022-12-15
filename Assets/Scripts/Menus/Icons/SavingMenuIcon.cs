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
                            serializableArray.lines.Add(new SerializableLine(item));
                            break;
                        case GlobalVars.Line3DName:
                            serializableArray.lines3d.Add(new SerializableLine3D(item));
                            break;
                        case GlobalVars.Line3DCubeSegmentName:
                        case GlobalVars.Line3DCylinderSegmentName:
                            break; // ignore these, they're handled by Line3DName
                        default:
                            if (item.name.Contains(GlobalVars.PrimitiveObjectName))
                            {
                                serializableArray.primitives.Add(new SerializablePrimitive(item));
                            }
                            else
                            {
                                Debug.LogError("Attempted serialization of unsupported GameObject [" + item.name + "]");
                            }
                            break;
                    }
                }

                // Write new save
                Debug.Log("Saving world to file: " + GameManager.Instance.PathToSaveFile);
                Debug.Log("    Lines: " + serializableArray.lines.Count);
                Debug.Log("    Lines3D: " + serializableArray.lines3d.Count);
                Debug.Log("    Primitives: " + serializableArray.primitives.Count);
                using (StreamWriter sw = File.CreateText(GameManager.Instance.PathToSaveFile)) // overwrites old save
                {
                    sw.Write(JsonUtility.ToJson(serializableArray));
                }
            }
        }
    }
}