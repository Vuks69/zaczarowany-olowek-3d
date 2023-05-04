using System;
using System.Collections.Generic;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableObjectArrayWrapper
    {
        public List<SerializableLine> lines;
        public List<SerializablePrimitive> primitives;

        public SerializableObjectArrayWrapper()
        {
            this.lines = new List<SerializableLine>();
            this.primitives = new List<SerializablePrimitive>();
        }
    }
}
