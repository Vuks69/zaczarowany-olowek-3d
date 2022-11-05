using UnityEngine;

namespace Assets.Scripts.Serialization
{
    public static class Serializator
    {
        public static SerializableObject SerializeObject(GameObject toSerialize)
        {
            SerializableObject serializableObject;
            switch (toSerialize.name)
            {
                case GlobalVars.LineName: // to serialize: transform.position and LineRenderer variables
                    var lr = toSerialize.GetComponent<LineRenderer>();
                    serializableObject = new SerializableObject
                    {
                        name = toSerialize.name,
                        tag = toSerialize.tag,
                        position = new SerializableObject.SerializableVector3
                        {
                            x = toSerialize.transform.position.x,
                            y = toSerialize.transform.position.y,
                            z = toSerialize.transform.position.z
                        },
                        lineRendererData = new SerializableObject.LineRendererData
                        {
                            numCapVertices = lr.numCapVertices,
                            numCornerVertices = lr.numCornerVertices,
                            positionCount = lr.positionCount,
                            useWorldSpace = lr.useWorldSpace,
                            shader = lr.material.shader.name,
                            startColor = new SerializableObject.SerializableColor
                            {
                                r = lr.startColor.r,
                                g = lr.startColor.g,
                                b = lr.startColor.b,
                                a = lr.startColor.a
                            },
                            endColor = new SerializableObject.SerializableColor
                            {
                                r = lr.endColor.r,
                                g = lr.endColor.g,
                                b = lr.endColor.b,
                                a = lr.endColor.a
                            },
                            startWidth = lr.startWidth,
                            endWidth = lr.endWidth,
                        }
                    };
                    break;
                default:
                    Debug.LogError("Attempted serialization of unsupported GameObject [" + toSerialize.name + "]");
                    return null;
            }

            return serializableObject;
        }
    }

}
