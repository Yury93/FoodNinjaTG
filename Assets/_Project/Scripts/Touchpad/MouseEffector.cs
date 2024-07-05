using Hanzzz.MeshSlicerFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEffector : MonoBehaviour
{
    public Camera mainCamera;
    public ParticleSystem particleSystem;
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
           if(particleSystem.gameObject.activeSelf == false) particleSystem.gameObject.SetActive(true); 
        }
        else
        {
            if (particleSystem.gameObject.activeSelf == true) particleSystem.gameObject.SetActive(false);
        }
        var position = SliceControl.GetMouseWorldPosition(mainCamera, particleSystem.transform);
        particleSystem.transform.position = position;
    }
}
