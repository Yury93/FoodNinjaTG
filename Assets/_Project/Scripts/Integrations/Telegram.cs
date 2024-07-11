using UnityEngine;
using System.Runtime.InteropServices;

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
         if(   Jammer.PlayerPrefs.HasKey("click" ))
            {
                Debug.Log($"SAVED CKLICK = {Jammer.PlayerPrefs.GetString("click")}");
            }
        }
        private void Update()
        {
            if(Input.GetMouseButtonDown(0) && enable && test) {
                if (click == 5)
                {
                    Jammer.PlayerPrefs.SetString("click", click.ToString());
                    click = 0;
                }
                else click++;
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