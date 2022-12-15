using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public struct SerializableColor
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public SerializableColor(Color color)
        {
            this.r = color.r;
            this.g = color.g;
            this.b = color.b;
            this.a = color.a;
        }

        public Color Deserialize()
        {
            return new Color
            {
                r = this.r,
                g = this.g,
                b = this.b,
                a = this.a
            };
        }
    }
}

