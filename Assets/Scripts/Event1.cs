using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Rigidbody2D m_toge;
    public float speed = -10f;
    public int MaxTurns = 3;
    private int turns=0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            turns++;
            print("success");
            Vector2 v = new Vector2(speed, 0);
            m_toge.velocity = v * Time.deltaTime;
            if(turns>=MaxTurns)
            {

                Destroy(gameObject);
            }
        }
    }
}
