using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilDudeScript : MonoBehaviour
{

    public GameObject evilDudeRoot;
    public GameObject princessRoot;
    private PrincessObject po;
    private Vector3[] pointCloud;
    public LineRenderer r;
    public float tresholdMultiplicator;
    public PolygonCollider2D p;

    // Use this for initialization
    void Start()
    {
        princessRoot = GameObject.FindWithTag("Princess");
        evilDudeRoot = GameObject.FindWithTag("EvilDude");
        po = princessRoot.GetComponentInChildren<PrincessObject>();
        p = po.GetComponentInChildren<PolygonCollider2D>();
        //r = princessRoot.GetComponentInChildren<LineRenderer>();
    }

    public void handle2DCollisionExit(Collision2D collision)
    {
        Debug.Log("left: " + collision.collider.name + "(" + collision.otherCollider.name + ")");
    }

    public void handle2DTriggerExit(Collider2D collision)
    {
        /*    Debug.Log("left: " + collision.name);
            ColliderDistance2D cd = collision.Distance(po.GetComponentInChildren<PolygonCollider2D>());
            Debug.Log("Distance: " + cd.distance + " - " + cd.pointA + " - " + cd.pointB + " - " + cd.normal);
            collision.transform.parent.parent.position = new Vector3(collision.transform.position.x + cd.normal.x * -0.1f, collision.transform.position.y + cd.normal.y * -0.1f, collision.transform.position.z);
            //Debug.Log("Collider2D: "+collision.)*/
    }

    private void FixedUpdate()
    {
        Vector3 movementTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dirVector = movementTarget - princessRoot.transform.position;
        dirVector.Normalize();
        dirVector.z = 0;
        //        Debug.Log("Normalized direction: " + dirVector);
        dirVector = dirVector * -1f;

        int steps = 0;
        while (!p.OverlapPoint(movementTarget))
        {
            movementTarget = movementTarget + dirVector * 0.1f;
            steps++;
            if (steps >= 10000)
            {
                Debug.LogException(new System.Exception("Steps limit reached. movementTarget = " + movementTarget));
                return;
            }
        }

        if (p.OverlapPoint(movementTarget))
        {
            movementTarget.z = 0;
            evilDudeRoot.transform.position = movementTarget;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("pew.");
            // pew
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*if (!r)
        {
            r = princessRoot.GetComponentInChildren<LineRenderer>();
            if (!r)
                return;
        }
        pointCloud = new Vector3[r.positionCount];
        r.GetPositions(pointCloud);


        Vector3 movementTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movementTarget.z = 0;

        bool inRange = false;
        Vector3 smallest = Vector3.positiveInfinity;
        foreach (Vector3 v in pointCloud)
        {
            if (Vector3.Distance(v, movementTarget) <= (po.Radius * tresholdMultiplicator))
            {
                //Debug.Log("distance is OK!" + movementTarget);
                inRange = true;
            }
            if (Vector3.Distance(v, movementTarget) < Vector3.Distance(smallest, movementTarget))
                smallest = v;

        }
        if (!inRange)
            movementTarget = smallest;
        //Debug.Log("MovementTarget: " + movementTarget + "; Mouse Position: " + Input.mousePosition);


        evilDudeRoot.transform.position = movementTarget;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("pew.");
            // pew
        }
    */

    }
}
