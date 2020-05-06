using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bullet_damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.SendMessage("BeShot", bullet_damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
        if (collision.gameObject.layer == 13)
        {
            Rigidbody2D c_body = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 v = c_body.velocity;
            v.x = gameObject.GetComponent<Rigidbody2D>().velocity.x/Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x)*100 * Time.deltaTime;
            c_body.velocity = v;
            var yk = collision.gameObject.GetComponent<Word>();
            yk.c_moveTimer = 0;

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
