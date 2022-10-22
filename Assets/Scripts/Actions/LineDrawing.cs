using UnityEngine;
using UnityEditor;
using Assets.Scripts.Managers;
using System.Collections.Generic;

namespace Assets.Scripts.Actions
{
    public class LineDrawing : Action
    {
        private bool drawing = false;
        private readonly GameObject tool = FlystickManager.Instance.MultiTool;
        private LineRenderer lineRenderer;
        private GameObject line;
        private Vector3 lastPosition;
        public float StrokeWidth { get; set; } = 0.1f;
        private List<Vector3> points = new List<Vector3>();

        public override void Init()
        {
            // Nothing happens
        }

        public override void HandleTriggerDown()
        {
            StartDrawing();
        }

        public override void HandleTriggerUp()
        {
            if (!drawing)
            {
                return;
            }
            StopDrawing();
            createCollider();
        }

        public override void Finish()
        {
            // Nothing happens
        }

        public override void Update()
        {
            if (drawing && Vector3.Distance(lastPosition, tool.transform.position) > 0.02f)
            {
                // once the flystick has moved away enough from last position, add new position
                // this is done to prevent adding 60 positions per second while drawing

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, tool.transform.position - line.transform.position);
                lastPosition = tool.transform.position;
            }
        }

        private void StartDrawing()
        {
            if (!drawing)
            {
                // each line has to be its own object, as it can only have one renderer
                line = instantiateLine();

                Undo.RegisterCreatedObjectUndo(line, "Created new line");

                drawing = true;
            }
        }

        private GameObject instantiateLine()
        {
            var gameObject = new GameObject();
            gameObject.name = "line_" + System.Guid.NewGuid().ToString();
            gameObject.tag = "Line";
            gameObject.transform.position = tool.transform.position;

            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.numCapVertices = 1;
            lineRenderer.numCornerVertices = 5;
            lineRenderer.positionCount = 0;
            lineRenderer.useWorldSpace = false;

            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));    // todo add shader selection
            lineRenderer.startColor = GameManager.Instance.CurrentColor;                // todo add color selection
            lineRenderer.endColor = GameManager.Instance.CurrentColor;                  // todo add color selection
            lineRenderer.startWidth = StrokeWidth;                                             // todo add width selection
            lineRenderer.endWidth = StrokeWidth;                                              // todo add width selection

            return gameObject;
        }

        private void StopDrawing()
        {
            drawing = false;
        }

        private void createCollider()
        {
            if (lineRenderer.positionCount < 2)
            {
                return;
            }

            points.Clear();
            GameObject caret = null;
            caret = new GameObject("Lines");

            Vector3 left, right;

            // For all but the last point
            for (var i = 0; i < lineRenderer.positionCount - 1; i++)
            {
                caret.transform.position = lineRenderer.GetPosition(i);
                caret.transform.LookAt(lineRenderer.GetPosition(i + 1));
                right = caret.transform.position + line.transform.right * lineRenderer.startWidth / 2;
                left = caret.transform.position - line.transform.right * lineRenderer.startWidth / 2;
                points.Add(left);
                points.Add(right);
            }

            // Last point looks backwards and reverses
            caret.transform.position = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            caret.transform.LookAt(lineRenderer.GetPosition(lineRenderer.positionCount - 2));
            right = caret.transform.position - line.transform.right * lineRenderer.startWidth / 2;
            left = caret.transform.position + line.transform.right * lineRenderer.startWidth / 2;
            points.Add(left);
            points.Add(right);
            Object.Destroy(caret);
            Mesh mesh = drawMesh();
            var collider = line.AddComponent<MeshCollider>();
            collider.sharedMesh = mesh;
        }

        private Mesh drawMesh()
        {
            Vector3[] verticies = new Vector3[points.Count];

            for (int i = 0; i < verticies.Length; i++)
            {
                verticies[i] = points[i];
            }

            int[] triangles = new int[((points.Count / 2) - 1) * 6];

            //Works on linear patterns tn = bn+c
            int position = 6;
            for (int i = 0; i < (triangles.Length / 6); i++)
            {
                triangles[i * position] = 2 * i;
                triangles[i * position + 3] = 2 * i;

                triangles[i * position + 1] = 2 * i + 3;
                triangles[i * position + 4] = (2 * i + 3) - 1;

                triangles[i * position + 2] = 2 * i + 1;
                triangles[i * position + 5] = (2 * i + 1) + 2;
            }

            var mesh = new Mesh();
            mesh.vertices = verticies;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}