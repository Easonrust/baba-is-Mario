using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeScene: MonoBehaviour
{
    // Start is called before the first frame update
    public string scene;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void click()
    {
        BlackFader.GoToScene(scene, UnityEngine.SceneManagement.LoadSceneMode.Single, 2f, atLoading, atFinish);
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
