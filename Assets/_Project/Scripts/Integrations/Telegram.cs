using UnityEngine;
using System.Runtime.InteropServices;

namespace Scripts.Integration
{
    public class Telegram : MonoBehaviour
    {
        public bool enable;
        [DllImport("__Internal")]
        private static extern void Hello();
        [DllImport("__Internal")]
        private static extern void ShowAdvReward();

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
    }
}