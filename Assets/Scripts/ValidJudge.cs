using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JudgeTrigger
{
    public class ValidJudge : MonoBehaviour
    {
        public string leftWord = "none";
        public string rightWord = "none";
        public string upWord = "none";
        public string downWord = "none";
        public bool upMovable = true;
        public bool leftMovable = true;
        public bool rightMovable = true;
        public bool downMovable = true;
        public bool dealWithLeftExit = false;
        public bool dealWithRightExit = false;
        public bool dealWithUpExit = false;
        public bool dealWithDownExit = false;

        private GameObject[] obj;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

            //if (leftWord != "none" && rightWord != "none")
            //{
            //    obj = GameObject.FindGameObjectsWithTag(leftWord);
            //    for (int i = 0; i < obj.Length; i++)
            //    {
            //        obj[i].GetComponent<playableControl>().condition = rightWord;
            //    }
            //}
            //if(upWord != "none" && downWord != "none")
            //{
            //    obj = GameObject.FindGameObjectsWithTag(upWord);
            //    for (int i = 0; i < obj.Length; i++)
            //    {
            //        obj[i].GetComponent<playableControl>().condition = downWord;
            //    }
            //}
        }
    }
}
