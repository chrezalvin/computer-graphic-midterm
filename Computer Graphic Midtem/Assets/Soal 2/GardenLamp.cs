using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GardenLamp : MonoBehaviour
{
    // tracks sun rotation so it'll go on/off on cycles
    [SerializeField]
    Transform sun;

    [SerializeField]
    Transform table;

    [SerializeField]
    Material lampMaterial;

    [SerializeField]
    Material lightMaterial;

    public float lampWidth = 6;
    public float lampHeight = 6;

    private GameObject spotlight = null;

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

    private Mesh createLamp(float y_pos = 0, float size = 1, float expand = 1)
    {
        Mesh mesh = new Mesh();

        // create cube base
        Vector3[] coordinates = new Vector3[] {
            new Vector3(+size/2, y_pos, -size/2),
            new Vector3(+size/2, y_pos, +size/2),
            new Vector3(-size/2, y_pos, +size/2),
            new Vector3(-size/2, y_pos, -size/2),

            // the "expansion" in between 2 planes is used to add the light texture
            new Vector3(+(size/2 + expand), y_pos + size, -(size/2 + expand)),
            new Vector3(+(size/2 + expand), y_pos + size, +(size/2 + expand)),
            new Vector3(-(size/2 + expand), y_pos + size, +(size/2 + expand)),
            new Vector3(-(size/2 + expand), y_pos + size, -(size/2 + expand)),

            new Vector3(+size/2, y_pos + size * 2, -size/2),
            new Vector3(+size/2, y_pos + size * 2, +size/2),
            new Vector3(-size/2, y_pos + size * 2, +size/2),
            new Vector3(-size/2, y_pos + size * 2, -size/2),
        };

        // square as base
        int[] triangles = new int[coordinates.Length * 6 * 2];

        int iii = 0;
        iii = setQuad(triangles, iii, 2, 6, 1, 5);
        iii = setQuad(triangles, iii, 1, 0, 5, 4);
        iii = setQuad(triangles, iii, 0, 4, 3, 7);
        iii = setQuad(triangles, iii, 3, 2, 7, 6);

        iii = setQuad(triangles, iii, 2 + 4, 6 + 4, 1 + 4, 5 + 4);
        iii = setQuad(triangles, iii, 1 + 4, 0 + 4, 5 + 4, 4 + 4);
        iii = setQuad(triangles, iii, 0 + 4, 4 + 4, 3 + 4, 7 + 4);
        iii = setQuad(triangles, iii, 3 + 4, 2 + 4, 7 + 4, 6 + 4);

        mesh.vertices = coordinates;
        mesh.triangles = triangles;

        return mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject tower = Instantiate(new GameObject("Tower"), this.transform);
        tower.AddComponent<MeshFilter>().mesh = createCube(lampWidth, lampHeight); ;
        tower.AddComponent<MeshRenderer>().material = lampMaterial;

        GameObject light = Instantiate(new GameObject("Light"), this.transform);
        float offset = lampHeight - lampWidth * 2;
        light.AddComponent<MeshFilter>().mesh = createLamp(lampHeight - lampWidth * 2 * 1.2f, lampWidth, 1);
        light.AddComponent<MeshRenderer>().material = lightMaterial;

        GameObject lightPlacement = Instantiate(new GameObject("LightPoint"), this.transform);
        lightPlacement.transform.position = new Vector3(this.transform.position.x, lampHeight - lampWidth * 2 * 1.2f, this.transform.position.z);
        Light spot = lightPlacement.AddComponent<Light>(); ;
        spot.type = UnityEngine.LightType.Spot;
        spot.range = Vector3.Distance(spot.transform.position, table.transform.position);
        spot.spotAngle = 75f;
        spot.intensity = 3;

        spotlight = lightPlacement;
    }

    // Update is called once per frame
    void Update()
    {
        if (spotlight)
        {
            spotlight.transform.LookAt(table);
        }
    }
}
