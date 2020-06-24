
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVar.levelWin)
        {
            BlackFader.GoToScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single, 2f, atLoading, atFinish);
            GlobalVar.roundStart = false;
            Debug.Log(sceneName);
            GlobalVar.playerCtrlNum = 0;
            GlobalVar.levelWin = false;
            if (sceneName != "GameStart")
            {
                GlobalVar.changeScene = true;
            }
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
