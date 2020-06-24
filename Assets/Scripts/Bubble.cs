
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
     public GameObject m_bubble;
     float timer_f=0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVar.changeScene || GlobalVar.bossLevel)
        {
            timer_f += Time.deltaTime;
            if (timer_f < 1)
                m_bubble.GetComponent<Renderer>().enabled = false;
            if (timer_f > 1 && timer_f < 5)
                m_bubble.GetComponent<Renderer>().enabled = true;

            if (timer_f > 5)
            {
                m_bubble.SetActive(false);
                GlobalVar.changeScene = false;
                GlobalVar.bossSpeaking = false;
                GlobalVar.roundStart = true;
            }

        }
        else
        {
            m_bubble.SetActive(false);
        }
    }
}

