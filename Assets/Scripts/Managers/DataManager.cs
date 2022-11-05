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
                var serializableArray = new SerializableObjectArrayWrapper<SerializableObject>
                {
                    objects = new SerializableObject[objectList.Length]
                };
                int i = 0;
                foreach (GameObject item in objectList)
                {
                    serializableArray.objects[i++] = Serializator.SerializeObject(item);
                }

                // Write new save
                Debug.Log("Saving world to file: " + path + "\nObjects: " + serializableArray.objects.Length);
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
