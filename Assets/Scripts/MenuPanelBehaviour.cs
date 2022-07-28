using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelBehaviour : MonoBehaviour
{
    private static Color DEFAULT_COLOR = Color.white;

    private static Color SELECTED_COLOR = Color.green;

    private Color currentColor = DEFAULT_COLOR;

    private bool updateColor = false;

    private long zmienna = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        zmienna++;
        if (zmienna == 100)
        {
            zmienna = 0;
            ToggleColor();
        }
        if (updateColor)
        {
            updateColor = false;
            var renderer = gameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", currentColor);
        }
    }

    void ToggleColor()
    {
        updateColor = true;
        if (currentColor == DEFAULT_COLOR)
        {
            currentColor = SELECTED_COLOR;
            return;
        }
        currentColor = DEFAULT_COLOR;
    }
}
