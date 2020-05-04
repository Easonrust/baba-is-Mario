using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Sprite m_saveSuccess;
    SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeShot(int k)
    {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);

        PlayerPrefs.SetFloat("CameraPosX", Camera.main.transform.position.x);
        PlayerPrefs.SetFloat("CameraPosY", Camera.main.transform.position.y);
        PlayerPrefs.SetFloat("CameraPosZ", Camera.main.transform.position.z);

        m_spriteRenderer.sprite = m_saveSuccess;
    }

}
