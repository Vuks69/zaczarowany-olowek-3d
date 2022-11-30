using UnityEngine;
using UnityEditor;
using Assets.Scripts.Actions;
using Assets.Scripts.Serialization;
using Assets.Scripts.Managers;
using System.IO;

namespace Assets.Scripts.Menus.Icons
{
    public class LoadingMenuIcon : MenuIcon
    {
        public LoadingMenuIcon(GameObject gameObject, Action action) : base(gameObject, action) { }

        public override void Select()
        {
            SetSelectedColor();
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

            //Undo.SetCurrentGroupName("Loading world from save");
            //int group = //Undo.GetCurrentGroup();

            GameObject[] objectList = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
            Debug.Log("Destroying " + objectList.Length + " objects.");
            foreach (var item in objectList)
            {
                //Undo.DestroyObjectImmediate(item);
            }

            var serializableArray = JsonUtility.FromJson<SerializableObjectArrayWrapper>(toDeserialize);
            Debug.Log("Deserializing and recreating " + serializableArray.lines.Count + " [" + GlobalVars.LineName + "] objects.");
            foreach (var serializableLine in serializableArray.lines)
            {
                Serializator.DeserializeLine(serializableLine);
            }

            //Undo.Collapse//UndoOperations(group);
        }
    }
}