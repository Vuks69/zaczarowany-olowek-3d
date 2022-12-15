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

        public abstract GameObject Deserialize();
    }
}
