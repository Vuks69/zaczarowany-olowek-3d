using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using Assets.Scripts.Serialization;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class LoadingMenuIcon : MenuIcon
    {
        public LoadingMenuIcon(GameObject gameObject, Action action) : base(gameObject, action) { }

        public override void Select()
        {
            SetSelectedColor();
            getIconsMenu().SelectedIcon = this;
            load();
        }

        private void load()
        {
            var path = GameManager.Instance.PathToSaveFile;
            if (!File.Exists(path))
            {
                Debug.LogError("Savefile not found at: " + path);
                return;
            }

            Debug.Log("Reading file: " + path);
            string toDeserialize;
            using (StreamReader sw = File.OpenText(path))
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