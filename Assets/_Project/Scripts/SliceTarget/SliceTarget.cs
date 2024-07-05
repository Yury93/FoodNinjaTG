using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class SliceTarget : MonoBehaviour
{
    public enum SliceName
    {
        none,
        apple,
        avacado,
        banana,
        bomb,
        bread,
        brocolli,
        cake,
        carrot,
        coffee,
        donut,
        fish,
        luk,
        mashroom,
        soda,
        tiqua,
        tomat,
        premium
    }
    [field:SerializeField] public SliceName SliceType { get;set; }
    [field: SerializeField] public RbMover Mover { get; set; }
    [field: SerializeField] public Material Material { get; set; }
    [field: SerializeField] public ParticleSystem particleSystem { get; set; }
}
