using UnityEngine;
using System;

namespace Assets.Scripts.Actions
{
    public static class DrawingToolShape
    { 
        public static Vector3[] rectangle()
        {
            var arr = new Vector3[]
            {
                new Vector3(0.5f, -0.5f, 0f),
                new Vector3(0.5f, 0.5f, 0f),
                new Vector3(-0.5f, 0.5f, 0f),
                new Vector3(-0.5f, -0.5f, 0f)
            };

            return arr;
        }

        public static Vector3[] star()
        {
            Vector3[] starVertices = new Vector3[10];
            float r1 = 1f;
            float r2 = 0.5f;
            for (int i = 0; i < 5; i++)
            {
                starVertices[i * 2] = new Vector3(r1 * Mathf.Cos(2f * Mathf.PI * (float)i / 5f + Mathf.PI / 2f), r1 * Mathf.Sin(2f * Mathf.PI * (float)i / 5f + Mathf.PI / 2f));
                starVertices[i * 2 + 1] = new Vector3(r2 * Mathf.Cos(2f * Mathf.PI * (float)i / 5f + Mathf.PI / 2f + 2f * Mathf.PI / 10f), r2 * Mathf.Sin(2f * Mathf.PI * (float)i / 5f + Mathf.PI / 2f + 2f * Mathf.PI / 10f));
            }

            return starVertices;
        }

        public static Vector3[] triangle()
        {
            var arr = new Vector3[]
            {
                new Vector3(0.5f, -0.5f, 0f),
                new Vector3(0f, 0.5f, 0f),
                new Vector3(-0.5f, -0.5f, 0f)
            };

            return arr;
        }

        public static Vector3[] circle()
        {
            int n = 100;
            Vector3[] verticiesList = new Vector3[n];
            float x;
            float y;
            for (int i = 0; i < n; i++)
            {
                x = Mathf.Sin((2 * Mathf.PI * i) / n);
                y = Mathf.Cos((2 * Mathf.PI * i) / n);
                verticiesList[n - 1 - i] = new Vector3(x, y, 0f);   // vertices must be in counter clockwise order
            }

            return verticiesList;
        }

        public static Vector3[] plus()
        {
            var arr = new Vector3[]
            {
                new Vector3(-1f, 0.2f, 0f),
                new Vector3(-1f, -0.2f, 0f),
                new Vector3(-0.2f, -0.2f, 0f),
                new Vector3(-0.2f, -1f, 0f),
                new Vector3(0.2f, -1f, 0f),
                new Vector3(0.2f, -0.2f, 0f),
                new Vector3(1f, -0.2f, 0f),
                new Vector3(1f, 0.2f, 0f),
                new Vector3(0.2f, 0.2f, 0f),
                new Vector3(0.2f, 1f, 0f),
                new Vector3(-0.2f, 1f, 0f),
                new Vector3(-0.2f, 0.2f, 0f)
            };

            return arr;
        }

        public static Vector3[] custom(float size)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}