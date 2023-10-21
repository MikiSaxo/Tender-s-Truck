using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [Header("Element")] [SerializeField] private GameObject _elementPrefab;
    [SerializeField] private InputAction _action;

    [Header("Spawners")] [SerializeField] private Transform[] _spawnersPos;

    private Transform _target;
    private float _timeToReachTarget;
    private string _levelName;
    private string _levelFolder;

    private MapConstructData _mapConstructData;

    private float _timeSinceStart;
    private float _time;
    private bool _canGo;
    private bool _hasLaunchMusic;
    private int _currentBPM;
    private int _countByFourBPM;
    private int _countElementIndex;
    private float _timeBetweenEachQuartBPM;
    private float _timeBetweenTwoBPM;
    private float _distanceTarget;
    private string _musicName;
    private List<GameObject> _spawnElements = new List<GameObject>();
    private bool _firstCheckWin = false;

    private float _speed;
    private float _startTime;
    private float _distanceToEnd;
    private int _numberToSpawn;


    private void Awake()
    {
        Instance = this;
        _levelFolder = PartyManager.Instance.LevelFolder;
        _levelName = PartyManager.Instance.LevelName;
    }

    private void Start()
    {
        _timeToReachTarget = PartyManager.Instance.TimeToReachTarget;
        _target = PartyManager.Instance.SpawnTarget;


        _action.Enable();
        _action.performed += context => { LaunchMap(); };

        LoadFileMap();
        _distanceTarget = Vector3.Distance(_target.position, transform.position);

        _musicName = "MainMusic";

        _startTime = Time.time;
    }

    private void LoadFileMap()
    {
        var mapPath = $"{Application.streamingAssetsPath}/{_levelFolder}/{_levelName}.txt";

        // Get the text map
        if (!File.Exists(mapPath))
        {
            Debug.LogErrorFormat("Streaming asset not found: {0}", mapPath);
        }

        var lineJson = File.ReadAllText(mapPath);
        _mapConstructData = JsonUtility.FromJson<MapConstructData>(lineJson);
        _currentBPM = _mapConstructData.MusicBPM;
        _timeBetweenEachQuartBPM = (1f / (_currentBPM / 60f)) * .25f;
        // print("bpm : " + _currentBPM + " / " + _timeBetweenEachTwoBPM);

        // _canGo = true;
        // InitializeMap();
        SpawnAll();
    }

    public void LaunchMap()
    {
        StopMusic();

        _canGo = true;

        ScoreManager.Instance.ResetScore();
        AudioManager.Instance.StopSound("MenuMusic");
        
        LifeManager.Instance.SetupLife();
        _startTime = Time.time;
        LaunchMusic();
    }

    public void StopMusic()
    {
        _canGo = false;
        _hasLaunchMusic = false;
        _time = 0;
        _countByFourBPM = 0;
        _countElementIndex = 0;
        _timeSinceStart = 0;

        // AudioManager.Instance.StopSound(_musicName);

        // foreach (var element in _spawnElements)
        // {
        //     if (element != null)
        //         Destroy(element);
        // }
        //
        // _spawnElements.Clear();
    }

    private void Update()
    {
        if (!_canGo)
            return;

        MoveElements();
        
        // PrepareLaunchMusic();

        // if (Input.GetKeyDown(KeyCode.A))
        // if(_action.triggered)
        // {
        //     print("allo");
        //     LaunchMap();
        // }
    }

    private void FixedUpdate()
    {
        if (!_canGo)
            return;
        
        // SpawnByTime();
    }

    private void SpawnAll()
    {
        _timeBetweenTwoBPM = 1f / (_currentBPM / 60f);
        _speed = 4f / _timeBetweenTwoBPM;
        _numberToSpawn = (int)(AudioManager.Instance.GetLengthMusic("MainMusic") * (_currentBPM + 1) / 60);
        _distanceToEnd = (_numberToSpawn - 1) * 4;

        // print($"{_mapConstructData.ElementsType.Count} / {_mapConstructData.ElementsBoardPosition.Count} / {_mapConstructData.ElementsIndex.Count}");
        for (int i = 0; i < _mapConstructData.ElementsIndex.Count; i++)
        {
            // print($"{_mapConstructData.ElementsType[i]} / {_mapConstructData.ElementsBoardPosition[i]} / {_mapConstructData.ElementsIndex[i]}");
            SpawnElement(_mapConstructData.ElementsType[i], _mapConstructData.ElementsBoardPosition[i], _mapConstructData.ElementsIndex[i]);
        }
    }
    
    private void MoveElements()
    {
        float distanceCovered = (Time.time - _startTime) * _speed;
        float totalDistance = Mathf.Abs(_distanceToEnd);

        if (distanceCovered < totalDistance)
        {
            float newPosition = distanceCovered / totalDistance;

            transform.position = Vector3.Lerp(Vector3.zero, Vector3.back * totalDistance, newPosition);
        }
        AudioManager.Instance.StopSound("MenuMusic");
    }
    
    private void SpawnByTime()
    {
        _time += Time.fixedDeltaTime;

        if (_time >= _timeBetweenEachQuartBPM)
        {
            _time = 0;

            if (_countElementIndex == _mapConstructData.ElementsIndex.Count)
            {
                _firstCheckWin = true;
                return;
            }

            for (int i = 0; i < _mapConstructData.ElementsIndex.Count; i++)
            {
                if (_mapConstructData.ElementsIndex[i] == _countByFourBPM)
                {
                    SpawnElement(_mapConstructData.ElementsType[i],
                        _mapConstructData.ElementsBoardPosition[i],0);
                    _countElementIndex++;
                }
            }

            _countByFourBPM++;
        }
    }

    private void PrepareLaunchMusic()
    {
        if (_timeSinceStart <= _timeToReachTarget+3)
        {
            _timeSinceStart += Time.deltaTime;
        }
        else
            LaunchMusic();
    }

    private void LaunchMusic()
    {
        if (_hasLaunchMusic)
            return;
        
        print("Launch la musica");
        _hasLaunchMusic = true;
        AudioManager.Instance.PlaySound(_musicName);
        AudioManager.Instance.StopSound("MenuMusic");
    }

    private void SpawnElement(ElementType element, BoardPosition spawnerIndex, float zPos)
    {

        if (PartyManager.Instance.WhichHanded == WhichHanded.Right)
        {
            if (spawnerIndex == BoardPosition.LeftTop)
                spawnerIndex = BoardPosition.RightTop;
            else if (spawnerIndex == BoardPosition.LeftDown)
                spawnerIndex = BoardPosition.RightDown;
            else if (spawnerIndex == BoardPosition.RightTop)
                spawnerIndex = BoardPosition.LeftTop;
            else if (spawnerIndex == BoardPosition.RightDown)
                spawnerIndex = BoardPosition.LeftDown;
        }
        
        GameObject go = Instantiate(_elementPrefab, _spawnersPos[(int)spawnerIndex]);
        _spawnElements.Add(go);

        go.transform.position += new Vector3(0, 0, zPos);

        go.GetComponent<ElementToSpawn>().Init(element, false, _target, _timeToReachTarget, _distanceTarget);

        // Destroy(go);
    }

    public void CheckIfVictory()
    {
        if (!_firstCheckWin)
            return;

        // print("check victory " + _spawnElements[^1]);

        StartCoroutine(WaitCheckVictory());
    }

    IEnumerator WaitCheckVictory()
    {
        yield return new WaitForSeconds(.1f);
        if (_spawnElements[^1] == null && LifeManager.Instance.CurrentLife > 0)
            Victory();
    }

    private void Victory()
    {
        print("victoire");
        AudioManager.Instance.StopSound(_musicName);
        AudioManager.Instance.PlaySound("Victory");
        StartCoroutine(WaitToLaunchMenuMusic());
    }

    IEnumerator WaitToLaunchMenuMusic()
    {
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.PlaySound("MenuMusic");
    }
}