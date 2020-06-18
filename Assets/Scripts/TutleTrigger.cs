using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutleTrigger : MonoBehaviour
{

    public int m_damage = 10;
    public float speed = 2;
    public float x_limit = 2f;

    public Transform transform;
    public Rigidbody2D rd;
    public SpriteRenderer sr;
    // private bool isFacingRight = true;
    private float init_pos_x;
    


    // Start is called before the first frame update
    void Start()
    {
        init_pos_x = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(bool)this.GetComponent<PlayerCtrl>().ctrlState["you"])
        {
            float off_set = transform.position.x - init_pos_x;
            if (Mathf.Abs(off_set) >= x_limit)
            {
                Flip();
            }
            Vector2 next_pos = transform.position;
            next_pos.x += speed * Time.deltaTime;

            transform.position = next_pos;
        }
    }

    void Flip()
    {        
        speed *= -1;
        sr.flipX =!sr.flipX;
    }

}

