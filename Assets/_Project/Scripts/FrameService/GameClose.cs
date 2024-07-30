using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameClose : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CloseFrameGame();
    [DllImport("__Internal")]
    private static extern void AddScore(int score);
    public Button CloseButton;
    private void Start()
    {
        CloseButton.onClick.AddListener(CloseCurrentFrame);
    }
    public void CloseCurrentFrame()
    {
        CloseFrameGame();
    }
    public void CallAddScore(int score)
    {
        try
        {
            AddScore(score);
        }
        catch { Debug.Log("ошибка прибавления очков"); }
    }
}
