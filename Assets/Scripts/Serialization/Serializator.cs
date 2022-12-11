using Assets.Scripts.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    public static class Serializator
    {
        // Note we have to explicitly create a method for each type, due to JsonUtility not working well with class inheritance
        // Also, thanks Unity for STILL not having sane, working serialization.

        public static SerializableLine SerializeLine(GameObject toSerialize)
        {
            var lr = toSerialize.GetComponent<LineRenderer>();
            var lrpos = new Vector3[lr.positionCount];
            var serpos = new SerializableVector3[lr.GetPositions(lrpos)];
            for (int i = 0; i < lrpos.Length; i++)
            {
                serpos[i] = new SerializableVector3(lrpos[i]);
            }

            SerializableLine serializableLine = new SerializableLine
            {
                name = toSerialize.name,
                tag = toSerialize.tag,
                position = new SerializableVector3(toSerialize.transform.position),
                lineRendererData = new LineRendererData
                {
                    numCapVertices = lr.numCapVertices,
                    numCornerVertices = lr.numCornerVertices,
                    positionCount = lr.positionCount,
                    useWorldSpace = lr.useWorldSpace,
                    shader = lr.material.shader.name,
                    startColor = new SerializableColor(lr.startColor),
                    endColor = new SerializableColor(lr.endColor),
                    startWidth = lr.startWidth,
                    endWidth = lr.endWidth,
                    positions = serpos
                }
            };

            return serializableLine;
        }

        public static void DeserializeLine(SerializableLine toDeserialize)
        {
            GameObject line = new GameObject()
            {
                name = toDeserialize.name,
                tag = toDeserialize.tag
            };
            line.transform.position = new Vector3()
            {
                x = toDeserialize.position.x,
                y = toDeserialize.position.y,
                z = toDeserialize.position.z
            };

            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.numCapVertices = toDeserialize.lineRendererData.numCapVertices;
            lr.numCornerVertices = toDeserialize.lineRendererData.numCornerVertices;
            lr.positionCount = toDeserialize.lineRendererData.positionCount;
            lr.useWorldSpace = toDeserialize.lineRendererData.useWorldSpace;
            lr.material = new Material(Shader.Find(toDeserialize.lineRendererData.shader));
            lr.startColor = new Color()
            {
                r = toDeserialize.lineRendererData.startColor.r,
                g = toDeserialize.lineRendererData.startColor.g,
                b = toDeserialize.lineRendererData.startColor.b,
                a = toDeserialize.lineRendererData.startColor.a
            };
            lr.endColor = new Color()
            {
                r = toDeserialize.lineRendererData.endColor.r,
                g = toDeserialize.lineRendererData.endColor.g,
                b = toDeserialize.lineRendererData.endColor.b,
                a = toDeserialize.lineRendererData.endColor.a
            };
            lr.startWidth = toDeserialize.lineRendererData.startWidth;
            lr.endWidth = toDeserialize.lineRendererData.endWidth;

            Vector3[] positions = new Vector3[toDeserialize.lineRendererData.positionCount];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector3()
                {
                    x = toDeserialize.lineRendererData.positions[i].x,
                    y = toDeserialize.lineRendererData.positions[i].y,
                    z = toDeserialize.lineRendererData.positions[i].z
                };
            }
            lr.SetPositions(positions);

            LineDrawing.CreateCollider(line);
        }

        public static SerializableLine3D SerializeLine3D(GameObject toSerialize)
        {
            List<SerializableSegment> segments = new List<SerializableSegment>();
            for (int index = 0; index < toSerialize.transform.childCount; index++)
            {
                var transform = toSerialize.transform.GetChild(index);
                var segment = transform.gameObject;

                SerializableVector3 rotation = new SerializableVector3
                {
                    x = segment.transform.rotation.x,
                    y = segment.transform.rotation.y,
                    z = segment.transform.rotation.z
                }; // discarding rotation.w as it is always 0

                var serializableSegment = new SerializableSegment
                {
                    name = segment.name,
                    tag = segment.tag,
                    position = new SerializableVector3(segment.transform.position),
                    objectScaleY = segment.transform.localScale.y,
                    rotation = rotation
                };
                segments.Add(serializableSegment);
            }

            SerializableLine3D serializableLine3D = new SerializableLine3D
            {
                name = toSerialize.name, // doubles as linetype
                tag = toSerialize.tag,
                position = new SerializableVector3(toSerialize.transform.position),
                color = new SerializableColor(toSerialize.transform.GetChild(0).GetComponent<Renderer>().material.color),
                localScale = new SerializableVector3(toSerialize.transform.GetChild(0).transform.localScale),
                segments = segments
            };

            return serializableLine3D;
        }

        public static void DeserializeLine3D(SerializableLine3D toDeserialize)
        {
            GameObject line = new GameObject()
            {
                name = toDeserialize.name,
                tag = toDeserialize.tag
            };
            line.transform.position = new Vector3()
            {
                x = toDeserialize.position.x,
                y = toDeserialize.position.y,
                z = toDeserialize.position.z
            };

            line.AddComponent<Rigidbody>();
            line.GetComponent<Rigidbody>().isKinematic = true;

            foreach (var segment in toDeserialize.segments)
            {
                GameObject newSegment;
                if (segment.name == GlobalVars.Line3DCylinderSegmentName)
                {
                    newSegment = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                }
                else if (segment.name == GlobalVars.Line3DCubeSegmentName)
                {
                    newSegment = GameObject.CreatePrimitive(PrimitiveType.Cube);
                }
                else
                {
                    Debug.LogError("Attempted deserialization of unsupported linetype: [" + segment.name + "]");
                    Object.Destroy(line);
                    return;
                }
                newSegment.name = segment.name;
                newSegment.tag = segment.tag;
                newSegment.transform.parent = line.transform;
                newSegment.transform.position = new Vector3()
                {
                    x = segment.position.x,
                    y = segment.position.y,
                    z = segment.position.z
                };
                newSegment.GetComponent<Renderer>().material.color = new Color()
                {
                    r = toDeserialize.color.r,
                    g = toDeserialize.color.g,
                    b = toDeserialize.color.b,
                    a = toDeserialize.color.a
                };
                newSegment.transform.localScale = new Vector3()
                {
                    x = toDeserialize.localScale.x,
                    y = segment.objectScaleY,
                    z = toDeserialize.localScale.z
                };
                newSegment.transform.rotation = new Quaternion()
                {
                    x = segment.rotation.x,
                    y = segment.rotation.y,
                    z = segment.rotation.z,
                    w = 0
                };
            }
        }
    }
}
