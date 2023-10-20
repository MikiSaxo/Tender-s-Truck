using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [Header("Level Infos")]
    [SerializeField] private string _levelName;
    [SerializeField] private string _levelFolder;
    [Header("Sabers")]
    [SerializeField] private float _velocityMinToCut;
    [Header("Map height")]
    [SerializeField] private Transform _mapTarget;
    [SerializeField] private float _height;
    [Space(10f)]
    [Header("-- Spawn Element -- ")]
    [SerializeField] private Transform _spawnTarget;
    [SerializeField] private float _timeToReachTarget;
    [Header("Score")]
    [SerializeField] private int _friteScore;
    [FormerlySerializedAs("_pointScore")] [SerializeField] private int croquetteScore;
    [SerializeField] private int _mozzaScore;
    [Header("Choose Hand")]
    [SerializeField] private WhichHanded _whichHanded;
    [SerializeField] private GameObject[] _sabers;
    [SerializeField] private GameObject[] _saberParents;
    [SerializeField] private GameObject[] _fritureParents;
    [Header("Materials")]
    [SerializeField] private Material[] _elementTypes;
    [Header("Choose Gameplay")]
    [SerializeField] private GameplaySauceType _sauceType;
    [SerializeField] private GameObject _bacASauce;
    [SerializeField] private GameObject _buttonsSauce;
 

    

    public Transform MapTarget => _mapTarget;
    public Transform SpawnTarget => _spawnTarget;
    public float Height => _height;
    public float VelocityMinToCut => _velocityMinToCut;
    public float TimeToReachTarget => _timeToReachTarget;
    public string LevelName => _levelName;
    public string LevelFolder => _levelFolder;
    public int FriteScore => _friteScore;
    public int CroquetteScore => croquetteScore;
    public int MozzaScore => _mozzaScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       SwitchHand((int)_whichHanded);

        UpdateBacASauce();
    }

    private void UpdateBacASauce()
    {
        if (_sauceType == GameplaySauceType.BacASauce)
        {
            _bacASauce.SetActive(true);
            _buttonsSauce.SetActive(false);
        }
        else
        {
            _bacASauce.SetActive(false);
            _buttonsSauce.SetActive(true);
            // _buttonsSauce.transform.position = _buttonsSaucePos[(int)_whichHanded].position;
        } 
    }

    public void SwitchHandButton()
    {
        _whichHanded = _whichHanded == WhichHanded.Left ? WhichHanded.Right : WhichHanded.Left;

        SwitchHand((int)_whichHanded);
    }

    private void SwitchHand(int index)
    {
        _whichHanded = (WhichHanded)index;
        if (_whichHanded == WhichHanded.Left)
        {
            _sabers[0].transform.parent = _saberParents[0].transform;
            _sabers[0].transform.localPosition = Vector3.zero;
            _sabers[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            
            _sabers[1].transform.parent = _fritureParents[1].transform;
            _sabers[1].transform.localPosition = Vector3.zero;
            _sabers[1].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            _sabers[0].transform.parent = _saberParents[1].transform;
            _sabers[0].transform.localPosition = Vector3.zero;
            _sabers[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            
            _sabers[1].transform.parent = _fritureParents[0].transform;
            _sabers[1].transform.localPosition = Vector3.zero;
            _sabers[1].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
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
