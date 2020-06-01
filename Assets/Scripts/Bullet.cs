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
            var dir= gameObject.GetComponent<Rigidbody2D>().velocity.x/Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x);
            Vector3 movement = new Vector3(dir * 1, 0,0);
            c_body.MovePosition(c_body.transform.position+movement );

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
