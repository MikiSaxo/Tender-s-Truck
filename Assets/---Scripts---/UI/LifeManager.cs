using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;
    
    [SerializeField] private int _maxLife;
    [SerializeField] private GameObject _lifeBarPrefab;
    [SerializeField] private GameObject _lifeBarParent;

    private List<GameObject> _lifeBars = new List<GameObject>();

    private int _currentLife;

    [SerializeField] private float OffsetLifeBar = .07f;

    public int CurrentLife => _currentLife;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ElementToSpawn.LoseLife += LoseLife;
        
        SetupLife();
    }

    public void SetupLife()
    {
        _currentLife = _maxLife;
        
        ResetLife();
        
        for (int i = 0; i < _maxLife; i++)
        {
            GameObject go = Instantiate(_lifeBarPrefab, _lifeBarParent.transform);

            var position = go.transform.localPosition;
            go.transform.localPosition = new Vector3(i * OffsetLifeBar, position.y, position.z);
            _lifeBars.Add(go);
        }
    }

    private void ResetLife()
    {
        if (_lifeBars.Count <= 0)
            return;
        
        foreach (var bar in _lifeBars)
        {
            Destroy(bar);
        }
        _lifeBars.Clear();
    }

    public void WinLife()
    {
        _currentLife++;

        if (_currentLife > _maxLife)
            _currentLife = _maxLife;
        // print("c winwin la life");
        
        _lifeBars[_currentLife-1].SetActive(true);
    }

    private void LoseLife()
    {
        _currentLife--;
        
        if (_currentLife > 0)
        {
            _lifeBars[_currentLife].SetActive(false);
        }
        else
        {
            _lifeBars[0].SetActive(false);
            Spawner.Instance.StopMusic();
            // Debug.LogWarning("t mort mon gadjo");
        }
    }

    private void OnDisable()
    {
        ElementToSpawn.LoseLife -= LoseLife;
    }
}