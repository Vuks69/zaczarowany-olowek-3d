using System;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class SerializablePrimitive : SerializableObject
    {
        public SerializableColor color;
        public SerializableVector3 rotation;
        public SerializableVector3 localScale;

        public SerializablePrimitive(GameObject gameObject)
        {
            this.name = gameObject.name;
            this.tag = gameObject.tag;
            this.color = new SerializableColor(gameObject.GetComponent<Renderer>().material.color);
            this.position = new SerializableVector3(gameObject.transform.position);
            this.rotation = new SerializableVector3(gameObject.transform.rotation);
            this.localScale = new SerializableVector3(gameObject.transform.localScale);
        }

        public override GameObject Deserialize()
        {
            GameObject primitive;
            switch (this.name.Replace(GlobalVars.PrimitiveObjectName, ""))
            {
                case "Sphere":
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    break;
                case "Capsule":
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    break;
                case "Cylinder":
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    break;
                case "Cube":
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    break;
                case "Plane":
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    break;
                case "Quad":
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    break;
                default:
                    throw new NotImplementedException();
            }

            primitive.name = this.name;
            primitive.tag = this.tag;
            primitive.GetComponent<Renderer>().material.color = this.color.Deserialize();
            primitive.transform.position = this.position.Deserialize();
            primitive.transform.rotation = this.rotation.DeserializeAsQuaternion();
            primitive.transform.localScale = this.localScale.Deserialize();

            return primitive;
        }
    }
}
