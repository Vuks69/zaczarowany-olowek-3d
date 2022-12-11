using System;

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
    }
}

