using System;
using System.Collections;
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

    [Header("Element")] 
    [SerializeField] private GameObject _elementPrefab;
    [SerializeField] private InputAction _action;

    [Header("Spawners")] [SerializeField] private Transform[] _spawnersPos;

    private Transform _target;
    private float _timeToReachTarget;
    private string _levelName;
    private string _levelFolder;

    private MapConstructData _mapConstructData;

    private float _timeSinceStart;
    private float _timeToBPM;
    private bool _canGo;
    private bool _hasLaunchMusic;
    private int _currentBPM;
    private int _countBPM;
    private int _countByFourBPM;
    private int _countElementIndex;
    private float _timeBetweenEachTwoBPM;

    private float _distanceTarget;

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
        _timeBetweenEachTwoBPM = 1f / (_currentBPM / 60f);
        // print("bpm : " + _currentBPM + " / " + _timeBetweenEachTwoBPM);

        // _canGo = true;
        // InitializeMap();
    }

    public void LaunchMap()
    {
        print("allo");
        _canGo = true;
    }

    private void Update()
    {
        if (!_canGo)
            return;

        PrepareLaunchMusic();
        SpawnByTime();

        // if (Input.GetKeyDown(KeyCode.A))
        // if(_action.triggered)
        // {
        //     print("allo");
        //     LaunchMap();
        // }
    }

    private void SpawnByTime()
    {
        _timeToBPM += Time.deltaTime;

        if (_timeToBPM >= _timeBetweenEachTwoBPM * .25f)
        {
            _timeToBPM -= _timeToBPM;

            if (_countByFourBPM % 4 == 0)
                _countBPM++;

            if (_countElementIndex == _mapConstructData.ElementsIndex.Count)
                return;

            for (int i = 0; i < _mapConstructData.ElementsIndex.Count; i++)
            {
                if (_mapConstructData.ElementsIndex[i] == _countByFourBPM)
                {
                    SpawnElement(_mapConstructData.ElementsType[i],
                        _mapConstructData.ElementsPosition[i]);
                    _countElementIndex++;
                }
            }

            _countByFourBPM++;
        }
    }

    private void PrepareLaunchMusic()
    {
        if (_timeSinceStart < _timeToReachTarget)
            _timeSinceStart += Time.deltaTime;
        else
            LaunchMusic();
    }

    private void LaunchMusic()
    {
        if (_hasLaunchMusic)
            return;
        print("Launch la musica");
        _hasLaunchMusic = true;
        AudioManager.Instance.PlaySound("120bpm");
    }

    private void SpawnElement(ElementType element, BoardPosition spawnerIndex)
    {
        GameObject go = Instantiate(_elementPrefab);

        if (PartyManager.Instance.WhichHanded == WhichHanded.Left)
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

        go.transform.position = _spawnersPos[(int)spawnerIndex].position;

        go.GetComponent<ElementToSpawn>().Init(element, false, _target, _timeToReachTarget, _distanceTarget);

        Destroy(go, 100);
    }
}