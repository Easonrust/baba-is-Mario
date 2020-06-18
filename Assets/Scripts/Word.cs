using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask wordLayer;
    public LayerMask groundLayer;
    public float distance1;
    public float distance2;
    public Transform left;
    public Transform right;
    public Transform up;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ok");

        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 13)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity.Set(0, 0);

            Debug.Log("no");
        }
        else if(collision.gameObject.layer == 10)
        {
            var hit_dir = 0;
            RaycastHit2D hit_ground_left = Physics2D.Raycast(transform.position, Vector2.left, distance2, groundLayer);
            RaycastHit2D hit_ground_right = Physics2D.Raycast(transform.position, Vector2.right, distance2, groundLayer);
            RaycastHit2D hit_word_left = Physics2D.Raycast(left.transform.position, Vector2.left, distance1, wordLayer);
            RaycastHit2D hit_word_right = Physics2D.Raycast(left.transform.position, Vector2.right, distance1, wordLayer);
            if (hit_word_left.collider != null|| hit_ground_left.collider!=null)
            {
                hit_dir = -1;
                Debug.Log("hit");
            }

            if (hit_word_right.collider != null || hit_ground_right.collider != null)
            {
                hit_dir = 1;
                Debug.Log("hit");
            }

            Rigidbody2D c_body = collision.gameObject.GetComponent<Rigidbody2D>();
            var dir = c_body.velocity.x / Mathf.Abs(c_body.velocity.x);
            if (dir != hit_dir)
            {
                Vector3 movement = new Vector3(dir * 1, 0, 0);
                gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.GetComponent<Rigidbody2D>().transform.position + movement);

            }
            
        }
        else if (collision.gameObject.layer == 9)
        {
            var hit_dir = 0;
            RaycastHit2D hit_ground_up = Physics2D.Raycast(transform.position, Vector2.up, distance2, groundLayer);
            RaycastHit2D hit_word_up = Physics2D.Raycast(left.transform.position, Vector2.up, distance1, wordLayer);
            if (hit_word_up.collider != null || hit_ground_up.collider != null)
            {
                hit_dir = 1;
                Debug.Log("hit");
            }


            Rigidbody2D c_body = collision.gameObject.GetComponent<Rigidbody2D>();
            var dir = c_body.velocity.x / Mathf.Abs(c_body.velocity.x);
            if (dir != hit_dir)
            {
                Vector3 movement = new Vector3(0, dir * 1, 0);
                gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.GetComponent<Rigidbody2D>().transform.position + movement);

            }
        }
    }
}
