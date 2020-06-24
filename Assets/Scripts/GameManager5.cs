using JudgeTrigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameManager5 : MonoBehaviour
{

    public AudioSource music;
    public AudioClip roundWinMusic;
    public AudioClip roundLoseMusic;
    public AudioClip throwMusic;


    public GameObject m_player;
    public ParticleSystem round1PS;
    public ParticleSystem round2PS;
    public ParticleSystem round3PS;
    public int round;
    public bool rounding = false;
    float timer_round = 0.0f;

    public GameObject word_is;
    public GameObject word_pig;
    public GameObject word_hurt;
    public GameObject word_none;
    public GameObject word_saw;
    public GameObject word_win;
    public GameObject word_float;
    public GameObject word_you;
    public GameObject word_lele;
    public GameObject word_wawa;
    public GameObject[] wordsList;
    public GameObject[] appearWords;

    public GameObject speakingBubble;
    public GameObject loseBubble;
    public GameObject winBubble;
    public GameObject gameOver;
    public GameObject gameWin;

    public bool losePlay = false;


    public bool throwing;
    public int throwCount;

    public bool wording = false;
    public bool roundOver = false;

    private string[] wordMoveTrigger;

    // Start is called before the first frame update
    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;
        music.volume = 1.0f;
        //加载音效文件，我把跳跃的音频文件命名为jump
        roundWinMusic = Resources.Load<AudioClip>("music/roundWin");
        roundLoseMusic = Resources.Load<AudioClip>("music/roundLose");
        throwMusic = Resources.Load<AudioClip>("music/throw");
        music.clip = throwMusic;
    }
    void Start()
    {
        GlobalVar.bossLevel = true;
        GlobalVar.bossSpeaking = true;
        round = 0;
        wordsList = new GameObject[10];
        wordsList[0] = word_is;
        wordsList[1] = word_pig;
        wordsList[2] = word_hurt;
        wordsList[3] = word_none;
        wordsList[4] = word_saw;
        wordsList[5] = word_win;
        wordsList[6] = word_float;
        wordsList[7] = word_you;
        wordsList[8] = word_lele;
        wordsList[9] = word_wawa;
        wordMoveTrigger = new string[5];
        wordMoveTrigger[0] = "rightUp";
        wordMoveTrigger[1] = "rightDown";
        wordMoveTrigger[2] = "leftUp";
        wordMoveTrigger[3] = "leftDown";
        wordMoveTrigger[4] = "middle";


    }

    GameObject[] Shuffle(GameObject[] arr)
    {//随机获取
        GameObject[] newarr = new GameObject[arr.Length];
        int k = 0;
        while (k < arr.Length)
        {
            int temp = new System.Random().Next(0, arr.Length);
            if (arr[temp] != null)
            {
                newarr[k] = arr[temp];
                k++;
                arr[temp] = null;
            }
        }
        return newarr;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("重新开始"))
        {
            GlobalVar.playerCtrlNum = 0;
            SceneManager.LoadScene("level5");
            gameOver.SetActive(false);
        }
        if (!GlobalVar.bossIsYou)
        {
            if (GlobalVar.roundStart)
            {
                round++;
                if (round == 1)
                {
                    round1PS.gameObject.SetActive(true);
                }
                else if (round == 2)
                {
                    round2PS.gameObject.SetActive(true);
                }
                else if (round == 3)
                {
                    round3PS.gameObject.SetActive(true);
                }
                GlobalVar.roundStart = false;
                rounding = true;
                timer_round = 0.0f;
                GlobalVar.bossSpeaking = false;
            }
            if (rounding)
            {
                timer_round += Time.deltaTime;
                if (timer_round >= 20)
                {
                    if (round == 1)
                    {
                        round1PS.gameObject.SetActive(false);
                    }
                    else if (round == 2)
                    {
                        round2PS.gameObject.SetActive(false);
                    }
                    else if (round == 3)
                    {
                        round3PS.gameObject.SetActive(false);
                    }
                    rounding = false;
                    speakingBubble.SetActive(true);
                    appearWords = new GameObject[5];
                    GlobalVar.throwing = true;
                    Vector3 originPos1 = new Vector3(8.0f, -28.0f + 100, 0.0f);
                    Vector3 originPos2 = new Vector3(8.0f, -28.0f + 200, 0.0f);
                    Vector3 originPos3 = new Vector3(8.0f, -28.0f + 300, 0.0f);
                    Vector3 originPos4 = new Vector3(8.0f, -28.0f + 400, 0.0f);
                    Vector3 originPos5 = new Vector3(8.0f, -28.0f + 500, 0.0f);
                    appearWords[0] = Instantiate(word_pig, originPos1, Quaternion.identity);
                    appearWords[1] = Instantiate(word_is, originPos2, Quaternion.identity);
                    appearWords[2] = Instantiate(word_hurt, originPos3, Quaternion.identity);
                    appearWords[3] = Instantiate(wordsList[Random.Range(0, 10)], originPos4, Quaternion.identity);
                    appearWords[4] = Instantiate(wordsList[Random.Range(0, 10)], originPos5, Quaternion.identity);

                    appearWords = Shuffle(appearWords);
                    throwCount = 0;
                    timer_round = 0.0f;
                    throwing = true;


                }
            }
            if (throwing)
            {
                timer_round += Time.deltaTime;
                if (timer_round > 1.0f)
                {
                    if (throwCount < 5)
                    {
                        music.clip = throwMusic;
                        music.Play();
                        appearWords[throwCount].GetComponent<Animator>().SetTrigger(wordMoveTrigger[throwCount]);
                    }
                    timer_round = 0.0f;
                    throwCount++;
                }
                if (throwCount == 8)
                {
                    throwing = false;
                    GlobalVar.throwing = false;
                    for (int i = 0; i < appearWords.Length; i++)
                    {
                        appearWords[i].GetComponent<ValidJudge>().initWord();
                        Destroy(appearWords[i].GetComponent<Animator>());
                    }
                    wording = true;
                }
            }
            if (GlobalVar.roundWin)
            {
                music.clip = roundWinMusic;
                music.Play();
                speakingBubble.SetActive(false);
                winBubble.SetActive(true);
                wording = false;
                roundOver = true;
                GlobalVar.bossSpeaking = true;
                GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");
                obj[0].GetComponent<Animator>().SetBool("Ground", true);
                timer_round = 0.0f;
                GlobalVar.roundWin = false;

            }
            if (wording)
            {
                timer_round += Time.deltaTime;
                if (timer_round > 55.0f - round * 10.0)
                {
                    wording = false;
                    GlobalVar.roundLose = true;
                }
            }
            if (GlobalVar.roundLose)
            {
                if (!losePlay)
                {
                    music.clip = roundLoseMusic;
                    music.Play();
                    losePlay = true;
                }
                speakingBubble.SetActive(false);
                loseBubble.SetActive(true);
                GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < obj.Length; i++)
                {
                    obj[0].GetComponent<PlayerCtrl>().BeDamagedAndPlay(10);
                }
                GlobalVar.roundLose = false;
            }
            if (roundOver)
            {
                timer_round += Time.deltaTime;
                if (timer_round > 3.0f && round < 3)
                {
                    GlobalVar.roundStart = true;
                    roundOver = false;
                    winBubble.SetActive(false);
                    for(int i = 0; i < appearWords.Length; i++)
                    {
                        Destroy(appearWords[i]);
                    }
                }
                else if (timer_round > 3.0f &&round >= 3)
                {
                    GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");
                    obj[0].GetComponent<PlayerCtrl>().BeWinAndPlay();
                    roundOver = false;
                }
                if(round >= 3)
                {
                    winBubble.SetActive(false);
                    gameWin.SetActive(true);
                }
            }
        }
    }
}
