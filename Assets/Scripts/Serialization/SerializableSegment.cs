using System;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableSegment : SerializableObject
    {
        public float objectScaleY;
        public SerializableVector3 rotation; //newSegment.transform.rotation = new Quaternion(rotationVector.x, rotationVector.y, rotationVector.z, 0);
    }
}
