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
            Vector2 local = collision.gameObject.transform.position;
            Vector2 distance=local;
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x>0)
            {
                distance = new Vector2(local.x+1, local.y);
                
            }
            else if(gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                distance = new Vector2(local.x-1, local.y);
            }
            collision.gameObject.transform.position = distance;
        }
    }
    //private void OnCollisieonEnter2D(Collision2D coll)
    //{
    //    coll.collider.SendMessage("BeShot", bullet_damage, SendMessageOptions.DontRequireReceiver);
    //    Destroy(gameObject);
    //    if (coll.gameObject.layer == 13)
    //    {
    //        if (coll.contacts[0].normal.y == -1)//从上方碰撞
    //        {

    //        }
    //        else if (coll.contacts[0].normal.y == 1)//从下方碰撞
    //        {

    //        }
    //        else if (coll.contacts[0].normal.x == -1)//左边碰撞
    //        {
    //            //coll.collider.gameObject.transform.Translate(64, 0, 0);
    //        }
    //        else if (coll.contacts[0].normal.x == 1)//右边碰撞
    //        {
    //            //coll.collider.gameObject.transform.Translate(64, 0, 0);
    //        }
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
