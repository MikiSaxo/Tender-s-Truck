using System;
using System.Collections;
using UnityEngine;
using System.IO;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
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
    private int _countByTwoBPM;
    private int _countElementIndex;
    private float _timeBetweenEachTwoBPM;

    private void Start()
    {
        LoadFileMap();
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

        if (_timeToBPM >= _timeBetweenEachTwoBPM)
        {
            _timeToBPM -= _timeToBPM;

            if (_countByTwoBPM % 2 == 0)
                _countBPM++;

            if (_countElementIndex == _mapConstructData.ElementsIndex.Count)
                return;

            if (_mapConstructData.ElementsIndex[_countElementIndex] == _countByTwoBPM)
            {
                SpawnElement(_mapConstructData.ElementsType[_countElementIndex],
                    _mapConstructData.ElementsPosition[_countElementIndex]);
                _countElementIndex++;
            }

            _countByTwoBPM++;
        }
    }

    private void SpawnElement(ElementType element, BoardPosition spawnerIndex)
    {
        GameObject go = Instantiate(_elementPrefab);
        go.transform.position = _spawnersPos[(int)spawnerIndex].position;

        // var rb = go.GetComponent<Rigidbody>();
        // rb.AddForce(_impulseDir * _force);

        print("spawn : " + element);

        go.GetComponent<ElementToSpawn>().Init(element, false);

        Destroy(go, 100);
    }
}