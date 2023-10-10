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
    
    // [SerializeField] private Vector3 _direction;
    [Header("Objects")]
    [SerializeField] private GameObject _frite;
    [SerializeField] private GameObject _point;

    private ElementType _currentType;
    private bool _isEditor;
    private Transform _target;
    private float _timeToReachTarget;
    
    private Vector3 _direction;
    private float _distanceTarget;

    public void Init(ElementType element, bool isEditor, Transform target, float timeToReachTarget, float distanceTarget)
    {
        // _direction.z *= speed;
        _target = target;
        _timeToReachTarget = timeToReachTarget;
        
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
        
        _direction = _target.position - transform.position;
        _distanceTarget = distanceTarget;
    }

    private void Update()
    {
        if (!_isEditor)
        {
            if (_target != null)
            {
                float distanceTotale = _direction.magnitude;
                float vitesse = distanceTotale / _timeToReachTarget;

                if (distanceTotale < _distanceTarget)
                {
                    vitesse = _distanceTarget / _timeToReachTarget;
                }
                transform.position += _direction.normalized * vitesse * Time.deltaTime;
            }
        }
        // transform.Translate(_direction, Space.World);
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
