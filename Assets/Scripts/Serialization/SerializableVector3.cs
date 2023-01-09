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
        public float w;

        public SerializableVector3(Vector3 vector3)
        {
            this.x = vector3.x;
            this.y = vector3.y;
            this.z = vector3.z;
            this.w = 0;
        }

        public SerializableVector3(Quaternion quaternion)
        {
            this.x = quaternion.x;
            this.y = quaternion.y;
            this.z = quaternion.z;
            this.w = quaternion.w;
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
                w = this.w
            };
        }
    }
}
