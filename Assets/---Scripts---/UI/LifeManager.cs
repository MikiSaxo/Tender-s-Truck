using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int _maxLife;
    [SerializeField] private GameObject _lifeBarPrefab;
    [SerializeField] private GameObject _lifeBarParent;

    private List<GameObject> _lifeBars = new List<GameObject>();

    private int _currentLife;

    [SerializeField] private float OffsetLifeBar = .07f;

    private void Start()
    {
        _currentLife = _maxLife;
        
        for (int i = 0; i < _maxLife; i++)
        {
            GameObject go = Instantiate(_lifeBarPrefab, _lifeBarParent.transform);

            var position = go.transform.localPosition;
            go.transform.localPosition = new Vector3(i * OffsetLifeBar, position.y, position.z);
            _lifeBars.Add(go);
        }
    }

    private void LoseLife()
    {
        _currentLife--;
        _lifeBars[^1].SetActive(false);
        
        if (_currentLife <= 0)
        {
            // c'est ciao
        }
    }
}