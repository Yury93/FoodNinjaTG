using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.UI;
using System;

namespace Scripts.Integration
{
    public class Telegram : MonoBehaviour
    {
        public SpecialSliceTargetHandler targetHandler;
        public Button showAdvButton,closeAdvButton;
        public Image fadeImage;
        public ScoreService scoreService;
        public Canvas bombCanvas;
        public static bool AdvButtonIsActive;

        public bool enable,test;
        [DllImport("__Internal")]
        private static extern void Hello();
        [DllImport("__Internal")]
        private static extern void ShowAdvReward();

        public float checkAdvTimer,startCheckAdvTimer;

        private void Start()
        {
            showAdvButton.gameObject.SetActive(false);
            fadeImage.gameObject.SetActive(false);
            closeAdvButton.onClick.AddListener(CloseAdvButton);
            AdvButtonIsActive = false;
            if (enable)
            {
                showAdvButton.onClick.AddListener(CallAdv);
                startCheckAdvTimer = checkAdvTimer;
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

        private void CloseAdvButton()
        {
            showAdvButton.gameObject.SetActive(false);
            AdvButtonIsActive = false;
            TimeHandler.OnSaveTimeAdv();
            fadeImage.gameObject.SetActive(false);
        }

        private void Update()
        {
            if(checkAdvTimer <= 0)
            {
                if (showAdvButton.gameObject.activeSelf == false )
                {
                    Debug.Log("advertising request");
                    if (TimeHandler.HasAdv(3))
                    {
                        showAdvButton.gameObject.SetActive(true);
                        fadeImage.gameObject.SetActive(true);
                        AdvButtonIsActive = true;
                        checkAdvTimer = startCheckAdvTimer;
                    }
                }
            }
            else
            {
                checkAdvTimer -= Time.deltaTime;
            }
            if(Input.GetKeyDown(KeyCode.Q)) {
            TimeHandler.DeleteSavedTimes();
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
            showAdvButton.gameObject.SetActive(false);
            AdvButtonIsActive = false;
            TimeHandler.OnSaveTimeAdv();
            targetHandler.ShowWhiteScreen(null);
            scoreService.AddScore(100);
            fadeImage.gameObject.SetActive(false);
            Debug.Log("SHOW ADV CALLBACK MY SCRIPT");
        }
        public void ErrorCallback()
        {
            showAdvButton.gameObject.SetActive(false);
            AdvButtonIsActive = false;
            fadeImage.gameObject.SetActive(false);
            Debug.Log("ERROR CALLBACK MY SCRIPT");
            TimeHandler.OnSaveTimeAdv();
        }
    }
}