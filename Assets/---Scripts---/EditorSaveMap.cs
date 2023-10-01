using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EditorSaveMap : MonoBehaviour
{
    public static EditorSaveMap Instance;

    [Header("Setup Save")] [SerializeField]
    private TMP_InputField _inputMapName;

    [SerializeField] private TMP_InputField _inputMusicName;
    [SerializeField] private TMP_InputField _inputMusicBPM;
    [SerializeField] private string _folderDestination;

    [Header("FB Text")] [SerializeField] private GameObject _fBTextPrefab;
    [SerializeField] private GameObject _fBTextParent;
    [SerializeField] private Color _colorGood;
    [SerializeField] private Color _colorNotGood;

    private string _mapName;
    private string _musicName;
    private int _musicBPM;
    public MapConstructData _currentMCD;
    private string _hexColorGood;
    private string _hexColorNotGood;

    private const string _saveNoName = "No MAP name written";
    private const string _saveNoMusicName = "No MUSIC name written";
    private const string _saveNoMusicBPM = "No BPM number written";
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

    private void PrintDico()
    {
        foreach (var kvp in _currentMCD.ElementByPosIndex)
        {
            Debug.Log($"Clé : {kvp.Key}, Valeur : {kvp.Value}");
        }
    }

    public string GetColorNotGood()
    {
        return _hexColorNotGood;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            SaveJson();
        if (Input.GetKeyDown(KeyCode.P))
            PrintDico();
    }

    private void UpdateMapName()
    {
        _mapName = _inputMapName.text;

        if (_inputMapName.text == "")
        {
            SpawnFbText($"{_hexColorNotGood}{_saveNoName}");
        }
    }

    private void UpdateMusicName()
    {
        _musicName = _inputMusicName.text;

        if (_inputMusicName.text == "")
        {
            SpawnFbText($"{_hexColorNotGood}{_saveNoMusicName}");
        }
        else
        {
            _currentMCD.MusicName = _musicName;
        }
    }

    private void UpdateMusicBPM()
    {
        if (_inputMusicBPM.text != "")
            _musicBPM = int.Parse(_inputMusicBPM.text);

        if (_musicBPM <= 0 || _inputMusicBPM.text == "")
        {
            SpawnFbText($"{_hexColorNotGood}{_saveNoMusicBPM}");
        }
        else
        {
            _currentMCD.MusicBPM = _musicBPM;
        }
    }

    public void UpdateElement(int elementIndex, BoardPosition boardPos, ElementType elementType, bool isAdd)
    {
        BoardPosType posType = new BoardPosType
        {
            BoardPosition = boardPos,
            ElementType = elementType
        };

        if (isAdd)
        {
            AddElement(elementIndex, posType);
        }
        else
        {
            RemoveElement(elementIndex, posType);
        }
    }

    private void AddElement(int elementIndex, BoardPosType posType)
    {
        _currentMCD.ElementByPosIndex.Add(elementIndex, posType);
    }

    private void RemoveElement(int elementIndex, BoardPosType posType)
    {
        List<int> keyToRemove = new List<int>();

        foreach (var element in _currentMCD.ElementByPosIndex)
        {
            if (element.Key == elementIndex 
                && element.Value.ElementType == posType.ElementType 
                && element.Value.BoardPosition == posType.BoardPosition)
            {
                keyToRemove.Add(elementIndex);
            }
        }

        foreach (int element in keyToRemove)
        {
            _currentMCD.ElementByPosIndex.Remove(element);
        }
    }

    private void ResetInputField()
    {
        _inputMapName.text = "";
        _inputMusicName.text = "";
        _inputMusicBPM.text = "";
    }

    public void UpdateInputField(string newName)
    {
        // Used for load map
        _inputMapName.text = newName;
    }

    public void SpawnFbText(string text)
    {
        // Message warning of success
        GameObject go = Instantiate(_fBTextPrefab, _fBTextParent.transform);
        var fBText = go.GetComponent<TMP_Text>();
        fBText.DOFade(1, 0);
        fBText.text = text;
        fBText.DOFade(0, _durationDispawnText);

        Destroy(go, _durationDispawnText);
    }

    public void SaveMap()
    {
        UpdateMapName();
        UpdateMusicName();
        UpdateMusicBPM();
        //GetMap();

        if (_mapName == "" || _musicName == "" || _musicBPM <= 0) return;

        ResetInputField();

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
        mapConstructData = _currentMCD;

        string json = JsonUtility.ToJson(mapConstructData);
        File.WriteAllText($"{Application.streamingAssetsPath}/{_folderDestination}/{_mapName}.txt", json);
        RefreshEditorProjectWindow();
    }
}