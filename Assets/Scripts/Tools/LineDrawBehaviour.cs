using System.Collections.Generic;
using UnityEngine;

public class LineDrawBehaviour : MonoBehaviour, IDrawingTool
{
    bool drawing = false;
    GameObject tool;
    GameObject line;
    LineRenderer lineRenderer;
    Vector3 lastPosition;

    // Use this for initialization
    void Start()
    {
        tool = GameObject.Find("MultiToolRight");
        
        // testing
        StartDrawing();
    }

    // Update is called once per frame
    private void Update()
    {
        if (drawing)
        {
            if (Vector3.Distance(lastPosition, tool.transform.position) > 0.02f)
            {
                // once the flystick has moved away enough from last position, add new position
                // this is done to prevent adding 60 positions per second while drawing
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, tool.transform.position);
                lastPosition = tool.transform.position;
            }
        }
    }

    public void StartDrawing()
    {
        if (!drawing)
        {
            // each line has to be its own object, as it can only have one renderer
            line = new GameObject();
            line.name = "line_" + System.Guid.NewGuid().ToString(); // not sure if a name is needed, but since we will be creating a bunch of those and later editing them...
            lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.numCapVertices = 1;
            lineRenderer.numCornerVertices = 5;
            lineRenderer.positionCount = 0;
            
            drawing = true;
        }
    }

    public void StopDrawing()
    {
        drawing = false;
    }
}
