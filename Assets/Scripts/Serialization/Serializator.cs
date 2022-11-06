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
    }
}
