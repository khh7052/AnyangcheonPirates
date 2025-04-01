using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GridDrawer : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Vector2 pos;
    public int rowCount = 10, columnCount = 10;
    public float gridSize = 10f;
    public bool drawRow = true;
    public bool drawColumn = true;

    void OnValidate()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawGrid();
    }

    void DrawGrid()
    {
        if (rowCount + columnCount == 0)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        List<Vector3> points = new();


        float width = columnCount * gridSize;
        float height = rowCount * gridSize;

        Vector3 myPos = pos;
        Vector3 lastPos = myPos;
        points.Add(lastPos);

        lastPos.x += width;
        points.Add(lastPos);

        lastPos.y += height;
        points.Add(lastPos);

        lastPos.x -= width;
        points.Add(lastPos);

        lastPos.y -= height;
        points.Add(lastPos);

        for (int i = 0; i < rowCount; i++)
        {
            if (i % 2 == 0)
            {
                lastPos.x += width;
                points.Add(lastPos);
            }
            else
            {
                lastPos.x -= width;
                points.Add(lastPos);
            }

            if (i == rowCount - 1) break;
            lastPos.y += gridSize;
            points.Add(lastPos);
        }

        lastPos.y -= height- gridSize;
        points.Add(lastPos);

        if (drawColumn)
        {
            if (rowCount % 2 != 0)
            {
                lastPos.x -= width;
                points.Add(lastPos);
            }

            for (int i = 0; i < columnCount; i++)
            {
                if (i != columnCount - 1)
                {
                    lastPos.x += gridSize;
                    points.Add(lastPos);
                }

                if (i % 2 == 0)
                {
                    lastPos.y += height;
                    points.Add(lastPos);
                }
                else
                {
                    lastPos.y -= height;
                    points.Add(lastPos);
                }
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

}
