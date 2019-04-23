using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private Transform t;
    public float speedX = 0.01f;
    public float speedY = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        t = this.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector3 p = t.position;
        p.x += speedX;
        p.y += speedY;
        t.position = p;
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}
