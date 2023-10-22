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
    [SerializeField] private GameObject _mozzaStick;
    [SerializeField] private List<GameObject> _elements;
    
    private ElementType _currentType;
    private bool _isEditor;
    private Transform _target;
    private float _timeToReachTarget;
    
    private Vector3 _direction;
    private float _distanceTarget;

    public void Init(ElementType element, bool isEditor)
    {
        // _direction.z *= speed;
        // _target = target;
        // _target.position = new Vector3(transform.position.x, transform.position.y, _target.position.z);
        // _timeToReachTarget = timeToReachTarget;

        var getIndex = 0;
        foreach (var ele in _elements)
        {
            ele.SetActive(false);
        }
        
        ElementType[] types = (ElementType[])Enum.GetValues(typeof(ElementType));
        for (int i = 0; i < types.Length; i++)
        {
            if (element == types[i])
            {
                getIndex = i;
                break;
            }
        }
        
        if (element == ElementType.Nothing)
            return;
        
        _elements[getIndex].SetActive(true);
        _elements[getIndex].GetComponent<ElementChild>().Init(element, isEditor);


        _isEditor = isEditor;
        
        _currentType = element;
        
        // _direction = _target.position - transform.position;
        // _distanceTarget = distanceTarget;
    }

    private void Update()
    {
        //Move();
    }

    private void Move()
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
    }
    
    public void OnDeathElementTrigger()
    {
        if (_frite != null && _point != null && _mozzaStick != null)
        {
            LoseLife?.Invoke();
        }
        Spawner.Instance.CheckIfVictory();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
    }
}
