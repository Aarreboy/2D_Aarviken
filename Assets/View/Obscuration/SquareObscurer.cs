using UnityEngine;

public class SquareObecurer : Obscurer
{
    void Awake()
    {
        filter = GetComponent<MeshFilter>();

        Mesh squareMesh = new Mesh();
        vertices = new Vector3[8];
        halfCount = vertices.Length / 2;
        vertices[0] = new Vector3(-0.5f, 0, -0.5f);
        vertices[1] = new Vector3(-0.5f, 0, 0.5f);
        vertices[2] = new Vector3(0.5f, 0, 0.5f);
        vertices[3] = new Vector3(0.5f, 0, -0.5f);

        CopyVerticesToSecondHalf();

        squareMesh.vertices = vertices;
        squareMesh.triangles = GenerateTriangles();
        squareMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 50);

        filter.mesh = squareMesh;
    }
}
