using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public struct SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(Vector3 vector3)
        {
            this.x = vector3.x;
            this.y = vector3.y;
            this.z = vector3.z;
        }

        public SerializableVector3(Quaternion quaternion)
        {
            this.x = quaternion.x;
            this.y = quaternion.y;
            this.z = quaternion.z;
        }

        public Vector3 Deserialize()
        {
            return new Vector3
            {
                x = this.x,
                y = this.y,
                z = this.z
            };
        }

        public Quaternion DeserializeAsQuaternion()
        {
            return new Quaternion
            {
                x = this.x,
                y = this.y,
                z = this.z,
                w = 0
            };
        }
    }
}
