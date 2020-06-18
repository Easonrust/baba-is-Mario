using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 vec = Vector3.zero;

        //vec.x = PlayerPrefs.GetFloat("CameraPosX", 0.5f);
        //vec.y = PlayerPrefs.GetFloat("CameraPosY", 0.5f);
        //vec.z = PlayerPrefs.GetFloat("CameraPosZ", -10);
        //Camera.main.transform.position = vec;

        //vec.x = PlayerPrefs.GetFloat("PlayerPosX", -8.5f);
        //vec.y = PlayerPrefs.GetFloat("PlayerPosY", 7.5f);
        //vec.z = PlayerPrefs.GetFloat("PlayerPosZ", 0);
        //m_player.transform.position = vec;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("重新开始"))
        {
            GlobalVar.playerCtrlNum = 1;
            SceneManager.LoadScene("SampleScene");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
