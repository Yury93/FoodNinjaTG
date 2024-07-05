using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowsEffect : MonoBehaviour
{
    public ParticleSystem particleSystem1, particleSystem3;
  public void SetColor(Color color)
    {
        particleSystem1.startColor = color; 
        particleSystem3.startColor = color;
    }
}
