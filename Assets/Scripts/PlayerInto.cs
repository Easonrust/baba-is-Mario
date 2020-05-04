using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInto : MonoBehaviour
{
    public Transform m_CameraPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Camera.main.transform.position = m_CameraPosition.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
