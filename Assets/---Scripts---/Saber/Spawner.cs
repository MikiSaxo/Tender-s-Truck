using System;
using System.Collections;
using UnityEngine;
using System.IO;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _timeToReachTarget;
    [SerializeField] private GameObject _elementPrefab;

    [Header("Level Infos")] [SerializeField]
    private string _levelName;

    [SerializeField] private string _levelFolder;
    
    
    [Header("Spawners")] [SerializeField] private Transform[] _spawnersPos;

    private MapConstructData _mapConstructData;

    private float _timeSinceStart;
    private float _timeToBPM;
    private bool _canGo;
    private int _currentBPM;
    private int _countBPM;
    private int _countByFourBPM;
    private int _countElementIndex;
    private float _timeBetweenEachTwoBPM;
    
    private float _distanceTarget;

    private void Start()
    {
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

        _canGo = true;
        // InitializeMap();
    }

    private void Update()
    {
        if (!_canGo)
            return;

        _timeSinceStart += Time.deltaTime;
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