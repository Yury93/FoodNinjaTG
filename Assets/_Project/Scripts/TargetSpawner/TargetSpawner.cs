using Hanzzz.MeshSlicerFree;
using Scripts.Integration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
 
    public SliceControl sliceControl;
    public SpecialSliceTargetHandler targetHandler;
    public float maxSpeed = 18f;
    public float minSpeed = 10f;
  public float spawnTimerMin,spawnTimerMax;
  private float spawnTimer;
    public List<TransformSpawn> spawnDirection;
    public List<SliceTarget> sliceTargetPrefabs;
    public bool AccessSpawn;
    private float rndChapterDelay;
    float rndTargetCreateDelay;
    private void Start()
    {
        AccessSpawn = true;
        targetHandler.onSlicePremiumTarget += () => AccessSpawn = true;
       
    }
    private void Update()
    {
        if (spawnTimer > 0) spawnTimer -= Time.deltaTime;
        else if(AccessSpawn)  spawnTimer = UnityEngine.Random.Range( spawnTimerMin, spawnTimerMax);
        
        if (spawnTimer <= 0)
        {
            if (AccessSpawn)
            {
                int rndPosition = UnityEngine.Random.Range(0, spawnDirection.Count);
                var target = CreateTarget(spawnDirection[rndPosition]);
                if (target.SliceType == SliceTarget.SliceName.premium)
                {
                    AccessSpawn = false;
                }
            }
        }
         
    }

    public SliceTarget CreateTarget(TransformSpawn spawn)
    {
      
       var prefabIndex = UnityEngine.Random.Range(0,sliceTargetPrefabs.Count);
       var target = Instantiate(sliceTargetPrefabs[prefabIndex],spawn.spawnPos,Quaternion.identity,this.transform);
        
        if (target.SliceType == SliceTarget.SliceName.premium)
        {
            sliceControl.countSlash = 0;
            target.transform.position = new Vector3(0, target.transform.position.y, target.transform.position.z);
            target.particleSystem.gameObject.SetActive(true);
            target.particleSystem.Play();
        }
        if (spawn.isRandom == false) return target;
        if (target.SliceType != SliceTarget.SliceName.premium)
        {
            float newY = Mathf.Abs(spawn.spawnPos.y);
          Vector3 dir = new Vector3(0, newY, 0);
        
            if (spawn.isRandom == false)
            {
                dir = new Vector3(-spawn.x, newY, 0);
            }
            else
            {
                dir = new Vector3(spawn.x, newY, 0);
            }
       
            var speed = UnityEngine.Random.Range(minSpeed,maxSpeed);
        
            target.Mover.vector3 = dir;
         
                target.Mover.speed = speed;
            Destroy(target?.gameObject,3.5f);
        }
        return target;
    }
    [Serializable]
    public class TransformSpawn
    {
        public Transform spawnTransform;
        public bool isRandom;
        public float minRandom, maxRandom;
        public float x => UnityEngine.Random.Range(minRandom, maxRandom);
        public Vector3 spawnPos => spawnTransform.position;
    }
}
