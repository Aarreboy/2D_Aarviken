using UnityEngine;

public class Obscurer : MonoBehaviour
{
    public Transform playerReference;
    protected MeshFilter filter;
    protected Vector3[] vertices;
    protected int halfCount;

    private void Start()
    {
        playerReference = GameObject.Find("Player").transform;
    }

    protected void CopyVerticesToSecondHalf()
    {
        for (int i = 0; i < halfCount; i++)
        {
            vertices[i + halfCount] = vertices[i];
        }
    }

    protected int[] GenerateTriangles()
    {
        int[] triangles = new int[6 * halfCount];

        for (int i = 0; i < halfCount - 1; i++)
        {
            triangles[i * 6] = i;
            triangles[i * 6 + 1] = i + halfCount;
            triangles[i * 6 + 2] = i + halfCount + 1;
            triangles[i * 6 + 3] = i;
            triangles[i * 6 + 4] = i + halfCount + 1;
            triangles[i * 6 + 5] = i + 1;

        }
        int last_i = halfCount - 1;
        triangles[last_i * 6] = last_i;
        triangles[last_i * 6 + 1] = last_i + halfCount;
        triangles[last_i * 6 + 2] = last_i + 1;
        triangles[last_i * 6 + 3] = last_i;
        triangles[last_i * 6 + 4] = last_i + 1;
        triangles[last_i * 6 + 5] = 0;

        return triangles;
    }

    void Update()
    {
        for (int i = 0; i < halfCount; i++)
        {
            vertices[i + halfCount] = vertices[i] + (transform.TransformPoint(vertices[i]) - playerReference.position).normalized * 20;
        }

        filter.mesh.vertices = vertices;
    }
}
