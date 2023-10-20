using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomCube : MonoBehaviour
{
    [SerializeField]
    Material material;

    [SerializeField]
    Color color;

    [SerializeField]
    Texture texture;

    public float cubeWidth = 6;
    public float cubeHeight = 6;

    private int setQuad(int[] triangles, int iii, int v00, int v10, int v01, int v11)
    {
        triangles[iii] = v00;
        triangles[iii + 1] = triangles[iii + 4] = v01;
        triangles[iii + 2] = triangles[iii + 3] = v10;
        triangles[iii + 5] = v11;

        iii += 6;

        // reverse
        triangles[iii] = v00;
        triangles[iii + 1] = triangles[iii + 4] = v10;
        triangles[iii + 2] = triangles[iii + 3] = v01;
        triangles[iii + 5] = v11;

        return iii + 6;
    }

    private Mesh createCube(float width = 1, float height = 1)
    {
        Mesh mesh = new Mesh();

        // base it at center bottom of the cube
        Vector3[] coordinates = new Vector3[] {
            new Vector3(+width/2, 0, -width/2),
            new Vector3(+width/2, 0, +width/2),
            new Vector3(-width/2, 0, +width/2),
            new Vector3(-width/2, 0, -width/2),
            new Vector3(+width/2, +height, -width/2),
            new Vector3(+width/2, +height, +width/2),
            new Vector3(-width/2, +height, +width/2),
            new Vector3(-width/2, +height, -width/2),
        };

        // square as base
        int[] triangles = new int[coordinates.Length * 6 * 2];

        int iii = 0;
        iii = setQuad(triangles, iii, 3, 2, 0, 1);
        iii = setQuad(triangles, iii, 2, 6, 1, 5);
        iii = setQuad(triangles, iii, 1, 0, 5, 4);
        iii = setQuad(triangles, iii, 0, 4, 3, 7);
        iii = setQuad(triangles, iii, 3, 7, 2, 6);


        mesh.vertices = coordinates;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return mesh;
    }

    private Mesh createCubeWithNormal(float width = 1, float height = 1)
    {
        Mesh mesh = new Mesh();

        var vertices = new Vector3[24];

        int[,] map = new int[24, 3] {
            // x positive
            {1, 1, 1 },
            {1, -1, 1 },
            {1, 1, -1 },
            {1, -1, -1 },

            // y positive
            {1, 1, 1 },
            {-1, 1, 1 },
            {1, 1, -1 },
            {-1, 1, -1 },

            // z positive
            {1, 1, 1 },
            {-1, 1, 1 },
            {1, -1, 1 },
            {-1, -1, 1 },
            
            // x negative
            {-1, 1, 1 },
            {-1, -1, 1 },
            {-1, 1, -1 },
            {-1, -1, -1 },

            // y negative
            {1, -1, 1 },
            {-1, -1, 1 },
            {1, -1, -1 },
            {-1, -1, -1 },

            // z negative
            {1, 1, -1 },
            {-1, 1, -1 },
            {1, -1, -1 },
            {-1, -1, -1 },
        };

        var uvVertices = new Vector3[4];
        var uv = new Vector2[4];
        var uvVecticesMapping = new int[4, 3] {
            {1, 1, 1 },
            {-1, 1, 1 },
            {1, 1, -1 },
            {-1, 1, -1 },
        };

        material.mainTexture = texture;

        for (int iii = 0; iii < vertices.Length; ++iii)
        {
            vertices[iii] = new Vector3(width * map[iii, 0], height * map[iii, 1], width * map[iii, 2]);
        }

        for (int iii = 0; iii < uvVertices.Length; ++iii)
        {
            uvVertices[iii] = new Vector3(width * uvVecticesMapping[iii, 0], height * uvVecticesMapping[iii, 1], width * map[iii, 2]);
        }

        uv[0] = new Vector2(1, 1);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);
        uv[3] = new Vector2(0, 0);

        // mesh.uv = uv;
        mesh.vertices = vertices;

        mesh.triangles = new int[] {
            2,  0,  1,
            2,  1,  3,
            6,  7,  5,
            4,  6,  5,
            8,  9,  11,
            8,  11, 10,
            12, 14, 13,
            14, 15, 13,
            19, 18, 17,
            18, 16, 17,
            21, 20, 23,
            20, 22, 23
            };

        mesh.RecalculateNormals();
        return mesh;
    }

    // Start is called before the first frame update
    void Start()
    {

        Mesh cube = createCubeWithNormal(cubeWidth, cubeHeight);

        var colors = new Color32[cube.vertices.Length];
        for (int iii = 0; iii < colors.Length; ++iii)
            colors[iii] = color;


        cube.colors32 = colors;

        GetComponent<MeshFilter>().mesh = cube;
        GetComponent<MeshRenderer>().material = material;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
