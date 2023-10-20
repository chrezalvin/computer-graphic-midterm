using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customeBase : MonoBehaviour
{
   
    [SerializeField]
    public Material baseMaterial;

    public float width = 1.0f;
    public float height = 1.0f;
    public float thick = 1.0f;

    private Texture myTexture;

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        var vertices = new Vector3[8];
        myTexture = Resources.Load<Texture>("Textures/carbons");
        baseMaterial.mainTexture = myTexture;
       
       //layer 1
        vertices[0] = new Vector3(-width, -height, thick);
        vertices[1] = new Vector3(-width, height, thick);
        vertices[2] = new Vector3(width, height, thick);
        vertices[3] = new Vector3(width, -height, thick);
        
      //layer 2 
        vertices[4] = new Vector3(-width, -height, -thick);
        vertices[5] = new Vector3(-width, height, -thick);
        vertices[6] = new Vector3(width, height, -thick);
        vertices[7] = new Vector3(width, -height, -thick);

        mesh.vertices = vertices;

        mesh.triangles = new int[] {
            2, 1, 0,
            3, 2, 0, //face 1

            3, 0, 4,
            4, 7, 3, //face 2

            3, 6, 2,
            3, 7, 6, //face 3

            6, 5, 2,
            5, 1, 2, //face 4 

            5, 6, 4,
            6, 7, 4, //face 5

            1, 5, 4,
            4, 0, 1  //face 6
        };
        GetComponent<MeshFilter>().mesh = mesh;

        GetComponent<MeshRenderer>().material = baseMaterial;

    }
	   void Update()
    {
      
    }
}
