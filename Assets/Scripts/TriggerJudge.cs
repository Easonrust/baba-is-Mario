using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JudgeTrigger
{
    public class TriggerJudge : MonoBehaviour
    {
        public string Direct; //当前触发器是哪个方向的
        public ValidJudge validJudge;
        private GameObject[] obj;
        private string oldLeft = "none";
        private string oldRight = "none";
        private string oldUp = "none";
        private string oldDown = "none";

        // Start is called before the first frame update
        void Start()
        {
            Direct = gameObject.name;
            validJudge = gameObject.GetComponentInParent<ValidJudge>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var Tag = collision.gameObject.tag;
            if(collision.gameObject.layer == 13)
            {
                if (Direct == "up")
                {
                    validJudge.upWord = collision.gameObject.tag;
                    if (validJudge.upWord.IndexOf("word") != -1 && validJudge.downWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(validJudge.upWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            if(validJudge.downWord == "state_you")
                            {
                                GlobalVar.playerCtrlNum++;
                            }
                            if (validJudge.downWord == "state_push")
                            {
                                obj[i].GetComponent<CapsuleCollider2D>().isTrigger = false;
                            }
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[validJudge.downWord.Split('_')[1]] = true;
                        }
                    }
                }
                else if (Direct == "left")
                {
                    validJudge.leftWord = collision.gameObject.tag;

                    if (validJudge.leftWord.IndexOf("word") != -1 && validJudge.rightWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(validJudge.leftWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            if (validJudge.rightWord == "state_you")
                            {
                                Debug.Log("left you++");
                                GlobalVar.playerCtrlNum++;
                            }
                            if (validJudge.rightWord == "state_push")
                            {
                                obj[i].GetComponent<CapsuleCollider2D>().isTrigger = false;
                            }
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[validJudge.rightWord.Split('_')[1]] = true;
                        }
                    }
                }
                else if (Direct == "right")
                {
                    validJudge.rightWord = collision.gameObject.tag;

                    if (validJudge.leftWord.IndexOf("word") != -1 && validJudge.rightWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(validJudge.leftWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            if (validJudge.rightWord == "state_you")
                            {
                                Debug.Log("right you++");
                                GlobalVar.playerCtrlNum++;
                            }
                            if (validJudge.rightWord == "state_push")
                            {
                                obj[i].GetComponent<CapsuleCollider2D>().isTrigger = false;
                            }
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[validJudge.rightWord.Split('_')[1]] = true;
                        }
                    }
                }
                else
                {
                    validJudge.downWord = collision.gameObject.tag;
                    if (validJudge.upWord.IndexOf("word") != -1 && validJudge.downWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(validJudge.upWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            if (validJudge.downWord == "state_you")
                            {
                                Debug.Log("down you++");
                                GlobalVar.playerCtrlNum++;
                            }
                            if (validJudge.downWord == "state_push")
                            {
                                obj[i].GetComponent<CapsuleCollider2D>().isTrigger = false;
                            }
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[validJudge.downWord.Split('_')[1]] = true;
                        }
                    }
                }
            }
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 13 || (collision.gameObject.layer == 9 && (bool)collision.gameObject.GetComponent<PlayerCtrl>().ctrlState["push"]))
            {
                switch (Direct)
                {
                    case "up":
                        validJudge.upMovable = false;
                        break;
                    case "left":
                        validJudge.leftMovable = false;
                        break;
                    case "right":
                        validJudge.rightMovable = false;
                        break;
                    case "bottom":
                        validJudge.downMovable = false;
                        break;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 13)
            {
                if (validJudge.dealWithLeftExit && Direct == "left")
                {
                    if (collision.GetComponent<ValidJudge>().leftWord.IndexOf("word") != -1 && collision.GetComponent<ValidJudge>().rightWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(collision.GetComponent<ValidJudge>().leftWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            if (collision.GetComponent<ValidJudge>().rightWord == "state_you")
                            {
                                Debug.Log("you--"); 
                                GlobalVar.playerCtrlNum--;
                            }
                            if (collision.GetComponent<ValidJudge>().rightWord == "state_push")
                            {
                                Debug.Log("left push");
                                obj[i].GetComponent<CapsuleCollider2D>().isTrigger = true;
                            }
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[collision.GetComponent<ValidJudge>().rightWord.Split('_')[1]] = false;
                        }
                    }
                    collision.GetComponent<ValidJudge>().rightWord = "none";
                    collision.GetComponent<ValidJudge>().rightMovable = true;
                    validJudge.dealWithLeftExit = false;
                }
                else if (validJudge.dealWithRightExit && Direct == "right")
                {
                    if (collision.GetComponent<ValidJudge>().leftWord.IndexOf("word") != -1 && collision.GetComponent<ValidJudge>().rightWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(collision.GetComponent<ValidJudge>().leftWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            if (collision.GetComponent<ValidJudge>().rightWord == "state_you")
                            {
                                Debug.Log("you--");
                                GlobalVar.playerCtrlNum--;
                            }
                            if (collision.GetComponent<ValidJudge>().rightWord == "state_push")
                            {
                                Debug.Log("right push");
                                obj[i].GetComponent<CapsuleCollider2D>().isTrigger = true;
                            }
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[collision.GetComponent<ValidJudge>().rightWord.Split('_')[1]] = false;
                        }
                    }
                    collision.GetComponent<ValidJudge>().leftWord = "none";
                    collision.GetComponent<ValidJudge>().leftMovable = true;
                    validJudge.dealWithRightExit = false;
                }
                else if (validJudge.dealWithUpExit && Direct == "up")
                {
                    if (collision.GetComponent<ValidJudge>().upWord.IndexOf("word") != -1 && collision.GetComponent<ValidJudge>().downWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(collision.GetComponent<ValidJudge>().upWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            if (collision.GetComponent<ValidJudge>().downWord == "state_you")
                            {
                                Debug.Log("you--");
                                GlobalVar.playerCtrlNum--;
                            }
                            if (collision.GetComponent<ValidJudge>().downWord == "state_push")
                            {
                                Debug.Log("up push");
                                obj[i].GetComponent<CapsuleCollider2D>().isTrigger = true;
                            }
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[collision.GetComponent<ValidJudge>().downWord.Split('_')[1]] = false;
                        }
                    }
                    collision.GetComponent<ValidJudge>().downWord = "none";
                    validJudge.dealWithUpExit = false;
                }
                else if (validJudge.dealWithDownExit && Direct == "bottom")
                {
                    if (collision.GetComponent<ValidJudge>().upWord.IndexOf("word") != -1 && collision.GetComponent<ValidJudge>().downWord.IndexOf("state") != -1)
                    {
                        obj = GameObject.FindGameObjectsWithTag(collision.GetComponent<ValidJudge>().upWord.Split('_')[1]);
                        for (int i = 0; i < obj.Length; i++)
                        {
                            if (collision.GetComponent<ValidJudge>().downWord == "state_you")
                            {
                                Debug.Log("you--");
                                GlobalVar.playerCtrlNum--;
                            }
                            if (collision.GetComponent<ValidJudge>().downWord == "state_push")
                            {
                                Debug.Log("down push");
                                obj[i].GetComponent<CapsuleCollider2D>().isTrigger = true;
                            }
                            obj[i].GetComponent<PlayerCtrl>().ctrlState[collision.GetComponent<ValidJudge>().downWord.Split('_')[1]] = false;
                        }
                    }
                    collision.GetComponent<ValidJudge>().upWord = "none";
                    collision.GetComponent<ValidJudge>().upMovable = true;
                    validJudge.dealWithDownExit = false;
                }

            }
            else if (collision.gameObject.layer == 9)
            {
                if (validJudge.dealWithLeftExit && Direct == "left")
                {
                    collision.GetComponent<ValidJudge>().rightMovable = true;
                    validJudge.dealWithLeftExit = false;
                }
                else if (validJudge.dealWithRightExit && Direct == "right")
                {
                    collision.GetComponent<ValidJudge>().leftMovable = true;
                    validJudge.dealWithRightExit = false;
                }
                else if (validJudge.dealWithUpExit && Direct == "up")
                {
                    collision.GetComponent<ValidJudge>().downMovable = true;
                    validJudge.dealWithUpExit = false;
                }
                else if (validJudge.dealWithDownExit && Direct == "bottom")
                {
                    collision.GetComponent<ValidJudge>().upMovable = true;
                    validJudge.dealWithDownExit = false;
                }

            }
        }

        //private void OnTriggerExit2D (Collider2D other)
        //{
        //    //if (other.gameObject.layer == 13)
        //    //{
        //    //    switch (Direct)
        //    //    {
        //    //        case "up":
        //    //            if (validJudge.upWord.IndexOf("word") != -1 && validJudge.downWord.IndexOf("state") != -1)
        //    //            {
        //    //                Debug.Log("out " + validJudge.upWord.Split('_')[1] + " " + validJudge.downWord.Split('_')[1]);
        //    //                obj = GameObject.FindGameObjectsWithTag(validJudge.upWord.Split('_')[1]);
        //    //                for (int i = 0; i < obj.Length; i++)
        //    //                {
        //    //                    obj[i].GetComponent<PlayerCtrl>().ctrlState[validJudge.downWord.Split('_')[1]] = false;
        //    //                }
        //    //            }
        //    //            validJudge.upWord = "none";
        //    //            break;
        //    //        case "left":
        //    //            if (validJudge.leftWord.IndexOf("word") != -1 && validJudge.rightWord.IndexOf("state") != -1)
        //    //            {
        //    //                Debug.Log("out " + validJudge.leftWord.Split('_')[1] + " " + validJudge.rightWord.Split('_')[1]);
        //    //                obj = GameObject.FindGameObjectsWithTag(validJudge.leftWord.Split('_')[1]);
        //    //                for (int i = 0; i < obj.Length; i++)
        //    //                {
        //    //                    obj[i].GetComponent<PlayerCtrl>().ctrlState[validJudge.rightWord.Split('_')[1]] = false;
        //    //                }
        //    //            }
        //    //            validJudge.leftWord = "none";
        //    //            break;
        //    //        case "down":
        //    //            if (validJudge.upWord.IndexOf("word") != -1 && validJudge.downWord.IndexOf("state") != -1)
        //    //            {
        //    //                Debug.Log("out " + validJudge.upWord.Split('_')[1] + " " + validJudge.downWord.Split('_')[1]);
        //    //                obj = GameObject.FindGameObjectsWithTag(validJudge.upWord.Split('_')[1]);
        //    //                for (int i = 0; i < obj.Length; i++)
        //    //                {
        //    //                    obj[i].GetComponent<PlayerCtrl>().ctrlState[validJudge.downWord.Split('_')[1]] = false;
        //    //                }
        //    //            }
        //    //            validJudge.downWord = "none";
        //    //            break;
        //    //        case "right":
        //    //            if (validJudge.leftWord.IndexOf("word") != -1 && validJudge.rightWord.IndexOf("state") != -1)
        //    //            {
        //    //                Debug.Log("out " + validJudge.leftWord.Split('_')[1] + " " + validJudge.rightWord.Split('_')[1]);
        //    //                obj = GameObject.FindGameObjectsWithTag(validJudge.leftWord.Split('_')[1]);
        //    //                for (int i = 0; i < obj.Length; i++)
        //    //                {
        //    //                    obj[i].GetComponent<PlayerCtrl>().ctrlState[validJudge.rightWord.Split('_')[1]] = false;
        //    //                }
        //    //            }
        //    //            validJudge.rightWord = "none";
        //    //            break;
        //    //    }
        //    //}
            
        //    //if (other.gameObject.layer == 8 || other.gameObject.layer == 13)
        //    //{
        //    //    switch (Direct)
        //    //    {
        //    //        case "up":
        //    //            validJudge.upMovable = true;
        //    //            break;
        //    //        case "left":
        //    //            validJudge.leftMovable = true;
        //    //            break;
        //    //        case "right":
        //    //            validJudge.rightMovable = true;
        //    //            break;
        //    //    }
        //    //}
        //}


    }

}
