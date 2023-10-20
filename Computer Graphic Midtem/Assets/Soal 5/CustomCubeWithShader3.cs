using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCubeWithShader3 : MonoBehaviour
{
    [SerializeField]
    private static Color[] grayValuedColors =
    {
        new Color(220,220,220),
        new Color(211,211,211),
        new Color(192,192,192),
        new Color(169,169,169),
        new Color(128,128,128),
        new Color(105,105,105),
        new Color(119,136,153),
        new Color(112,128,144),
        new Color(47,79,79),
        new Color(0,0,0)
    };
    public static Color getRandomGrayValuedColor()
    {
        return grayValuedColors[Random.Range(0, grayValuedColors.Length)];
    }

    public static Color getRandomRGB()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    [SerializeField]
    Material cubeMaterial;

    [SerializeField]
    Texture texture;

    public float spinSpeed;
    public Vector3 rotateAmount;

    float width = 3.0f;
    float height = 3.0f;
    float thickness = 3.0f;

    int acc = 0;

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();

        var vertices = new Vector3[24];

        int[,] map = new int[24,3] {
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

        cubeMaterial.mainTexture = texture;

        for (int iii = 0; iii < vertices.Length; ++iii)
        {
            vertices[iii] = new Vector3(width * map[iii, 0], height * map[iii, 1], thickness * map[iii, 2]);
        }        
        
        for (int iii = 0; iii < uvVertices.Length; ++iii)
        {
            uvVertices[iii] = new Vector3(width * uvVecticesMapping[iii, 0], height * uvVecticesMapping[iii, 1], thickness * map[iii, 2]);
        }

        uv[0] = new Vector2(1, 1);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);
        uv[3] = new Vector2(0, 0);

        mesh.uv = uv;
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

        var colors = new Color32[vertices.Length];
        for (int iii = 0; iii < vertices.Length; ++iii)
            colors[iii] = getRandomGrayValuedColor();


        mesh.colors32 = colors;

        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = cubeMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime / spinSpeed);
    }

    private void FixedUpdate()
    {
    }
}
