using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

namespace Scripts.Integration
{
    public class Telegram : MonoBehaviour
    { 
        public bool enable,test;
        [DllImport("__Internal")]
        private static extern void Hello();
        [DllImport("__Internal")]
        private static extern void ShowAdvReward();
       
         
        private int click;

        private void Start()
        {
            if (enable)
            {
                if (Application.isEditor == false)
                {
                    Hello();
                }
                else
                {
                    Debug.Log("editor_hello");
                }
            }
         
        }
      
        public void CallAdv()
        {
            if (enable)
            {
                if (Application.isEditor == false)
                {
                    ShowAdvReward();
                }
                else
                {
                    Debug.Log("call_ads");
                }
            }
        }
        public void ShowAdvCallback()
        {
            Debug.Log("SHOW ADV CALLBACK MY SCRIPT");
        }
        public void ErrorCallback()
        {
            Debug.Log("ERROR CALLBACK MY SCRIPT");
        }
    }
}