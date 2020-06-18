using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playableControl : MonoBehaviour
{
    public string condition;
    public float movespeed = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(condition);
        if (condition == "you")
        {
            gameObject.transform.Translate(Vector2.left * movespeed * Time.deltaTime);
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    Debug.Log("move");
            //    gameObject.transform.Translate(Vector2.left * movespeed * Time.deltaTime);
            //}
        }
        if (condition == "dead")
        {
            Destroy(gameObject);
        }
    }
}
