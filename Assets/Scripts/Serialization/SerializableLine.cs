using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableLine : SerializableObject
    {
        public SerializableMesh mesh;
        public MeshRendererData meshRendererData;

        public SerializableLine(GameObject line)
        {
            this.name = line.name;
            this.tag = line.tag;
            this.position = new SerializableVector3(line.transform.position);
            this.mesh = new SerializableMesh(line.GetComponent<MeshFilter>().mesh);
            this.meshRendererData = new MeshRendererData(line.GetComponent<MeshRenderer>());
        }

        public override GameObject Deserialize()
        {
            GameObject line = new GameObject()
            {
                name = this.name,
                tag = this.tag
            };
            line.transform.position = this.position.Deserialize();
            meshRendererData.DeserializeOnto(line);
            line.AddComponent<MeshCollider>().sharedMesh = this.mesh.Deserialize();
            line.AddComponent<MeshFilter>().sharedMesh = this.mesh.Deserialize();

            return line;
        }
    }
}
