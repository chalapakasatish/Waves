using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawRadiusInGame : MonoBehaviour
{
    public float radius;
    public int segments = 100;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        line.loop = true; // Close the circle
        radius = GetComponent<ObjectDetectionRadius>().detectionRadius;
        DrawCircle();
    }

    void DrawCircle()
    {
        float angle = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, -1, z));

            angle += 360f / segments;
        }
    }
}
