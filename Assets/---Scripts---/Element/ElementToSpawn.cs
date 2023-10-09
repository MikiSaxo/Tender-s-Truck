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
    public static event Action LoseLife;
    
    [SerializeField] private Vector3 _direction;
    [Header("Objects")]
    [SerializeField] private GameObject _frite;
    [SerializeField] private GameObject _point;

    private ElementType _currentType;
    private bool _isEditor;

    private void Start()
    {
    }

    public void Init(ElementType element, bool isEditor, float speed)
    {
        _direction.z *= speed;
        
        if (element == ElementType.Nothing)
            return;
        if (element == ElementType.Croquette)
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

    private void FixedUpdate()
    {
        if(!_isEditor)
            transform.Translate(_direction, Space.World);
    }

    public void OnDeathElementTrigger()
    {
        if (_frite != null && _point != null)
        {
            LoseLife?.Invoke();
        }
        Destroy(gameObject);
    }

    private void OnDisable()
    {
    }
}
