using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameplaySauceType
{
    BacASauce = 0,
    ButtonsSauce = 1
}
public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;
    public WhichHanded WhichHanded => _whichHanded;

    [Header("Choose Hand")]
    [SerializeField] private WhichHanded _whichHanded;
    [SerializeField] private GameObject[] _leftHand;
    [SerializeField] private GameObject[] _rightHand;
    [Header("Materials")]
    [SerializeField] private Material[] _elementTypes;
    [Header("Choose Gameplay")]
    [SerializeField] private GameplaySauceType _sauceType;
    [SerializeField] private GameObject _bacASauce;
    [SerializeField] private GameObject _buttonsSauce;
    [SerializeField] private Transform[] _buttonsSaucePos;
    [Header("Map height")]
    [SerializeField] private Transform _mapTarget;
    [SerializeField] private float _height;
    [Header("Sabers")]
    [SerializeField] private float _velocityMinToCut;
    [Space(10f)]
    [Header("-- Spawn Element -- ")]
    [SerializeField] private Transform _spawnTarget;
    [SerializeField] private float _timeToReachTarget;
    [Header("Level Infos")]
    [SerializeField] private string _levelName;
    [SerializeField] private string _levelFolder;

    public Transform MapTarget => _mapTarget;
    public Transform SpawnTarget => _spawnTarget;
    public float Height => _height;
    public float VelocityMinToCut => _velocityMinToCut;
    public float TimeToReachTarget => _timeToReachTarget;
    public string LevelName => _levelName;
    public string LevelFolder => _levelFolder;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_whichHanded == WhichHanded.Left)
        {
            _leftHand[0].SetActive(true);
           _leftHand[1].SetActive(false);
           _rightHand[0].SetActive(false);
           _rightHand[1].SetActive(true);
        }
        else
        {
           _leftHand[0].SetActive(false);
           _leftHand[1].SetActive(true);
           _rightHand[0].SetActive(true);
           _rightHand[1].SetActive(false);
        }

        if (_sauceType == GameplaySauceType.BacASauce)
        {
            _bacASauce.SetActive(true);
            _buttonsSauce.SetActive(false);
        }
        else
        {
            _bacASauce.SetActive(false);
            _buttonsSauce.SetActive(true);
            _buttonsSauce.transform.position = _buttonsSaucePos[(int)_whichHanded].position;
        }
    }

    public Material GetElementTypeMat(int index)
    {
        return _elementTypes[index];
    }
}

public enum WhichHanded
{
    Right = 0,
    Left = 1
}
