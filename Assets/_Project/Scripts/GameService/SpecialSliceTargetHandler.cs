using Hanzzz.MeshSlicerFree;
using System;
using System.Collections; 
using UnityEngine;
using UnityEngine.UI;

public class SpecialSliceTargetHandler : MonoBehaviour
{
    public Canvas canvasBomb;
    public Image whiteImage;
    public SliceControl sliceControl;
    public float speedAlpha;
 
    public Action onSlicePremiumTarget;
    private void Awake()
    {
        sliceControl.onBomb += ShowWhiteScreen;
        sliceControl.onSlice += OnSlice;
    }

    private void OnSlice(SliceTarget target)
    {
        if(target.SliceType == SliceTarget.SliceName.premium)
        { 
            onSlicePremiumTarget?.Invoke();
            ShowWhiteScreen(target);
        }
    }

    private void ShowWhiteScreen(SliceTarget sliceTarget)
    {
        StartCoroutine(CorSliceBomb());
       IEnumerator CorSliceBomb()
        {
            canvasBomb.gameObject.SetActive(true);
            whiteImage.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.7f);
            while (whiteImage.color.a > 0.01f)
            {

                whiteImage.color = Color.Lerp(whiteImage.color, new Color(1, 1, 1, 0), Time.deltaTime * speedAlpha);
                yield return null;
            }
            canvasBomb.gameObject.SetActive(false);
        }
    }
}
