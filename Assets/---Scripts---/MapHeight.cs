using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MapHeight : MonoBehaviour
{
    private Transform _targetHeight;
    private float _height;
    void Start()
    {
        _targetHeight = PartyManager.Instance.MapTarget;
        _height = PartyManager.Instance.Height;
        
        float startPosY = _targetHeight.position.y - _height;
        // print(startPosY);

        transform.DOMove(new Vector3(_targetHeight.position.x, startPosY, _targetHeight.position.z), 0);
    }
}
