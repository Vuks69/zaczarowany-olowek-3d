using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawBehaviour : MonoBehaviour, IDrawingTool
{
    bool drawing = false;
    GameObject tool;
    GameObject line;
    LineRenderer lineRenderer;
    List<Vector3> positions;
    Vector3 lastPosition;

    // Use this for initialization
    void Start()
    {
        tool = GameObject.Find("MultiToolRight");
        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.numCapVertices = 5;
        lineRenderer.numCornerVertices = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (drawing)
        {
            if (Vector3.Distance(lastPosition, tool.transform.position) > 1.0f)
            {
                positions.Add(tool.transform.position);
                lastPosition = tool.transform.position;
                lineRenderer.SetPositions(positions.ToArray());
            }
        }
    }

    public void StartDrawing()
    {
        if (!drawing)
        {
            GameObject line = new GameObject();
            line.name = "line_" + System.Guid.NewGuid().ToString();
            line.AddComponent(typeof(LineRenderer));
            positions.Add(tool.transform.position);
            lastPosition = tool.transform.position;
            drawing = true;
        }
    }

    public void StopDrawing()
    {
        drawing = false;
        positions.Clear();
    }
}
