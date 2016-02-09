using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Ellipse : MonoBehaviour
{
    /*
    public float a = 5;
    public float b = 3;
    public float h = 1;
    public float k = 1;
    public float theta = 45;
    public int resolution = 100;
    */
    private Vector3[] positions;

    void Start()
    {

    }
    public void DrawCircle(float radius, float h, float k, float height)
    {
        DrawEllipse(radius, radius, h, k, height, 0f, 100);
    }
    public void DrawEllipse(float a, float b, float h, float k, float height, float theta, int resolution)
    {
        positions = CreateEllipse(a, b, h, k, height, theta, resolution);
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetVertexCount(resolution + 1);
        for (int i = 0; i <= resolution; i++)
        {
            lr.SetPosition(i, positions[i]);
        }
    }
    Vector3[] CreateEllipse(float a, float b, float h, float k, float height, float theta, int resolution)
    {

        positions = new Vector3[resolution + 1];
        Quaternion q = Quaternion.AngleAxis(theta, Vector3.forward);
        Vector3 center = new Vector3(h, height, k);

        for (int i = 0; i <= resolution; i++)
        {
            float angle = (float)i / (float)resolution * 2.0f * Mathf.PI;
            positions[i] = new Vector3(a * Mathf.Cos(angle), 0f,  b * Mathf.Sin(angle));
            positions[i] = q * positions[i] + center;
        }

        return positions;
    }
}