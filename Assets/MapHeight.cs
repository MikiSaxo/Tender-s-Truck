using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MapHeight : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _height;
    void Start()
    {
        float startPos = Vector3.Distance(transform.position, _target.position);

        transform.DOMoveY(startPos - _height, 0);
    }

   
}
