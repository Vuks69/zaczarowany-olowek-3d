using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public abstract class SerializableObject
    {
        public string name;
        public string tag;
        public SerializableVector3 position;

        public struct SerializableVector3
        {
            public float x;
            public float y;
            public float z;
        }
    }

    [Serializable]
    public class SerializableLine : SerializableObject
    {
        public LineRendererData lineRendererData;

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
        }

        [Serializable]
        public struct SerializableColor
        {
            public float r;
            public float g;
            public float b;
            public float a;
        }
    }

    [Serializable]
    public class SerializableObjectArrayWrapper<T>
    {
        public T[] objects;
    }


}
