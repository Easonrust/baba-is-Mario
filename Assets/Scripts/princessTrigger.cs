﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princessTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.SendMessage("BeWin",  SendMessageOptions.DontRequireReceiver);
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
