using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoverNoPhysics : MonoBehaviour
{
    public float clampYMax, clampYMin;
 
    public float initialMaxVelocity = 17f;
    public float initialMinVelocity = 9f;
    public float gravity = 9.8f;
    public  float finishTime;
    private float initialVelocity;
    private float startTime, start ;
    private Vector3 initialPosition;


    private void Start()
    {
        Throw();
    }
    public void Throw()
    {
        initialVelocity = UnityEngine.Random.Range(initialMinVelocity, initialMaxVelocity);
       startTime = Time.time;
        transform.position = new Vector3(transform.position.x, clampYMin, transform.position.z);
        initialPosition = transform.position;
    }
    void Update()
    {
        if (start < finishTime)
        {
            start += Time.deltaTime; 
        }
        float elapsedTime = Time.time - startTime;

        float y = initialPosition.y + initialVelocity * elapsedTime - 0.5f * gravity * elapsedTime * elapsedTime;

        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        if (transform.position.y <= clampYMin)
        {
            Throw();
        }
    }
}
