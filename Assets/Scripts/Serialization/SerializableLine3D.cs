using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableLine3D : SerializableObject
    {
        public SerializableColor color;
        public SerializableVector3 localScale;
        public List<SerializableSegment> segments;

        public SerializableLine3D(GameObject line3D)
        {
            this.name = line3D.name; // doubles as linetype
            this.tag = line3D.tag;
            this.position = new SerializableVector3(line3D.transform.position);
            this.rotation = new SerializableVector3(line3D.transform.rotation);
            this.color = new SerializableColor(line3D.transform.GetChild(0).GetComponent<Renderer>().material.color);
            this.localScale = new SerializableVector3(line3D.transform.GetChild(0).transform.localScale);
            this.segments = new List<SerializableSegment>();
            for (int index = 0; index < line3D.transform.childCount; index++)
            {
                this.segments.Add(new SerializableSegment(line3D.transform.GetChild(index).gameObject));
            }
        }

        public override GameObject Deserialize()
        {
            GameObject line = new GameObject()
            {
                name = this.name,
                tag = this.tag
            };
            line.transform.position = this.position.Deserialize();
            line.transform.rotation = this.rotation.DeserializeAsQuaternion();

            line.AddComponent<Rigidbody>();
            line.GetComponent<Rigidbody>().isKinematic = true;

            foreach (var segment in this.segments)
            {
                GameObject newSegment = segment.Deserialize();
                if (newSegment == null)
                {
                    Debug.LogError("Attempted deserialization of unsupported linetype: [" + segment.name + "]");
                    UnityEngine.Object.Destroy(line);
                    return null;
                }

                var _localScale = localScale.Deserialize();
                _localScale.y = segment.objectScaleY;
                newSegment.transform.localScale = _localScale;

                newSegment.transform.parent = line.transform;
                newSegment.GetComponent<Renderer>().material.color = color.Deserialize();
            }

            return line;
        }
    }
}
