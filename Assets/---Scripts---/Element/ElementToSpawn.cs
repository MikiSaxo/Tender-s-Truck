using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ElementToSpawn : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [Header("Objects")]
    [SerializeField] private GameObject _frite;
    [SerializeField] private GameObject _point;

    private ElementType _currentType;
    private bool _isEditor;

    
    public void Init(ElementType element, bool isEditor)
    {
        if (element == ElementType.Nothing)
            return;
        if (element == ElementType.Point)
        {
            _point.SetActive(true);
            _frite.SetActive(false);
            _point.GetComponent<ElementChild>().Init(element, isEditor);
        }
        else
        {
            _frite.SetActive(true);
            _point.SetActive(false);
            _frite.GetComponent<ElementChild>().Init(element, isEditor);
        }

        _isEditor = isEditor;
        
        _currentType = element;
    }

    private void Update()
    {
        if(!_isEditor)
            transform.Translate(_direction, Space.World);
    }

    public ElementType GetElementType()
    {
        return _currentType;
    }
}
