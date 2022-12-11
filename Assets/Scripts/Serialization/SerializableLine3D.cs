using System;
using System.Collections.Generic;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableLine3D : SerializableObject
    {
        public SerializableColor color;
        public SerializableVector3 localScale;
        public List<SerializableSegment> segments;
    }
}
