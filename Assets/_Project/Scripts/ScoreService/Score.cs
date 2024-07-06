using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float hieghtFlyText ;
    public TextMeshProUGUI flyText;
    public List<Tween> tweens = new List<Tween>();  
    public void PlayAnimation(Vector3 vector3)
    {
        var seq = DOTween.Sequence();
        if(vector3 == Vector3.up)
        {
            tweens.Add(transform.DOMoveY(transform.position.y + (Vector3.up.y * hieghtFlyText), 1));
            tweens.Add(flyText.DOFade(0, 3f));
              
        }
        else
        {
            tweens.Add(transform.DOMoveY(transform.position.y - (Vector3.up.y * hieghtFlyText), 1));
            tweens.Add(flyText.DOFade(0, 3f));
        }
        Destroy(gameObject, 3f);
    }
    private void OnDestroy()
    {
        for (int i = 0; i < tweens.Count; i++)
        {
            tweens[i]?.Kill();
        }
    }
}
