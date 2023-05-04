using Assets.Scripts.Actions;
using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableLine : SerializableObject
    {
        public LineRendererData lineRendererData;

        public SerializableLine(GameObject line)
        {
            this.name = line.name;
            this.tag = line.tag;
            this.position = new SerializableVector3(line.transform.position);
            this.lineRendererData = new LineRendererData(line.GetComponent<LineRenderer>());
        }

        public override GameObject Deserialize()
        {
            GameObject line = new GameObject()
            {
                name = this.name,
                tag = this.tag
            };
            line.transform.position = this.position.Deserialize();
            lineRendererData.DeserializeOnto(line);
            LineDrawing.CreateCollider(line);

            return line;
        }
    }
}
