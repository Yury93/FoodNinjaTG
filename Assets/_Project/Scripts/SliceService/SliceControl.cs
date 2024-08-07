using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using System;
using System.Xml.Schema;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;
using DG.Tweening;

namespace Hanzzz.MeshSlicerFree
{

    public class SliceControl : MonoBehaviour
    {
        [SerializeField] private SliceTarget originalGameObject;
        [SerializeField] private Transform slicePlane;
        [SerializeField] private Material intersectionMaterial;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private KeyCode cutKey;
        [SerializeField] private GameObject[] slashPrefabs;
        [SerializeField] private Touchpad touchpad;
        [SerializeField] private float raycastDistance;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float moveDistance;
        [SerializeField] private BlowsEffect blowsEffectPrefab;
        [SerializeField] private GameObject premiumBlowPrefab;
 
        public SliceTarget.SliceName currentSliceName;
        public float blockSlice;
      
        private static Slicer slicer;

        public Action<SliceTarget> onSlice;
        public Action onSlashPremiumTarget;
        public Action<SliceTarget> onBomb;
       public int countSlash;
        private void Awake()
        {
            if (null == slicer)
            {
                slicer = new Slicer();
            }
        }
        Tween slashTween;
        private void Update()
        {
            RotatePlane();
            MoveSlicer();
            if (blockSlice > 0)
            {
                blockSlice -= Time.deltaTime;
            }

            RaycastHit hit = new RaycastHit();
            Vector3 worldPos = GetMouseWorldPosition(mainCamera, slicePlane);
            if (Input.GetMouseButton(0))
            {
               
                Ray ray = new Ray(mainCamera.transform.position, worldPos - mainCamera.transform.position);

                if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
                {
                    if (hit.collider.CompareTag("Plane") == false)
                        originalGameObject = hit.collider?.gameObject?.GetComponent<SliceTarget>();

                }
            }

            if (Input.GetMouseButton(0) && originalGameObject != null && hit.collider != null)
            {

                var target = originalGameObject;
                if (target == null)
                {
                    originalGameObject = null; return;
                }
                if (target.SliceType == SliceTarget.SliceName.bomb)
                {
                    onBomb?.Invoke(target);
                    Destroy(originalGameObject.gameObject); return;
                }
                if(target.SliceType == SliceTarget.SliceName.premium && countSlash < 25)
                {
                    CreateSlashEffect();
                    countSlash++;
                   slashTween?.Kill();
                   slashTween = target.transform.DOShakeScale(0.5f, 0.7f, 15);
                    onSlashPremiumTarget?.Invoke();
                    return;
                }
                else if(target.SliceType == SliceTarget.SliceName.premium)
                {
                    onSlice?.Invoke(target);
                    Destroy(originalGameObject.gameObject); return;
                }
                IncrementBlock(target);

                intersectionMaterial = target.Material;
                currentSliceName = target.SliceType;
                blockSlice = Mathf.Clamp(blockSlice, 0, 2.5f);
                Plane plane = new Plane(slicePlane.up, slicePlane.position);
                Slicer.SliceReturnValue sliceReturnValue;
                try
                {
                    int triangleCount = originalGameObject.meshFilter.sharedMesh.triangles.Length;
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    sliceReturnValue = slicer.Slice(originalGameObject.gameObject, plane, intersectionMaterial);
                }
                catch
                {
                    sliceReturnValue = null;
                }

                if (null == sliceReturnValue)
                {
                    return;
                }
                sliceReturnValue.topGameObject.transform.SetParent(originalGameObject.transform.parent, false);
                sliceReturnValue.bottomGameObject.transform.SetParent(originalGameObject.transform.parent, false);

                sliceReturnValue.topGameObject.transform.position += slicePlane.up.normalized * moveDistance;
                sliceReturnValue.bottomGameObject.transform.position += -slicePlane.up.normalized * moveDistance;


                sliceReturnValue.bottomGameObject.AddComponent<MeshCollider>().convex = true;
                sliceReturnValue.topGameObject.AddComponent<MeshCollider>().convex = true;
                sliceReturnValue.bottomGameObject.AddComponent<Rigidbody>();
                sliceReturnValue.topGameObject.AddComponent<Rigidbody>();

                CreateSlashEffect();

                if (blockSlice < 1)
                { 
                    if (target.SliceType == SliceTarget.SliceName.premium)
                    {
                        Destroy(sliceReturnValue.bottomGameObject.GetComponentInChildren<ParticleSystem>()?.gameObject);
                        Destroy(sliceReturnValue.topGameObject.GetComponentInChildren<ParticleSystem>()?.gameObject);
                    }
                }
               
                if (target.SliceType != SliceTarget.SliceName.premium)
                {
                    var blows = Instantiate(blowsEffectPrefab, new Vector3(worldPos.x, worldPos.y, 5), Quaternion.identity);
                    blows.SetColor(target.Material.color);


                }
                else
                {
                    var blows = Instantiate(premiumBlowPrefab, new Vector3(worldPos.x, worldPos.y, 5), Quaternion.identity);
                    Destroy(blows?.gameObject, 3f);
                }
                onSlice?.Invoke(target);
                Destroy(originalGameObject.gameObject);
                Destroy(sliceReturnValue.bottomGameObject, 3);
                Destroy(sliceReturnValue.topGameObject, 3.2f);
                originalGameObject = null;
            }
        }

        private void CreateSlashEffect()
        {
            var rndSlashEffect = UnityEngine.Random.Range(0, slashPrefabs.Length);
            var slashEffect = Instantiate(slashPrefabs[rndSlashEffect], slicePlane.transform.position, slicePlane.transform.rotation);
            Destroy(slashEffect.gameObject, 2f);
        }

        private void IncrementBlock(SliceTarget target)
        {
            if (currentSliceName == target.SliceType)
            {
                blockSlice += 1;
                currentSliceName = target.SliceType;
            }
            else
            {
                blockSlice -= 1;
            }
        }

        private void RotatePlane()
        {
            if (Input.GetMouseButton(0))
            { 
                slicePlane .transform.right =  new Vector3(touchpad.Horizontal, touchpad.Vertical, 0) ;
            }
        }

        private void MoveSlicer()
        {
            Vector3 worldPosition = GetMouseWorldPosition(mainCamera,slicePlane); 
            slicePlane.position = new Vector3(worldPosition.x, worldPosition.y, slicePlane.position.z);
        }

        public static Vector3 GetMouseWorldPosition(Camera mainCamera, Transform target)
        {
            float plainPositionZ = mainCamera.WorldToScreenPoint(target.position).z;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, plainPositionZ));
            return worldPosition;
        }
        private void OnDestroy()
        {
            slashTween?.Kill();
        }
    }
    

}
