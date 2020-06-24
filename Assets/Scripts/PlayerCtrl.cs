using JudgeTrigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public string scene;
    
    //music
    public AudioSource music;
    public AudioClip shootMusic;
    public AudioClip jumpMusic;
    public AudioClip upMusic;
    public AudioClip winMusic;
    public AudioClip killMusic;

    bool m_isGrounded;
    bool m_isWalled;

    public LayerMask m_groundLayer;
    public float m_groundCheckDistance = 0.5f;

    public Transform m_headCheck;
    public Transform m_footCheck;
    public float m_wallCheckDistance = 0.4f;

    Animator m_anim;
    Rigidbody2D m_body;

    bool m_FacingRight = true;

    public float m_Speed = 200f;
    public float m_jumpForce = 20f;

    public float m_CanJumpTime = 0.2f;
    private float m_JumpTimer;
    private bool m_isJumping;

    private Vector2 m_vec;
    private float m_input_h;

    // 二段跳
    private int m_jumpTimes;

    // 状态
    public Hashtable ctrlState;
    public bool is_player = false;
    public bool is_kill = false;
    public bool is_win = false;
    public bool is_push = false;

    public GameObject pfb_bullet;
    protected Vector2 bulletSpeed = new Vector2(15, 0);

    private GameObject[] obj;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((bool)ctrlState["kill"] && (collision.gameObject.layer == 9 || collision.gameObject.layer == 17) && (bool)collision.gameObject.GetComponent<PlayerCtrl>().ctrlState["you"])
        {
            AudioSource.PlayClipAtPoint(killMusic, gameObject.transform.localPosition);
            collision.gameObject.GetComponent<PlayerCtrl>().BeDamaged(10);
        }
        else if ((bool)ctrlState["win"] && (collision.gameObject.layer == 9 || collision.gameObject.layer == 17) && (bool)collision.gameObject.GetComponent<PlayerCtrl>().ctrlState["you"])
        {
            music.Play();
            collision.gameObject.GetComponent<PlayerCtrl>().BeWin();
        }
        if ((bool)ctrlState["you"] && collision.gameObject.layer == 13 || collision.gameObject.layer == 8)
        {
            if (collision.contacts[0].normal.y == -1)
            {
                m_isJumping = false;
                if (collision.gameObject.layer == 13 && collision.gameObject.GetComponent<ValidJudge>().upMovable)
                {
                    AudioSource.PlayClipAtPoint(upMusic, gameObject.transform.localPosition);
                    //music.clip = upMusic;
                    ////播放音效
                    //music.Play();
                    Rigidbody2D c_body = collision.gameObject.GetComponent<Rigidbody2D>();
                    Vector2 v = c_body.velocity;
                    var dir = 1;
                    Vector3 movement = new Vector3(0, dir * 1, 0);
                    if (collision.gameObject.GetComponent<ValidJudge>().leftWord != "none")
                    {
                        collision.gameObject.GetComponent<ValidJudge>().dealWithLeftExit = true;
                    }
                    if (collision.gameObject.GetComponent<ValidJudge>().rightWord != "none")
                    {
                        collision.gameObject.GetComponent<ValidJudge>().dealWithRightExit = true; 
                    }
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

        }
    }

    private void OnParticleCollision(GameObject other)
    {
        AudioSource.PlayClipAtPoint(killMusic, gameObject.transform.localPosition);
        BeDamaged(10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((bool)ctrlState["you"] && collision.gameObject.layer == 9 && (bool)collision.gameObject.GetComponent<PlayerCtrl>().ctrlState["kill"])
        {
            AudioSource.PlayClipAtPoint(killMusic, gameObject.transform.localPosition);
            BeDamaged(10);
        }
        else if ((bool)ctrlState["you"] && collision.gameObject.layer == 9 && (bool)collision.gameObject.GetComponent<PlayerCtrl>().ctrlState["win"])
        {
            BeWin();
        }
        if ((bool)ctrlState["you"] && collision.gameObject.layer == 9 && (bool)collision.gameObject.GetComponent<PlayerCtrl>().ctrlState["float"])
        {
            m_jumpTimes = 1;
        }
        if ((bool)ctrlState["you"] && collision.gameObject.layer == 16)
        {
            AudioSource.PlayClipAtPoint(killMusic, gameObject.transform.localPosition);
        }
    }

    void Awake()
    {
        if (gameObject.name == "player")
        {

            m_anim = GetComponent<Animator>();
        }
        m_body = GetComponent<Rigidbody2D>();
        //给对象添加一个AudioSource组件
        music = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;
        music.volume = 1.0f;
        //加载音效文件，我把跳跃的音频文件命名为jump
        shootMusic = Resources.Load<AudioClip>("music/shoot");
        jumpMusic = Resources.Load<AudioClip>("music/jump");
        upMusic = Resources.Load<AudioClip>("music/wordMove");
        winMusic = Resources.Load<AudioClip>("music/win");
        killMusic = Resources.Load<AudioClip>("music/kill2");
        music.clip = winMusic;

    }

    // Start is called before the first frame update
    void Start()
    {
        m_JumpTimer = 0f;
        m_isJumping = false;
        m_vec = new Vector2(0, m_jumpForce);
        m_jumpTimes = 0;
        ctrlState = new Hashtable();
        ctrlState.Add("you", is_player);
        ctrlState.Add("kill", is_kill);
        ctrlState.Add("win", is_win);
        ctrlState.Add("push", is_push);


    }


    private void Update()
    {
        if ((bool)ctrlState["you"] && (!GlobalVar.levelWin && !GlobalVar.changeScene && !GlobalVar.bossSpeaking))
        {

            m_isGrounded = IsGrounded();

            #region 跳跃代码
            // 跳跃
            if (gameObject.name == "player")
            {

                if (m_anim.GetBool("Ground") != m_isGrounded)
                {
                    m_anim.SetBool("Ground", m_isGrounded);
                }
            }

            if (m_jumpTimes == 2 && m_isGrounded)
            {
                m_jumpTimes = 0;
            }
            if (m_isJumping && Input.GetButton("跳跃"))
            {
                if (m_JumpTimer <= m_CanJumpTime)
                {
                    m_vec.x = m_body.velocity.x;
                    m_body.velocity = m_vec;
                    m_JumpTimer += Time.deltaTime;
                }
                else
                {
                    m_isJumping = false;
                }
            }

            if (Input.GetButtonDown("跳跃"))
            {
                if (m_isGrounded)
                {
                    AudioSource.PlayClipAtPoint(jumpMusic, gameObject.transform.localPosition, 0.3f);
                    m_jumpTimes = 1;

                    m_isJumping = true;
                    m_JumpTimer = 0f;
                    m_isGrounded = false;
                    m_vec.x = m_body.velocity.x;
                    m_body.velocity = m_vec;
                }
                else if (m_jumpTimes == 1 || m_jumpTimes == 0)
                {
                    AudioSource.PlayClipAtPoint(jumpMusic, gameObject.transform.localPosition, 0.3f);
                    m_jumpTimes = 2;

                    if (gameObject.name == "player")
                    {
                        m_anim.SetTrigger("DoubleJumping");
                    }

                    m_isJumping = true;
                    m_JumpTimer = 0f;
                    m_isGrounded = false;
                    m_vec.x = m_body.velocity.x;
                    m_body.velocity = m_vec;
                }

            }


            if (Input.GetButtonUp("跳跃"))
            {
                m_isJumping = false;
            }

            if(gameObject.name == "player")
            {

                m_anim.SetFloat("vSpeed", m_body.velocity.y);
            }
            #endregion

            #region 移动代码
            m_input_h = Input.GetAxisRaw("水平移动");
            Move(m_input_h);
            #endregion

            #region 射击代码
            if (Input.GetButtonDown("射击"))
            {
                GameObject obj = Instantiate(pfb_bullet, transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = m_FacingRight ? bulletSpeed : -1 * bulletSpeed;
                obj.GetComponent<Bullet>().dir2 = m_FacingRight ? 1 : -1;
                //把音源music的音效设置为jump
                AudioSource.PlayClipAtPoint(shootMusic, gameObject.transform.localPosition);
            }
            #endregion
        }
    }

    private void Move(float h)
    {
        m_isWalled = IsWalled(m_FacingRight ? 1 : -1);

        if (m_FacingRight)
        {
            if (h < 0)
            {
                Flip();
            }
            else if (m_isWalled)
            {
                if (gameObject.name == "player")
                {

                    m_anim.SetBool("run", false);
                }
                return;
            }
        }
        else
        {
            if (h > 0)
            {
                Flip();
            }
            else if (m_isWalled)
            {
                if (gameObject.name == "player")
                {
                    m_anim.SetBool("run", false);
                }
                return;
            }
        }

        Vector2 v = m_body.velocity;
        v.x = h * m_Speed * Time.deltaTime;
        m_body.velocity = v;

        if (gameObject.name == "player")
        {
            m_anim.SetBool("run", !(h == 0));
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, m_groundLayer);
        if (hit.collider != null)
        {

            return true;
        }
        return false;

    }

    private bool IsWalled(float dir)
    {
        RaycastHit2D hit1 = Physics2D.Raycast(m_headCheck.position, dir * Vector2.right, m_wallCheckDistance, m_groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(m_footCheck.position, dir * Vector2.right, m_wallCheckDistance, m_groundLayer);
        if ((hit1.collider == null) && (hit2.collider == null))
        {
            return false;
        }
        return true;
    }

    public int m_HP = 10;
    public GameObject ui_GameOverImage;

    public void BeDamaged(int damage)
    {
        m_HP -= damage;
        if (m_HP <= 0)
        {
            // 玩家死亡
            GlobalVar.playerCtrlNum--;
            Destroy(gameObject);
            if (GlobalVar.playerCtrlNum <= 0)
            {
                ui_GameOverImage.SetActive(true);
            }
            //ui_GameOverImage.SetActive(true);
        }
    }

    public void BeDamagedAndPlay(int damage)
    {
        m_HP -= damage;
        if (m_HP <= 0)
        {
            // 玩家死亡
            GlobalVar.playerCtrlNum--;
            Destroy(gameObject);
            if (GlobalVar.playerCtrlNum <= 0)
            {
                AudioSource.PlayClipAtPoint(killMusic, gameObject.transform.localPosition);
                ui_GameOverImage.SetActive(true);
            }
            //ui_GameOverImage.SetActive(true);
        }
    }
    public void BeWin()
    {
        //ui_GameOverImage.SetActive(true);
        GlobalVar.levelWin = true;
    }
    public void BeWinAndPlay()
    {
        music.Play();
        GlobalVar.levelWin = true;
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
