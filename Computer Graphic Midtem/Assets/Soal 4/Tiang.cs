using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiang : MonoBehaviour
{

    //Declare Material
    [SerializeField]
    public Material tiangMaterial;
    public Texture myTexture;

    float width = .1f;
    float height = 1.6f;
    float thick = .1f;

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[24];
        var uvs = new Vector2[vertices.Length];
        //Load Texture
        myTexture = Resources.Load<Texture>("Textures/Iron");
        //Set Texture to Material
        tiangMaterial.mainTexture = myTexture;

        vertices[0] = new Vector3(width, height, thick);
        vertices[1] = new Vector3(width, -height, thick);
        vertices[2] = new Vector3(width, height , -thick);
        vertices[3] = new Vector3(width, -height, -thick);
     
        //1st
        uvs[0] = new Vector2(0.0f, 0.5f);
        uvs[1] = new Vector2(0.25f, 0.5f);
        uvs[2] = new Vector2(0.0f, 0.0f);
        uvs[3] = new Vector2(0.25f, 0.0f);

        vertices[4] = new Vector3(width, height , thick);
        vertices[5] = new Vector3(-width, height, thick);
        vertices[6] = new Vector3(width, height, -thick);
        vertices[7] = new Vector3(-width, height, -thick);

        //2nd
        uvs[4] = new Vector2(0.0f, 0.5f);
        uvs[5] = new Vector2(0.25f, 0.5f);
        uvs[6] = new Vector2(0.0f, 1.0f);
        uvs[7] = new Vector2(0.25f, 1.0f);

        vertices[8] = new Vector3(width, height, thick);
        vertices[9] = new Vector3(-width, height,thick);
        vertices[10] = new Vector3(width, -height ,thick);
        vertices[11] = new Vector3(-width, -height,thick);

        //3rd
        uvs[8] = new Vector2(0.5f, 0.5f);
        uvs[9] = new Vector2(0.25f, 0.5f);
        uvs[10] = new Vector2(0.25f, 1.0f);
        uvs[11] = new Vector2(0.5f, 1.0f);

        vertices[12] = new Vector3(-width, height , thick);
        vertices[13] = new Vector3(-width, -height, thick);
        vertices[14] = new Vector3(-width, height, -thick);
        vertices[15] = new Vector3(-width, -height, -thick);

        //4th
        uvs[12] = new Vector2(0.25f, 0.5f);
        uvs[13] = new Vector2(0.5f, 0.5f);
        uvs[14] = new Vector2(0.25f, 0.0f);
        uvs[15] = new Vector2(0.5f, 0.0f);

        vertices[16] = new Vector3(width, -height, thick);
        vertices[17] = new Vector3(-width, -height,thick);
        vertices[18] = new Vector3(width, -height ,-thick);
        vertices[19] = new Vector3(-width, -height,-thick);

        //5th
        uvs[16] = new Vector2(0.5f, 0.0f);
        uvs[17] = new Vector2(0.75f, 0.0f);
        uvs[18] = new Vector2(0.5f, 0.5f);
        uvs[19] = new Vector2(0.75f, 0.5f);

        vertices[20] = new Vector3(width, height , -thick);
        vertices[21] = new Vector3(-width, height, -thick);
        vertices[22] = new Vector3(width, -height, -thick);
        vertices[23] = new Vector3(-width, -height, -thick);

        //6th
        uvs[20] = new Vector2(0.5f, 0.5f);
        uvs[21] = new Vector2(0.75f, 0.5f);
        uvs[22] = new Vector2(0.5f, 1.0f);
        uvs[23] = new Vector2(0.75f, 1.0f);

        mesh.vertices = vertices;

        mesh.uv = uvs;

        mesh.triangles = new int[]{
            2,0,1,
            2,1,3,
            6,7,5,
            4,6,5,
            8,9,11,
            8,11,10,
            12,14,13,
            14,15,13,
            19,18,17,
            18,16,17,
            21,20,23,
            20,22,23

        };
        
        GetComponent<MeshFilter>().mesh = mesh;
        
        GetComponent<MeshRenderer>().material = tiangMaterial;
    }

    
    void Update()
    {
        
    }
}
