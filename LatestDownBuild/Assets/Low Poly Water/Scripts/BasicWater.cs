using UnityEngine;
using System.Collections;

/*
 Simple Water botched from https://www.reddit.com/r/gamedev/comments/2xdi7c/generating_procedural_water_in_unity_3d_includes/
 and mesh generation added from http://catlikecoding.com/unity/tutorials/procedural-grid/
     
    This is not intended to be a functional script and is only used to showcase the effect
     */


public class BasicWater : MonoBehaviour
{
    public int xSize;
    public int ySize;
    public float waveSpeed = 0.5f;
    public float waveScale = 0.25f;
    public float waveNoise = 1;
    public float waveNoiseStrength = 1;
    float random;

    Vector3[] baseHeight;
    Vector3[] startVerts;

    Mesh mesh;
    private void Start()
    {
        random = Random.Range(0f, 1f);
         Generate();
        startVerts = new Vector3[GetComponent<MeshFilter>().mesh.vertices.Length];
    }
    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Water Grid";

        baseHeight = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                baseHeight[i] = new Vector3(x, 0 ,y);
            }
        }
        mesh.vertices = baseHeight;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
    }
    private void OnApplicationQuit()
    {
        GetComponent<MeshFilter>().mesh.vertices = startVerts;
    }

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        if (baseHeight == null)
            baseHeight = mesh.vertices;

        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            vertex.y += Mathf.Sin(Time.time * waveSpeed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * waveScale;
            vertex.y += Mathf.PerlinNoise(baseHeight[i].x + waveNoise, baseHeight[i].y + Mathf.Sin(Time.time * 0.2f)) * waveNoiseStrength;
            vertex.y += Mathf.PerlinNoise(baseHeight[i].x + Time.time * (waveNoise/100), baseHeight[i].z + Time.time * (waveNoise/100)) * waveNoiseStrength;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}