using System;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableObject
    {
        public string name;
        public string tag;
        public SerializableVector3 position;
    }
}
