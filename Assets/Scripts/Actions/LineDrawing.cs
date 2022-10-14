using UnityEngine;
using UnityEditor;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Actions
{
    public class LineDrawing : Action
    {
        private bool drawing = false;
        private readonly GameObject tool = FlystickManager.Instance.MultiTool;
        private LineRenderer lineRenderer;
        private Vector3 lastPosition;
        public float StrokeWidth { get; set; } = 0.1f;

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
            StopDrawing();
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
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, tool.transform.position);
                lastPosition = tool.transform.position;
            }
        }

        private void StartDrawing()
        {
            if (!drawing)
            {
                // each line has to be its own object, as it can only have one renderer
                var line = new GameObject();
                line.name = "line_" + System.Guid.NewGuid().ToString();
                lineRenderer = line.AddComponent<LineRenderer>();
                lineRenderer.numCapVertices = 1;
                lineRenderer.numCornerVertices = 5;
                lineRenderer.positionCount = 0;

                lineRenderer.material = new Material(Shader.Find("Particles/Additive"));    // todo add shader selection
                lineRenderer.startColor = GameManager.Instance.CurrentColor;                // todo add color selection
                lineRenderer.endColor = GameManager.Instance.CurrentColor;                  // todo add color selection
                lineRenderer.startWidth = StrokeWidth;                                             // todo add width selection
                lineRenderer.endWidth = StrokeWidth;                                              // todo add width selection

                Undo.RegisterCreatedObjectUndo(line, "Created new line");

                drawing = true;
            }
        }

        private void StopDrawing()
        {
            drawing = false;
        }
    }
}