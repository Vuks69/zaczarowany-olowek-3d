using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class LineDrawing : Action
    {
        public enum LineType
        {
            Rectangle,
            Triangle,
            Circle,
            Star,
            Plus,
            Custom
        }

        private bool drawing = false;
        private readonly GameObject drawingTool = FlystickManager.Instance.DrawingTool;
        private GameObject line;
        private Vector3 lastPosition;

        public override void Init()
        {
            // Nothing
        }

        public override void HandleTriggerDown()
        {
            StartDrawing();
        }

        public override void HandleTriggerUp()
        {
            if (drawing)
            {
                drawing = false;
                finishDrawing();
            }
        }

        public override void Finish()
        {
            // Nothing happens
        }

        public override void Update()
        {
            if (drawing && Vector3.Distance(lastPosition, drawingTool.transform.position) > 0.005f)
            {
                Mesh mesh = line.GetComponent<MeshFilter>().mesh;
                Mesh drawingToolMesh = drawingTool.GetComponent<MeshFilter>().mesh;
                Vector3[] drawingToolVertices = drawingToolMesh.vertices;

                updateLineMesh(mesh, drawingToolMesh, drawingToolVertices);

                lastPosition = drawingTool.transform.position;
            }
        }

        private void StartDrawing()
        {
            if (!drawing)
            {
                // each line has to be its own object, as it can only have one renderer
                line = InstantiateLine();

                Mesh mesh = line.GetComponent<MeshFilter>().mesh;
                Mesh drawingToolMesh = drawingTool.GetComponent<MeshFilter>().mesh;
                Vector3[] drawingToolVertices = drawingToolMesh.vertices;
                Vector3[] transformedMeshVertices = new Vector3[drawingToolVertices.Length];

                for (int i = 0; i < drawingToolVertices.Length; i++)
                {
                    transformedMeshVertices[i] = drawingTool.transform.TransformPoint(drawingToolVertices[i]);
                }

                mesh.vertices = transformedMeshVertices;
                mesh.triangles = drawingToolMesh.triangles;

                drawing = true;
            }
        }

        private GameObject InstantiateLine()
        {
            GameObject newLine = new GameObject(GlobalVars.LineName);
            newLine.tag = GlobalVars.UniversalTag;
            var renderer = newLine.AddComponent<MeshRenderer>();
            renderer.material = new Material(Shader.Find("Sprites/Diffuse"));
            renderer.material.color = GameManager.Instance.CurrentColor;
            newLine.AddComponent<MeshFilter>();

            lastPosition = drawingTool.transform.position;

            return newLine;
        }

        private void updateLineMesh(Mesh lineMesh, Mesh drawingToolMesh, Vector3[] drawingToolVertices)
        {
            Vector3[] oldVertices = lineMesh.vertices;
            int[] oldTriangles = lineMesh.triangles;

            Vector3[] newSegmentVertices = new Vector3[drawingToolMesh.vertexCount];
            for (int i = 0; i < drawingToolMesh.vertexCount; i++)
            {
                newSegmentVertices[i] = drawingTool.transform.TransformPoint(drawingToolVertices[i]);
            }

            int newSegmentTriangleCount = drawingToolMesh.vertexCount * 2;

            int[] newSegmentTriangles = new int[newSegmentTriangleCount * 3];

            for (int i = 0; i < drawingToolMesh.vertexCount; i++)
            {
                newSegmentTriangles[6 * i] = oldVertices.Length - drawingToolMesh.vertexCount + i;
                newSegmentTriangles[6 * i + 2] = oldVertices.Length + i;
                newSegmentTriangles[6 * i + 1] = oldVertices.Length - drawingToolMesh.vertexCount + i + 1;

                newSegmentTriangles[6 * i + 3] = oldVertices.Length - drawingToolMesh.vertexCount + i + 1;
                newSegmentTriangles[6 * i + 5] = oldVertices.Length + i;
                newSegmentTriangles[6 * i + 4] = oldVertices.Length + i + 1;
            }

            // fix two last triangles
            newSegmentTriangles[newSegmentTriangleCount * 3 - 5] = oldVertices.Length - drawingToolMesh.vertexCount;
            newSegmentTriangles[newSegmentTriangleCount * 3 - 3] = oldVertices.Length - drawingToolMesh.vertexCount;
            newSegmentTriangles[newSegmentTriangleCount * 3 - 2] = oldVertices.Length;

            Vector3[] newVertices = new Vector3[oldVertices.Length + newSegmentVertices.Length];
            int[] newTriangles = new int[oldTriangles.Length + newSegmentTriangles.Length];

            oldVertices.CopyTo(newVertices, 0);
            newSegmentVertices.CopyTo(newVertices, oldVertices.Length);

            oldTriangles.CopyTo(newTriangles, 0);
            newSegmentTriangles.CopyTo(newTriangles, oldTriangles.Length);

            lineMesh.vertices = newVertices;
            lineMesh.triangles = newTriangles;
        }

        private void finishDrawing()
        {
            Mesh mesh = line.GetComponent<MeshFilter>().mesh;
            int[] oldTriangles = mesh.triangles;

            int[] newSegmentTriangles = DrawingUtils.MeshTrianglesFromVertices(drawingTool.GetComponent<MeshFilter>().mesh.vertices);

            // close tube
            for (int i = 0; i < newSegmentTriangles.Length / 2; i++)
            {
                var tmp = newSegmentTriangles[i] + mesh.vertexCount - drawingTool.GetComponent<MeshFilter>().mesh.vertexCount;
                newSegmentTriangles[i] = newSegmentTriangles[newSegmentTriangles.Length - 1 - i] + mesh.vertexCount - drawingTool.GetComponent<MeshFilter>().mesh.vertexCount;
                newSegmentTriangles[newSegmentTriangles.Length - 1 - i] = tmp;
            }

            int[] newTriangles = new int[oldTriangles.Length + newSegmentTriangles.Length];

            oldTriangles.CopyTo(newTriangles, 0);
            newSegmentTriangles.CopyTo(newTriangles, oldTriangles.Length);

            mesh.triangles = newTriangles;

            line.AddComponent<MeshCollider>().sharedMesh = mesh;
        }

        public void SetLineType(LineType type)
        {
            switch (type)
            {
                case LineType.Rectangle:
                    {
                        setDrawingToolShape(DrawingToolShape.rectangle());
                        break;
                    }
                case LineType.Triangle:
                    {
                        setDrawingToolShape(DrawingToolShape.triangle());
                        break;
                    }
                case LineType.Circle:
                    {
                        setDrawingToolShape(DrawingToolShape.circle());
                        break;
                    }
                case LineType.Star:
                    {
                        setDrawingToolShape(DrawingToolShape.star());
                        break;
                    }
                case LineType.Plus:
                    {
                        setDrawingToolShape(DrawingToolShape.plus());
                        break;
                    }
                case LineType.Custom:
                    {
                        setDrawingToolShape(DrawingToolShape.custom(GameManager.Instance.LineSize));
                        break;
                    }
                default: break;
            }
            UpdateDrawingToolSize();
        }

        private void setDrawingToolShape(Vector3[] vertices)
        {
            Mesh drawingToolMesh = drawingTool.GetComponent<MeshFilter>().mesh;
            drawingToolMesh.Clear();
            drawingToolMesh.vertices = vertices;
            drawingToolMesh.triangles = DrawingUtils.MeshTrianglesFromVertices(vertices);
        }

        public void UpdateDrawingToolSize()
        {
            drawingTool.transform.localScale = new Vector3(GameManager.Instance.LineSize, GameManager.Instance.LineSize);
        }
    }
}