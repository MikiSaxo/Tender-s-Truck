using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EditorSaveMap : MonoBehaviour
{
    public static EditorSaveMap Instance;

    [FormerlySerializedAs("_inputFieldMapName")]
    [Header("Setup Save")] 
    [SerializeField] private TMP_InputField _inputMapName;
    [SerializeField] private TMP_InputField _inputMusicName;
    [SerializeField] private TMP_InputField _inputMusicBPM;
    [SerializeField] private string _folderDestination;

    [Header("FB Text")] [SerializeField] private GameObject _fBTextPrefab;
    [SerializeField] private GameObject _fBTextParent;
    [SerializeField] private Color _colorGood;
    [SerializeField] private Color _colorNotGood;

    private string _mapName;
    public MapConstructData _currentMapConstructData;
    private string _hexColorGood;
    private string _hexColorNotGood;
    private GameObject _currentFBTextPrefab;

    private const string _saveNoName = "No map name written";
    private const string _saveSucceed = "saved";
    private const float _durationDispawnText = 5f;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _hexColorGood = $"<color=#{_colorGood.ToHexString()}>";
        _hexColorNotGood = $"<color=#{_colorNotGood.ToHexString()}>";
    }

    public string GetColorNotGood()
    {
        return _hexColorNotGood;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            SaveJson();
    }

    private void UpdateMapName(char[,] mapGrid)
    {
        _mapName = _inputMapName.text;

        if (_inputMapName.text == "")
        {
            SpawnFbText($"{_hexColorNotGood}{_saveNoName}");
            return;
        }

        _inputMapName.text = "";
    }

    public void UpdateInputField(string newName)
    {
        _inputMapName.text = newName;
    }

    public void SpawnFbText(string text)
    {
        if (_currentFBTextPrefab != null)
        {
            _currentFBTextPrefab.GetComponent<TMP_Text>().DOKill();
            Destroy(_currentFBTextPrefab);
        }
        
        GameObject go = Instantiate(_fBTextPrefab, _fBTextParent.transform);
        _currentFBTextPrefab = go;
        var fBText = _currentFBTextPrefab.GetComponent<TMP_Text>();
        fBText.DOFade(1, 0);
        fBText.text = text;
        fBText.DOFade(0, _durationDispawnText);
        
        Destroy(_currentFBTextPrefab, _durationDispawnText);
    }

    private void CreateTextFile(char[,] mapGrid)
    {
        string textName = $"{Application.streamingAssetsPath}/{_folderDestination}/{_mapName}.txt";

        if (File.Exists(textName))
            File.Delete(textName);

        // string map = ConvertMapGridToString(mapGrid);
        // File.WriteAllText(textName, map);
        // if (map != null)
        //     _currentMapConstructData.Map = map;
    }

    // private void GetMap()
    // {
    //     string map = ConvertMapGridToString(EditorMapManager.Instance.GetMapGrid());
    //     if (map != null)
    //         _currentMapConstructData.Map = map;
    // }
    // private string ConvertMapGridToString(char[,] mapGrid)
    // {
    //     var str = String.Empty;
    //
    //     for (int y = 0; y < mapGrid.GetLength(1); y++)
    //     {
    //         for (int x = 0; x < mapGrid.GetLength(0); x++)
    //         {
    //             str += mapGrid[x, y];
    //         }
    //
    //         if(y == mapGrid.GetLength(1)-1)
    //             continue;
    //         
    //         str += "\n";
    //     }
    //
    //     return str;
    // }
    
    public void SaveMap()
    {
        //UpdateMapName(EditorMapManager.Instance.GetMapGrid());
        //GetMap();

        if (_mapName == "") return;
        
        SaveJson();
        
        SpawnFbText($"{_hexColorGood}{_mapName} {_saveSucceed} in {_folderDestination} folder!");
        RefreshEditorProjectWindow();
    }

    private void RefreshEditorProjectWindow()
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    // public void AddCoordsEnergy(Vector2Int coords)
    // {
    //     if(!_currentMapConstructData.Coords.Contains(coords))
    //         _currentMapConstructData.Coords.Add(coords);
    // }
    //
    // public void DestroyCoordsEnergy(Vector2Int coords)
    // {
    //     if(_currentMapConstructData.Coords.Contains(coords))
    //         _currentMapConstructData.Coords.Remove(coords);
    // }

    private void SaveJson()
    {
        MapConstructData mapConstructData = new MapConstructData();
        mapConstructData = _currentMapConstructData;

        string json = JsonUtility.ToJson(mapConstructData);
        File.WriteAllText($"{Application.streamingAssetsPath}/{_folderDestination}/{_mapName}.txt", json);
        RefreshEditorProjectWindow();
    }
    
}
