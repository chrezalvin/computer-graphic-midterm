using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customeCube : MonoBehaviour
{
    [SerializeField]
    Material material;

    [SerializeField]
    Color color;

    public float cubeWidth = 0.1f;  
    public float cubeHeight = 1; 

    private Texture myTexture;
     

    private Mesh rectangle(float width, float height)
    {
        Mesh mesh = new Mesh();
        myTexture = Resources.Load<Texture>("Textures/wood");
        material.mainTexture = myTexture;

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(width / 2, 0, -width / 2),
            new Vector3(width / 2, 0, width / 2),
            new Vector3(-width / 2, 0, width / 2),

            new Vector3(-width / 2, 0, -width / 2),
            new Vector3(width / 2, height, -width / 2),
            new Vector3(width / 2, height, width / 2),

            new Vector3(-width / 2, height, width / 2),
            new Vector3(-width / 2, height, -width / 2),

            new Vector3(width / 2, 0, -width / 2), 

            new Vector3(width / 2, 0, width / 2),
            new Vector3(-width / 2, 0, width / 2),
            new Vector3(-width / 2, 0, -width / 2)
        };

        int[] triangles = new int[]
        {
          
            0, 1, 5, 
            0, 5, 4, 

            3, 7, 6, 
            3, 6, 2, 

            1, 2, 6, 
            1, 6, 5, 

            0, 4, 7,
            0, 7, 3,  

            0, 2, 1, 
            0, 3, 2, // Bawah

            4, 5, 6, 
            4, 6, 7, // Atas
        };

       Vector2[] uvs = new Vector2[]
      {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
         };


         Vector3[] normals = new Vector3[]
        {
            
            Vector3.back, 
            Vector3.forward, 
            Vector3.right, 
            Vector3.left, 

            Vector3.down, 
            Vector3.up,   
        };

         for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.normals = normals;

        return mesh;
    }

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        Mesh mesh = rectangle(cubeWidth, cubeHeight);
        meshFilter.mesh = mesh;

        meshRenderer.material = material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
