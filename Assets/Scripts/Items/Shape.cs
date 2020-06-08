using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shape
{
    public static Vector2[] randd =
    {
        new Vector2(0.29f, 0.59f),
        new Vector2(0.36f, 0.71f),
        new Vector2(0.43f, 0.66f),
        new Vector2(0.61f, 0.65f),
        new Vector2(0.6f, 0.8f),
        new Vector2(0.54f, 0.89f),
        new Vector2(0.66f, 0.95f),
        new Vector2(0.85f, 0.89f),
        new Vector2(0.82f, 0.68f),
        new Vector2(0.74f, 0.51f),
        new Vector2(0.86f, 0.42f),
        new Vector2(0.84f, 0.25f),
        new Vector2(0.57f, 0.25f),
        new Vector2(0.49f, 0.45f)
    };

    public static Vector2[] square =
    {
        new Vector2(0, 0),
        new Vector2(0, 1),
        new Vector2(1, 1),
        new Vector2(1, 0)
    };

    public static Vector2[] triangle =
    {
        new Vector2(0.48f, 0.93f),
        new Vector2(0.94f, 0.12f),
        new Vector2(0.11f, 0.08f)
    };

    public static Vector2[] heh =
    {
        new Vector2(0.26f, 0.06f),
        new Vector2(0.75f, 0.06f),
        new Vector2(1f, 0.5f),
        new Vector2(0.7f, 0.82f),
        new Vector2(0.49f, 0.56f),
        new Vector2(0.06f, 0.52f)
    };

    public static Vector2[] hexagon =
    {
        new Vector2(0.06f, 0.29f),
        new Vector2(0.5f, 0.11f),
        new Vector2(0.93f, 0.31f),
        new Vector2(0.91f, 0.7f),
        new Vector2(0.49f, 0.92f),
        new Vector2(0.06f, 0.67f)
    };

    public static bool[,] PolygonFillMask(
        int height, int width, Vector2[] templatePoints, bool isPointNormalized = false)
    {
        Vector2[] points = templatePoints.Clone() as Vector2[];
        if (isPointNormalized)
        {
            Vector2 scale = new Vector2(width, height);
            for (int i = 0; i < templatePoints.Length; i++)
            {
                points[i] = Vector2.Scale(templatePoints[i], scale);
            }
        }

        bool[,] mask = new bool[height, width];

        int numPoints = points.Length;
        int[] nodeX = new int[numPoints];

        //  Loop through the rows of the image.
        for (int pixelY = 0; pixelY < height; pixelY++)
        {
            //  Build a list of nodes.
            int nodes = 0;
            int j = numPoints - 1;
            for (int i = 0; i < numPoints; i++)
            {
                Vector2 point_i = points[i];
                Vector2 point_j = points[j];
                if (point_i.y < pixelY && point_j.y >= pixelY || point_j.y < pixelY && point_i.y >= pixelY)
                {
                    nodeX[nodes++] = (int)(point_i.x + (pixelY - point_i.y) / (point_j.y - point_i.y) * (point_j.x - point_i.x));
                }
                j = i;
            }

            //  Sort the nodes, via a simple “Bubble” sort.
            int idx = 0;
            while (idx < nodes - 1)
            {
                if (nodeX[idx] > nodeX[idx + 1])
                {
                    int swap = nodeX[idx];
                    nodeX[idx] = nodeX[idx + 1];
                    nodeX[idx + 1] = swap;
                    if (idx > 0) idx--;
                }
                else
                {
                    idx++;
                }
            }

            //  Fill the pixels between node pairs.
            for (int i = 0; i < nodes; i += 2)
            {
                if (nodeX[i] >= width)
                    break;
                if (nodeX[i + 1] > 0)
                {
                    if (nodeX[i] < 0)
                        nodeX[i] = 0;
                    if (nodeX[i + 1] > width)
                        nodeX[i + 1] = width;
                    for (int pixelX = nodeX[i]; pixelX < nodeX[i + 1]; pixelX++)
                        mask[pixelY, pixelX] = true;
                }
            }
        }

        return mask;
    }

    public static bool[,] GenerateMask(Texture2D texture)
    {
        bool[,] mask = new bool[texture.height, texture.width];

        for (int i = 0; i < texture.height; i++)
        {
            for (int j = 0; j < texture.width; j++)
            {
                mask[i, j] = texture.GetPixel(j, i).a == 1;
            }
        }
        return mask;
    }

    public static void PrintBool(bool[,] b)
    {
        int height = b.GetLength(0);
        int width = b.GetLength(1);
        for (int i = 0; i < height; i++)
        {
            string s = "";
            for (int j = 0; j < width; j++)
            {
                s += b[i, j] ? '1' : '0';
            }
            Debug.Log(s);
        }
    }
}
