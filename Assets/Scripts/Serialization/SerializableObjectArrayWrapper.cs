using System;
using System.Collections.Generic;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableObjectArrayWrapper
    {
        public List<SerializableLine> lines;
        public List<SerializableLine3D> lines3d;

        public SerializableObjectArrayWrapper()
        {
            this.lines = new List<SerializableLine>();
            this.lines3d = new List<SerializableLine3D>();
        }
    }
}
