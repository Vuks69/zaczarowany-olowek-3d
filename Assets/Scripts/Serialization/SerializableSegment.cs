﻿using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializableSegment : SerializableObject
    {
        public float objectScaleY;
        public SerializableVector3 rotation; //newSegment.transform.rotation = new Quaternion(rotationVector.x, rotationVector.y, rotationVector.z, 0);

        public SerializableSegment(GameObject segment)
        {
            this.name = segment.name;
            this.tag = segment.tag;
            this.objectScaleY = segment.transform.localScale.y;
            this.position = new SerializableVector3(segment.transform.position);
            this.rotation = new SerializableVector3(segment.transform.rotation);
        }

        public override GameObject Deserialize()
        {
            GameObject newSegment;
            if (this.name == GlobalVars.Line3DCylinderSegmentName)
            {
                newSegment = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            }
            else if (this.name == GlobalVars.Line3DCubeSegmentName)
            {
                newSegment = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }
            else
            {
                return null;
            }

            newSegment.name = this.name;
            newSegment.tag = this.tag;
            newSegment.transform.position = this.position.Deserialize();
            newSegment.transform.rotation = this.rotation.DeserializeAsQuaternion();
            return newSegment;
        }
    }
}
