using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public struct LineRendererData
    {
        public int numCapVertices;
        public int numCornerVertices;
        public int positionCount;
        public bool useWorldSpace;
        public string shader;
        public SerializableColor startColor;
        public SerializableColor endColor;
        public float startWidth;
        public float endWidth;
        public SerializableVector3[] positions;

        public LineRendererData(LineRenderer lineRenderer)
        {
            this.numCapVertices = lineRenderer.numCapVertices;
            this.numCornerVertices = lineRenderer.numCornerVertices;
            this.positionCount = lineRenderer.positionCount;
            this.useWorldSpace = lineRenderer.useWorldSpace;
            this.shader = lineRenderer.material.shader.name;
            this.startColor = new SerializableColor(lineRenderer.startColor);
            this.endColor = new SerializableColor(lineRenderer.endColor);
            this.startWidth = lineRenderer.startWidth;
            this.endWidth = lineRenderer.endWidth;

            var lrpos = new Vector3[lineRenderer.positionCount];
            var serpos = new SerializableVector3[lineRenderer.GetPositions(lrpos)];
            for (int i = 0; i < lrpos.Length; i++)
            {
                serpos[i] = new SerializableVector3(lrpos[i]);
            }
            this.positions = serpos;
        }

        public void DeserializeOnto(GameObject line)
        {
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.numCapVertices = this.numCapVertices;
            lr.numCornerVertices = this.numCornerVertices;
            lr.positionCount = this.positionCount;
            lr.useWorldSpace = this.useWorldSpace;
            lr.material = new Material(Shader.Find(this.shader));
            lr.startColor = this.startColor.Deserialize();
            lr.endColor = this.endColor.Deserialize();
            lr.startWidth = this.startWidth;
            lr.endWidth = this.endWidth;

            Vector3[] _positions = new Vector3[this.positionCount];
            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i] = this.positions[i].Deserialize();
            }
            lr.SetPositions(_positions);
        }
    }
}

