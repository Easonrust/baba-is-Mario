using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVar.levelWin == true)
        {
            BlackFader.GoToScene("2-4", UnityEngine.SceneManagement.LoadSceneMode.Single, 2f, atLoading, atFinish);
        }
    }

    private void atLoading()
    {
        Debug.Log("just loaded scene!");
    }

    private void atFinish()
    {
        Debug.Log("now its completely unblacked!");
    }
}
