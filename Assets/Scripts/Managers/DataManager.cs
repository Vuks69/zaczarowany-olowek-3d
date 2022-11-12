using Assets.Scripts.Serialization;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;
        private string path;

        void Awake()
        {
            Instance = this;
            path = Application.persistentDataPath + "/save.json";
        }

        void Start()
        {
            // Do nothing.
        }

        public void SaveWorld()
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
                Debug.Log("Saving world to file: " + path + "\nLines: " + serializableArray.lines.Count);
                using (StreamWriter sw = File.CreateText(path)) // overwrites old save
                {
                    sw.Write(JsonUtility.ToJson(serializableArray));
                }
            }
        }

        public void LoadWorld()
        {
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

            Undo.SetCurrentGroupName("Loading world from save");
            int group = Undo.GetCurrentGroup();

            GameObject[] objectList = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
            Debug.Log("Destroying " + objectList.Length + " objects.");
            foreach (var item in objectList)
            {
                Undo.DestroyObjectImmediate(item);
            }

            var serializableArray = JsonUtility.FromJson<SerializableObjectArrayWrapper>(toDeserialize);
            Debug.Log("Deserializing and recreating " + serializableArray.lines.Count + " [" + GlobalVars.LineName + "] objects.");
            foreach (var serializableLine in serializableArray.lines)
            {
                Serializator.DeserializeLine(serializableLine);
            }

            Undo.CollapseUndoOperations(group);
        }
    }
}
