using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
   public AudioSource audioSource;

    public void PlayClip(AudioClip audioClip)
    {

        audioSource.clip = audioClip;
        audioSource.Play();
        Destroy(gameObject,audioClip.length);
    }
}
