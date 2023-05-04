using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using Assets.Scripts.Serialization;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class LoadingMenuIcon : MenuIcon
    {
        private readonly string pathToSaveFile;
        public LoadingMenuIcon(GameObject icon, Action action, int iconIndex) : base(icon, action)
        {
            var path = GameManager.Instance.PathToSaveFile.Split(new string[] { ".json" }, System.StringSplitOptions.None);
            pathToSaveFile = path[0] + iconIndex.ToString() + path[1];
        }

        public override void Select()
        {
            SetSelectedColor();
            getIconsMenu().SelectedIcon = this;
            load();
        }

        private void load()
        {
            if (!File.Exists(pathToSaveFile))
            {
                Debug.LogError("Savefile not found at: " + pathToSaveFile);
                return;
            }

            Debug.Log("Reading file: " + pathToSaveFile);
            string toDeserialize;
            using (StreamReader sw = File.OpenText(pathToSaveFile))
            {
                toDeserialize = sw.ReadToEnd();
            }

            var serializableArray = JsonUtility.FromJson<SerializableObjectArrayWrapper>(toDeserialize);
            Debug.Log("Deserializing and recreating objects:");
            Debug.Log("    " + serializableArray.lines.Count + " [" + GlobalVars.LineName + "]");
            foreach (var serializableLine in serializableArray.lines)
            {
                serializableLine.Deserialize();
            }
            Debug.Log("    " + serializableArray.lines3d.Count + " [" + GlobalVars.Line3DName + "]");
            foreach (var serializableLine3D in serializableArray.lines3d)
            {
                serializableLine3D.Deserialize();
            }
            Debug.Log("    " + serializableArray.primitives.Count + " [" + GlobalVars.PrimitiveObjectName + "]");
            foreach (var primitive in serializableArray.primitives)
            {
                primitive.Deserialize();
            }
        }
    }
}