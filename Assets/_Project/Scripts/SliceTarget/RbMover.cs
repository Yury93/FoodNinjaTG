using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbMover : MonoBehaviour
{
    public SliceTarget sliceTarget;
    public float maxY;
    public float minY;
    public Rigidbody rb;
    public Vector3 vector3;
    public float speed;
    public Vector3 rndRotation;
    private bool startCorPremiumSliceTarget;
    private void OnValidate()
    {
        if(sliceTarget == null)
        sliceTarget = GetComponent<SliceTarget>();  
    }
    private void Start()
    {
        var rndOperator = UnityEngine.Random.Range(0, 3);
         
        var x = UnityEngine.Random.Range(20, 150);
        var y = UnityEngine.Random.Range(20, 150);
        var z = UnityEngine.Random.Range(20, 150);

        if (rndOperator == 2)
        {
            x = -x; y = -y; z = -z;
        }
        if ( sliceTarget.SliceType == SliceTarget.SliceName.premium)
        {
              y = UnityEngine.Random.Range(-150, 150);
            x = 0;
            z = 0;
        }
        rndRotation = new Vector3(x, y, z);
    }

    private void Update()
    {
        
        transform.Rotate(rndRotation * Time.deltaTime);
    }
    private void FixedUpdate()
    { 
        if (transform.position.y < maxY &&   rb.useGravity == false  )
        { 
            rb.AddForce(vector3.normalized * speed * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        else
        {
            if ( sliceTarget.SliceType != SliceTarget.SliceName.premium)
                rb.useGravity = true;
            else if (startCorPremiumSliceTarget == false)
            {
                rb.AddForce(-rb.velocity, ForceMode.Impulse);
                startCorPremiumSliceTarget = true;
            }
        } 
    }
}
