using DG.Tweening;
using Hanzzz.MeshSlicerFree;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreService : MonoBehaviour
{
    public SliceControl sliceControl;
    public float hieghtFlyText;
    public Image moneyImage;
    public TextMeshProUGUI scoreText;
    public Score incrementTextPrefab;
    public Score decrementTextPrefab;
    public Camera cameraMain;
    public int score;
    private Vector3 textMoneyPosition;
    Tween tween;
    private void Awake()
    {
        sliceControl.onSlice += Increment;
        sliceControl.onBomb += Dencrement;
     textMoneyPosition =   scoreText.transform.position;
    }
    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
        var text = Instantiate(incrementTextPrefab, Vector3.zero, Quaternion.identity, this.transform);
        text.PlayAnimation(Vector3.up);
        Destroy(text?.gameObject, 3f);
    }
    public void Increment( SliceTarget sliceTarget)
    {
        if ( sliceTarget.SliceType == SliceTarget.SliceName.premium)
        {
            this.score += 20;
        }
        else
        {
            this.score += sliceTarget.scoreTarget; 
        }
        scoreText.text = this.score.ToString();

        var targetPos = sliceTarget.transform.position;
        var screenPoint = cameraMain.WorldToScreenPoint(targetPos);

        var text = Instantiate(incrementTextPrefab, screenPoint , Quaternion.identity,this.transform);
        if (sliceTarget.SliceType == SliceTarget.SliceName.premium)
        {
            text.flyText.text = ("+" + 20).ToString();
        }
        else
        {
            text.flyText.text = ("+" + sliceTarget.scoreTarget).ToString();
        }
        text.PlayAnimation(Vector3.up);
        tween?.Kill();
       
        tween = scoreText.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.5f).OnComplete(() => scoreText.transform.localScale = Vector3.one);
        Destroy(text?.gameObject, 3f);
    }
    public void Dencrement(SliceTarget sliceTarget)
    { 
        score-= 10;
        score = Mathf.Clamp(score, 0, 2000000);
      var targetPos =  sliceTarget.transform.position;
        var screenPoint = cameraMain.WorldToScreenPoint(targetPos);
        var text = Instantiate(decrementTextPrefab, screenPoint ,Quaternion.identity, this.transform); 
        text.flyText.text = ("-"+10).ToString();
      text.PlayAnimation(Vector3.down);
        scoreText.text = score.ToString();
        tween?.Kill();
        //   moneyImage.transform.localScale = Vector3.one;
        // scoreText.transform.DOPunchScale(0.5f, 1, 10).OnComplete(() => moneyImage.transform.localScale = Vector3.one);
        tween = scoreText.transform.DOPunchScale(new Vector3(0.15f,0.15f,0.15f),0.5f).OnComplete(() => scoreText.transform.localScale = Vector3.one);

        Destroy(text?.gameObject, 3f);
    }
    private void OnDestroy()
    {
        tween?.Kill();
    }
}
