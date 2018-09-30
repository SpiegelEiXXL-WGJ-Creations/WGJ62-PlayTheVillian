using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessObject : MonoBehaviour
{


    //public float ThetaScale = 0.01f;
    public float Radius = 3f;
    public int Points = 20;
    //public int Size;
    //public float Theta = 0f;
    public Material myMat;
    public EvilDudeScript eds;


    //private LineRenderer LineDrawer;
    //private Vector3[] lines;
    private float radiusSet = 0f;
    private int pointsSet = 0;
    private MeshFilter mf;
    private Rigidbody2D rb;
    private MeshRenderer mr;

    void Start()
    {
        /*LineDrawer = gameObject.AddComponent<LineRenderer>();
        LineDrawer.materials = new Material[1] { myMat };
        LineDrawer.sortingOrder = 2;
        LineDrawer.loop = true;*/
        eds = GameObject.FindWithTag("EvilDude").GetComponentInChildren<EvilDudeScript>();

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;


        polyCollider = gameObject.AddComponent<PolygonCollider2D>();
        mf = gameObject.AddComponent<MeshFilter>();
        mr = gameObject.AddComponent<MeshRenderer>();
        mr.materials = new Material[1] { myMat };
        polyCollider.isTrigger = true;
        /*Color q = LineDrawer.material.color;
        q.a = 0.85f;
        LineDrawer.material.color = q;*/

        //evalLines();
        PolyMesh(Radius, Points);

    }

    public PolygonCollider2D polyCollider;

    void PolyMesh(float radius, int n)
    {
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        //verticies
        List<Vector3> verticiesList = new List<Vector3> { };
        float x;
        float y;
        for (int i = 0; i < n; i++)
        {
            x = radius * Mathf.Sin((2 * Mathf.PI * i) / n);
            y = radius * Mathf.Cos((2 * Mathf.PI * i) / n);
            verticiesList.Add(new Vector3(x, y, 0f));
        }
        Vector3[] verticies = verticiesList.ToArray();

        //triangles
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (n - 2); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }
        int[] triangles = trianglesList.ToArray();

        //normals
        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < verticies.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();

        //initialise
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.normals = normals;

        //polyCollider
        polyCollider.pathCount = 1;

        List<Vector2> pathList = new List<Vector2> { };
        for (int i = 0; i < n; i++)
        {
            pathList.Add(new Vector2(verticies[i].x, verticies[i].y));
        }
        Vector2[] path = pathList.ToArray();

        polyCollider.SetPath(0, path);
        pointsSet = Points;
        radiusSet = Radius;
    }

    //void evalLines()
    //{
    //    /*Size = (int)((1f / ThetaScale) + 2f);
    //    // LineDrawer.SetVertexCount(Size);
    //    LineDrawer.positionCount = Size;

    //    lines = new Vector3[Size];
    //    Theta = 0f;

    //    for (int i = 0; i < Size; i++)
    //    {
    //        Theta += (2.0f * Mathf.PI * ThetaScale);
    //        float x = radius * Mathf.Cos(Theta);
    //        float y = radius * Mathf.Sin(Theta);
    //        lines[i] = new Vector3(x + gameObject.transform.position.x, y + gameObject.transform.position.y, 0);
    //    }
    //    ThetaSet = ThetaScale;
    //    radiusSet = radius;*/
    //LineDrawer.positionCount = Points - 1;

    //    lines = new Vector3[LineDrawer.positionCount];

    //    //lines[Points] = new Vector3(transform.position.x + radius, transform.position.y, 0);
    //    float slice = 2 * Mathf.PI / Points;
    //    for (int i = 1; i < Points; i++)
    //    {
    //        float angle = slice * i;
    //        float newX = transform.position.x + radius * Mathf.Cos(angle);
    //        float newY = transform.position.y + radius * Mathf.Sin(angle);
    //        lines[i - 1] = new Vector3(newX, newY, 0);
    //    }
    //    //lines.
    //    radiusSet = radius;

    //}


    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Radius - radiusSet) > 0.001f || Points != pointsSet)
            PolyMesh(Radius, Points);


        //LineDrawer.SetPositions(lines);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        eds.handle2DTriggerExit(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        eds.handle2DCollisionExit(collision);
    }

}
