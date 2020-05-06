using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    Rigidbody2D c_body;
    public float c_moveTimer;
    // Start is called before the first frame update
    void Start()
    {
        c_body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (c_moveTimer!=-1&&c_moveTimer < 0.1f)
        {
            c_moveTimer += Time.deltaTime;
        }
        else
        {
            Vector2 v = new Vector2(0, 0);
            c_moveTimer = -1;
            c_body.velocity = v;
        }
    }
}
