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
                    serializableArray.objects[i++] = SerializeObject(item);
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

        private SerializableObject SerializeObject(GameObject toSerialize)
        {
            SerializableObject serializableObject;
            switch (toSerialize.name)
            {
                case GlobalVars.LineName: // to serialize: transform.position and LineRenderer variables
                    var lr = toSerialize.GetComponent<LineRenderer>();
                    serializableObject = new SerializableLine
                    {
                        name = toSerialize.name,
                        tag = toSerialize.tag,
                        position = new SerializableObject.SerializableVector3
                        {
                            x = toSerialize.transform.position.x,
                            y = toSerialize.transform.position.y,
                            z = toSerialize.transform.position.z
                        },
                        lineRendererData = new SerializableLine.LineRendererData
                        {
                            numCapVertices = lr.numCapVertices,
                            numCornerVertices = lr.numCornerVertices,
                            positionCount = lr.positionCount,
                            useWorldSpace = lr.useWorldSpace,
                            shader = lr.material.shader.name,
                            startColor = new SerializableLine.SerializableColor
                            {
                                r = lr.startColor.r,
                                g = lr.startColor.g,
                                b = lr.startColor.b,
                                a = lr.startColor.a
                            },
                            endColor = new SerializableLine.SerializableColor
                            {
                                r = lr.endColor.r,
                                g = lr.endColor.g,
                                b = lr.endColor.b,
                                a = lr.endColor.a
                            },
                            startWidth = lr.startWidth,
                            endWidth = lr.endWidth,
                        }
                    };
                    break;
                default:
                    Debug.LogError("Attempted serialization of unsupported GameObject [" + toSerialize.name + "]");
                    return null;
            }

            return serializableObject;
        }
    }
}
