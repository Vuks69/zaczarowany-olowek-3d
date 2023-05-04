using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public struct MeshRendererData
    {
        public SerializableColor color;
        public string shader;

        public MeshRendererData(MeshRenderer meshRenderer)
        {
            this.color = new SerializableColor(meshRenderer.material.color);
            this.shader = meshRenderer.material.shader.name;
        }

        public void DeserializeOnto(GameObject line)
        {
            var renderer = line.AddComponent<MeshRenderer>();
            renderer.material = new Material(Shader.Find(this.shader));
            renderer.material.color = this.color.Deserialize();
        }
    }
}