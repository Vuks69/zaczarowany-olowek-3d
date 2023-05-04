using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public struct SerializableMesh
    {
        public SerializableVector3[] vertices;
        public int[] triangles;

        public SerializableMesh(Mesh mesh)
        {
            var serializedMeshVertices = new SerializableVector3[mesh.vertexCount];
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                serializedMeshVertices[i] = new SerializableVector3(mesh.vertices[i]);
            }
            this.vertices = serializedMeshVertices;
            this.triangles = mesh.triangles;
        }

        public Mesh Deserialize()
        {
            Mesh mesh = new Mesh();
            var deserializedMeshVertices = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                deserializedMeshVertices[i] = this.vertices[i].Deserialize();
            }
            mesh.vertices = deserializedMeshVertices;
            mesh.triangles = this.triangles;
            return mesh;
        }
    }
}