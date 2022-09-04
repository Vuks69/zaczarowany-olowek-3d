﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Actions
{
    public class LineDrawing : Action
    {
        bool drawing = false;
        readonly GameObject tool = FlystickManager.Instance.MultiTool;
        LineRenderer lineRenderer;
        Vector3 lastPosition;

        override public void HandleTriggerDown()
        {
            StartDrawing();
        }

        public override void HandleTriggerUp()
        {
            StopDrawing();
        }

        // Update is called once per frame
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

        public void StartDrawing()
        {
            if (!drawing)
            {
                // each line has to be its own object, as it can only have one renderer
                var line = new GameObject();
                line.name = "line_" + System.Guid.NewGuid().ToString(); // not sure if a name is needed, but since we will be creating a bunch of those and later editing them...
                lineRenderer = line.AddComponent<LineRenderer>();
                lineRenderer.numCapVertices = 1;
                lineRenderer.numCornerVertices = 5;
                lineRenderer.positionCount = 0;

                lineRenderer.material = new Material(Shader.Find("Particles/Additive"));    // todo add shader selection
                lineRenderer.startColor = new Color(0.0f, 0.0f, 1.0f, 1.0f);                // todo add color selection
                lineRenderer.endColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);                  // todo add color selection
                lineRenderer.startWidth = 0.1f;                                             // todo add width selection
                lineRenderer.endWidth = 0.05f;                                              // todo add width selection

                Undo.RegisterCreatedObjectUndo(line, "Created new line");

                drawing = true;
            }
        }

        public void StopDrawing()
        {
            drawing = false;
        }
    }
}