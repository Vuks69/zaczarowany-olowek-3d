using System;
using System.Collections.Generic;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableObjectArrayWrapper
    {
        public List<SerializableLine> lines;
        public List<SerializableLine3D> lines3d;
        public List<SerializablePrimitive> primitives;

        public SerializableObjectArrayWrapper()
        {
            this.lines = new List<SerializableLine>();
            this.lines3d = new List<SerializableLine3D>();
            this.primitives = new List<SerializablePrimitive>();
        }
    }
}
