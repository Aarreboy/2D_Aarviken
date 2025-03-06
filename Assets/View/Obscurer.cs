using UnityEngine;

public class Obscurer : MonoBehaviour
{
    public Transform playerReference;
    MeshFilter filter;
    Vector3[] vertices;

    void Awake()
    {
        filter = GetComponent<MeshFilter>();

        Mesh squareMesh = new Mesh();
        vertices = new Vector3[8];
        vertices[0] = new Vector3(-0.5f, 0, -0.5f);
        vertices[4] = vertices[0];
        vertices[1] = new Vector3(-0.5f, 0, 0.5f);
        vertices[5] = vertices[1];
        vertices[2] = new Vector3(0.5f, 0, 0.5f);
        vertices[6] = vertices[2];
        vertices[3] = new Vector3(0.5f, 0, -0.5f);
        vertices[7] = vertices[3];
        
        int[] triangles = new int[] {
            0,4,5,0,5,1,
            1,5,6,1,6,2,
            2,6,7,2,7,3,
            3,7,4,3,4,0 
        };

        squareMesh.vertices = vertices;
        squareMesh.triangles = triangles;
        squareMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 50);

        filter.mesh = squareMesh;
    }

    // Update is called once per frame
    void Update()
    {

        vertices[4] = vertices[0] + (transform.TransformPoint(vertices[0]) - playerReference.position).normalized * 20;
        vertices[5] = vertices[1] + (transform.TransformPoint(vertices[1]) - playerReference.position).normalized * 20;
        vertices[6] = vertices[2] + (transform.TransformPoint(vertices[2]) - playerReference.position).normalized * 20;
        vertices[7] = vertices[3] + (transform.TransformPoint(vertices[3]) - playerReference.position).normalized * 20;

        filter.mesh.vertices = vertices;
    }
}
