using JudgeTrigger;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public int bullet_damage = 10;
    private GameObject[] obj;
    public float dir2; 
    protected Vector2 bulletSpeed = new Vector2(15, 0);
    public AudioClip moveMusic;
    public AudioClip finishMusic;
    void Awake()
    {
        moveMusic = Resources.Load<AudioClip>("music/wordMove"); 
        finishMusic = Resources.Load<AudioClip>("music/wordFinish");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 14)
        {
            AudioSource.PlayClipAtPoint(moveMusic, gameObject.transform.localPosition);
            AudioSource.PlayClipAtPoint(finishMusic, gameObject.transform.localPosition);
            Destroy(gameObject);
            Debug.Log("START");
            Vector3 newPosition = new Vector3(2.17f, collision.gameObject.transform.localPosition.y, collision.gameObject.transform.localPosition.z);
            collision.gameObject.transform.localPosition = newPosition;
            GlobalVar.changeScene = true;
            BlackFader.GoToScene("0-1", UnityEngine.SceneManagement.LoadSceneMode.Single, 2f, atLoading, atFinish);
        }
        if (collision.gameObject.layer == 15)
        {
            AudioSource.PlayClipAtPoint(moveMusic, gameObject.transform.localPosition);
            AudioSource.PlayClipAtPoint(finishMusic, gameObject.transform.localPosition);
            Destroy(gameObject);
            Vector3 newPosition = new Vector3(2.17f, collision.gameObject.transform.localPosition.y, collision.gameObject.transform.localPosition.z);
            collision.gameObject.transform.localPosition = newPosition;
            Debug.Log("QUIT");
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                                Application.Quit();
            #endif

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            AudioSource.PlayClipAtPoint(moveMusic, gameObject.transform.localPosition);
            Destroy(gameObject);
            Rigidbody2D c_body = collision.gameObject.GetComponent<Rigidbody2D>();
            //var dir = gameObject.GetComponent<Rigidbody2D>().velocity.x / Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x);
            Vector3 movement = new Vector3(dir2 * 1, 0, 0);
            if((dir2 < 0 && collision.gameObject.GetComponent<ValidJudge>().leftMovable)|| (dir2 > 0 && collision.gameObject.GetComponent<ValidJudge>().rightMovable))
            {
                if (collision.gameObject.tag == "is")
                {
                    if (collision.gameObject.GetComponent<ValidJudge>().upWord.IndexOf("word") != -1 && collision.gameObject.GetComponent<ValidJudge>().downWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(collision.gameObject.GetComponent<ValidJudge>().upWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[collision.gameObject.GetComponent<ValidJudge>().downWord.Split('_')[1]] = false;
                        }
                    }
                    if (collision.gameObject.GetComponent<ValidJudge>().leftWord.IndexOf("word") != -1 && collision.gameObject.GetComponent<ValidJudge>().rightWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(collision.gameObject.GetComponent<ValidJudge>().leftWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[collision.gameObject.GetComponent<ValidJudge>().rightWord.Split('_')[1]] = false;
                        }
                    }
                }
                if (collision.gameObject.GetComponent<ValidJudge>().upWord != "none")
                {
                    collision.gameObject.GetComponent<ValidJudge>().dealWithUpExit = true;
                }
                if (collision.gameObject.GetComponent<ValidJudge>().downWord != "none")
                {
                    collision.gameObject.GetComponent<ValidJudge>().dealWithDownExit = true;
                }
                collision.gameObject.GetComponent<ValidJudge>().upMovable = true;
                collision.gameObject.GetComponent<ValidJudge>().leftMovable = true;
                collision.gameObject.GetComponent<ValidJudge>().rightMovable = true;
                collision.gameObject.GetComponent<ValidJudge>().upWord = "none";
                collision.gameObject.GetComponent<ValidJudge>().leftWord = "none";
                collision.gameObject.GetComponent<ValidJudge>().downWord = "none";
                collision.gameObject.GetComponent<ValidJudge>().rightWord = "none";
                c_body.MovePosition(c_body.transform.position + movement);
            }

        }
        if(collision.gameObject.layer == 13|| collision.gameObject.layer == 8 || collision.gameObject.name == "boss")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 9 && (bool)collision.gameObject.GetComponent<PlayerCtrl>().ctrlState["push"])
        {
            AudioSource.PlayClipAtPoint(moveMusic, gameObject.transform.localPosition);
            Destroy(gameObject);
            if ((dir2 < 0 && collision.gameObject.GetComponent<ValidJudge>().leftMovable) || (dir2 > 0 && collision.gameObject.GetComponent<ValidJudge>().rightMovable))
            {
                if (!collision.gameObject.GetComponent<ValidJudge>().upMovable)
                {
                    collision.gameObject.GetComponent<ValidJudge>().dealWithUpExit = true;
                }
                if (!collision.gameObject.GetComponent<ValidJudge>().downMovable)
                {
                    collision.gameObject.GetComponent<ValidJudge>().dealWithDownExit = true;
                }
                collision.gameObject.GetComponent<ValidJudge>().upMovable = true;
                collision.gameObject.GetComponent<ValidJudge>().leftMovable = true;
                collision.gameObject.GetComponent<ValidJudge>().rightMovable = true;
                Rigidbody2D c_body = collision.gameObject.GetComponent<Rigidbody2D>();
                Vector3 movement = new Vector3(dir2 * 1, 0, 0);
                c_body.MovePosition(c_body.transform.position + movement);
            }

        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = dir2 * bulletSpeed;
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
