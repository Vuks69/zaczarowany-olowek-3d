using UnityEngine;

namespace Assets.Scripts.Serialization
{
    public static class Serializator
    {
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
                lineRendererData = new SerializableLine.LineRendererData
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

        public static GameObject DeserializeLine(SerializableLine toDeserialize)
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

            return line;
        }
    }
}
