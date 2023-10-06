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
    private MapConstructData _mapConstructData;


    private void CheckFileMap()
    {
        var mapPath = $"{Application.streamingAssetsPath}/{_levelFolder}/{_levelName}.txt";

        // Get the text map
        if (!File.Exists(mapPath))
        {
            Debug.LogErrorFormat("Streaming asset not found: {0}", mapPath);
        }

        var lineJson = File.ReadAllText(mapPath);
        _mapConstructData = JsonUtility.FromJson<MapConstructData>(lineJson);

        // InitializeMap();
    }

    private void SpawnElement(int index)
    {
        GameObject go = Instantiate(_elementPrefab);
        go.transform.position = transform.position;

        // var rb = go.GetComponent<Rigidbody>();
        // rb.AddForce(_impulseDir * _force);


        go.GetComponent<ElementToSpawn>().Init((ElementType)index, false);

        Destroy(go, 10);
    }
}