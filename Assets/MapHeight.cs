using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MapHeight : MonoBehaviour
{
    [SerializeField] private Transform _targetHeight;
    [SerializeField] private float _height;
    void Start()
    {
        float startPosY = _targetHeight.position.y - _height;

        transform.DOMove(new Vector3(_targetHeight.position.x, startPosY, _targetHeight.position.z), 0);
    }
}
