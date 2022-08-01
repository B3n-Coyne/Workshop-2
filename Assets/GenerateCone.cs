// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GenerateCone : MonoBehaviour
{

    [SerializeField] // SerializeField exposes 'changeColorTo' in the editor.
    private float radius = 1.0f;

    [SerializeField] // SerializeField exposes 'changeColorTo' in the editor.
    private int numSides = 4;


    private void Update()
    {
        // First we'll get the MeshFilter attached to this game object, in the
        // same way that we got the MeshRenderer component last week.
        var meshFilter = GetComponent<MeshFilter>();
        
        // Now we can create a cube mesh and assign it to the mesh filter.
        meshFilter.mesh = CreateMesh();
    }

    private Mesh CreateMesh()
    {

        float AngleOfSide = 360.0f / numSides;
        float halfAngle = AngleOfSide / 2;
        //var vec = new Vector3()

        // Step 0: Create the mesh object. This contains various data structures
        // that allow us to define complex 3D objects. Recommended reading:
        // - https://docs.unity3d.com/ScriptReference/Mesh.html
        var mesh = new Mesh
        {
            name = "Cone"
        };

        // Step 1: Define the vertices. These are "points" in 3D space that
        // allow us to construct 3D geometry. For everyone's sanity, we have
        // ordered them such that consecutive chunks of three vertices represent
        // individual triangles. This does not have to be the case though, and
        // depends on the way the mesh surface itself is defined (step 3).
        Vector3[] vertices = new Vector3[3 * numSides];
        int ctr = 0;

        for (int i = 0 ; i < numSides ; i++) {

            float center = i * AngleOfSide;
            

            //Add vertex
            vertices[ctr++] = new Vector3(0.0f, 1.0f, 0.0f);
            //Add bottom pt 1
            
            //Add bottom pt 2
            vertices[ctr++] = new Vector3(Mathf.Cos(center - halfAngle), -1.0f, Mathf.Sin(center-halfAngle));

            vertices[ctr++] = new Vector3(Mathf.Cos(center + halfAngle), -1.0f, Mathf.Sin(center+halfAngle));
        }


        mesh.SetVertices(vertices);


        // Step 2: Define the vertex colours. There is a one-to-one index
        // mapping between the vertices defined above and these colours below,
        // so the length of both arrays must be the same.
        Color[] colors = new Color[3*numSides];

        for (int i = 0 ; i < numSides * 3 ; i++) {
            colors[i] = Color.green;
        }

        mesh.SetColors(colors);

        // Step 3: Define the indices. The indices "connect" vertices together
        // in order to define the triangles that represent the mesh surface.
        // The indices are simply an integer array whereby consecutive chunks
        // of 3 integers represent individual triangles as index mappings. For
        // example: [2, 3, 9] defines a single triangle composed of the points
        // vertices[2], vertices[3], vertices[9] (and the respective colours).
        //
        // Luckily for us this is easy to automate because we already ordered
        // the above vertex and colour arrays just like this! We only need to
        // generate a range of integers from 0 to the # of vertices - 1:
        var indices = Enumerable.Range(0, mesh.vertices.Length).ToArray();
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        
        // Note that the topology argument specifies that we are in fact
        // defining *triangles* in our indices array. It is also possible to
        // define the mesh surface using quads (MeshTopology.Quads).

        return mesh;
    }
}