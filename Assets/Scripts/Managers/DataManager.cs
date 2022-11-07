using Assets.Scripts.Serialization;
using System.IO;
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
            //if (File.Exists(path))
            //{
            //    Debug.Log("Loading world from file: " + path);
            //    GameObject[] objectList = JsonUtility.FromJson<GameObject[]>(File.ReadAllText(path));
            //}
            //else
            //{
            //    Debug.LogError("Savefile not found at " + path);
            //}
        }
    }
}
