﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{

    public GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("重新开始"))
        {
            GlobalVar.playerCtrlNum = 0;
            SceneManager.LoadScene("level4");
        }
    }
}