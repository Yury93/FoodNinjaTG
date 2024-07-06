using Hanzzz.MeshSlicerFree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    public List<AudioClip> foodSlashes;
    public Sound soundPrefab;
    public SliceControl sliceControl;
    public AudioClip slashPremium,slicePremium, slashBomb;
    public List<AudioClip> slashes;

    public Touchpad touchpad;
    public AudioSource audioSource; 
    public float slashScheduled = 0.03f;
    int slashQueu;
    public Coroutine corSlashes;
    public bool isPlaySlash;

    private void Awake()
    {
        sliceControl.onBomb += CreateBombVFX;
        sliceControl.onSlice += CreateFoodsVFX;
        sliceControl.onSlice += CreatePremiumVFX;
        sliceControl.onSlashPremiumTarget += OnSlashPremiumTarget;
    } 
    private void OnSlashPremiumTarget()
    {
        var sound = Instantiate(soundPrefab, this.transform);
        sound.PlayClip(slashPremium);
    }

    public void CreateFoodsVFX(SliceTarget sliceTarget)
    {
        if(sliceTarget.SliceType == SliceTarget.SliceName.premium) { return; }
        var sound = Instantiate(soundPrefab, this.transform);
        var rnd = UnityEngine. Random.Range(0, foodSlashes.Count);
        sound.PlayClip(foodSlashes[rnd]);

    }
    public void CreatePremiumVFX(SliceTarget sliceTarget)
    {
        if (sliceTarget.SliceType != SliceTarget.SliceName.premium) { return; }
        var sound = Instantiate(soundPrefab, this.transform);
        sound.PlayClip(slicePremium);

    }
    public void CreateBombVFX(SliceTarget sliceTarget)
    {
        var sound = Instantiate(soundPrefab, this.transform);
        sound.PlayClip(slashBomb);
    }



    private void Update()
    {
        if (touchpad.Horizontal != 0f || touchpad.Vertical != 0f)
            PlaySlash();
        else if(corSlashes != null)
            StopCoroutine(corSlashes);
    }
    public void PlaySlash()
    {
        if(isPlaySlash == false && audioSource.isPlaying == false)
            corSlashes = StartCoroutine(CorPlaySlashes());   
    }
    private IEnumerator CorPlaySlashes()
    {
        yield return new WaitUntil(() => isPlaySlash == false && audioSource.isPlaying == false);
        if ((touchpad.Horizontal != 0f || touchpad.Vertical != 0f )&& isPlaySlash == false && Input.GetMouseButton(0))
        {
            isPlaySlash = true;
            if(slashQueu == 0) {  slashQueu = 1; }
            else slashQueu = 0;
            audioSource.clip = slashes[slashQueu];
            audioSource.timeSamples = (int)(audioSource.clip.length * slashScheduled * audioSource.clip.frequency);
            audioSource.Play();
        }
        if(audioSource.clip != null)
        {
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaySlash = false;
        }
    }
}
