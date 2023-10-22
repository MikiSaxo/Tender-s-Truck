using System;
using System.Collections;
using UnityEngine;
using System.IO;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class EditorSaveMap : MonoBehaviour
{
    public static EditorSaveMap Instance;

    [Header("Setup Save")] [SerializeField]
    private TMP_InputField _inputMapName;

    [SerializeField] private TMP_InputField _inputMusicName;
    [SerializeField] private TMP_InputField _inputMusicBPM;
    [SerializeField] private TMP_InputField _inputLoapMapStart;
    [SerializeField] private TMP_InputField _inputLoapMap;
    [SerializeField] private string _folderDestination;

    [Header("FB Text")] [SerializeField] private GameObject _fBTextPrefab;
    [SerializeField] private GameObject _fBTextParent;
    [SerializeField] private Color _colorGood;
    [SerializeField] private Color _colorNotGood;

    private string _mapName;
    private string _musicName;
    private int _musicBPM;
    private int _nbToSpawn;
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
        for (int i = 0; i < _currentMCD.ElementsIndex.Count; i++)
        {
            Debug.Log($"Index : {_currentMCD.ElementsIndex[i]}" +
                      $", Pos : {_currentMCD.ElementsBoardPosition[i]}" +
                      $", Type : {_currentMCD.ElementsType[i]}");
        }
    }

    public string GetColorNotGood()
    {
        return _hexColorNotGood;
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.O))
        //     SaveJson();
        // if (Input.GetKeyDown(KeyCode.P))
        //     PrintDico();
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

    public void AddElement(int elementIndex, BoardPosition elementPos, ElementType elementType)
    {
        _currentMCD.ElementsIndex.Add(elementIndex);
        _currentMCD.ElementsBoardPosition.Add(elementPos);
        _currentMCD.ElementsType.Add(elementType);
    }

    public void RemoveElement(int elementIndex, BoardPosition elementPos, ElementType elementType)
    {
        for (int i = 0; i < _currentMCD.ElementsIndex.Count; i++)
        {
            if (_currentMCD.ElementsIndex[i] == elementIndex
                && _currentMCD.ElementsBoardPosition[i] == elementPos
                && _currentMCD.ElementsType[i] == elementType)
            {
                _currentMCD.ElementsIndex.RemoveAt(i);
                _currentMCD.ElementsBoardPosition.RemoveAt(i);
                _currentMCD.ElementsType.RemoveAt(i);
                break;
            }
        }
    }

    public void DeleteElement(int elementIndex)
    {
        for (int i = 0; i < _currentMCD.ElementsIndex.Count; i++)
        {
            if (_currentMCD.ElementsIndex[i] == elementIndex)
            {
                _currentMCD.ElementsIndex.RemoveAt(i);
                _currentMCD.ElementsBoardPosition.RemoveAt(i);
                _currentMCD.ElementsType.RemoveAt(i);
                DeleteElement(elementIndex);
            }
        }
    }
    
    private void UpdateInputField(string mapName, string musicName, int BPM_Number)
    {
        // Reset input field Load Map
        _inputLoapMap.text = "";
        // Update other input field
        _inputMapName.text = mapName;
        _inputMusicName.text = musicName;
        _inputMusicBPM.text = $"{BPM_Number}";
    }

    public void LoadMap()
    {
        // Get the input field text
        string mapName = String.Empty;
        mapName = _inputLoapMap.text;
        if (mapName == "")
        {
            mapName = _inputLoapMapStart.text;
            if (mapName == "")
            {
                SpawnFbText($"{GetColorNotGood()}No map name written");
                return;
            }
        }

        // Get the file
        var mapPath = $"{Application.streamingAssetsPath}/{_folderDestination}/{mapName}.txt";
        if (!File.Exists(mapPath))
        {
            // Debug.LogErrorFormat("Streaming asset not found: {0}", mapPath);
            SpawnFbText($"{GetColorNotGood()}This map doesn't exist");
            return;
        }

        // Reset old Map
        BoardManager.Instance.ResetMap();

        // Put the file on the current Construct Data
        var lineJson = File.ReadAllText(mapPath);
        _currentMCD = JsonUtility.FromJson<MapConstructData>(lineJson);

        // Update all map
        _nbToSpawn = (int)(BoardManager.Instance.MusicLength * (_currentMCD.MusicBPM) / 60);

        for (int i = 0; i < _nbToSpawn; i++)
        {
            BoardManager.Instance.AddMultipleBoard();
        }


        // StartCoroutine(MakeItSlowly());
        SpawnAllElements();
        // Update Input Field to save the same map
        UpdateInputField(mapName, _currentMCD.MusicName, _currentMCD.MusicBPM);

        // Reset the position
        BoardManager.Instance.BPM = _currentMCD.MusicBPM;
        BoardManager.Instance.ResetPos();
        BoardManager.Instance.HasLoadMap();
    }
    
    private void SpawnAllElements()
    {
        var getAllPanel = BoardManager.Instance.GetObjectList();

        for (int i = 0; i < getAllPanel.Count; i++)
        {
            for (int j = 0; j < _currentMCD.ElementsIndex.Count; j++)
            {
                if (_currentMCD.ElementsIndex[j] == i)
                {
                    // print($"Element Number: {i} | Change element : {_currentMCD.ElementsType[j]} | Add element with this pos : {_currentMCD.ElementsBoardPosition[j]}");

                    if (_currentMCD.ElementsIndex[j] > _nbToSpawn * 4)
                    {
                        print("To much");
                        break;
                    }
                    
                    gameObject.GetComponent<EditorManager>().ChangeElement((int)_currentMCD.ElementsType[j]);
                    getAllPanel[i].GetComponent<BoardPanel>().GetBoardSign((int)_currentMCD.ElementsBoardPosition[j]).AddElement();
                }
            }
        }
        gameObject.GetComponent<EditorManager>().ChangeElement(4);
        print("Load Done!");
    }

    public void MoveEachElements(int index)
    {
        for (int i = 0; i < _currentMCD.ElementsIndex.Count; i++)
        {
            if (_currentMCD.ElementsIndex[i] + index >= 0 &&
                _currentMCD.ElementsIndex[i] + index < _nbToSpawn * 4)
            {
                // print($"currentIndex : {_currentMCD.ElementsIndex[i]} / add {index}");
                _currentMCD.ElementsIndex[i] += index;
            }
            else if (_currentMCD.ElementsIndex[i] + index < 0 || _currentMCD.ElementsIndex[i] + index >= _nbToSpawn * 4)
            {
                _currentMCD.ElementsIndex.RemoveAt(i);
            }
        }
        
        BoardManager.Instance.ClearMap();
        SpawnAllElements();
    }

    public void SpawnFbText(string text)
    {
        // Message warning of success
        GameObject go = Instantiate(_fBTextPrefab, _fBTextParent.transform);
        var fBText = go.GetComponent<TMP_Text>();
        // fBText.DOFade(1, 0);
        fBText.text = text;
        // fBText.DOFade(0, _durationDispawnText);

        Destroy(go, _durationDispawnText);
    }

    public void SaveMap()
    {
        UpdateMapName();
        UpdateMusicName();
        UpdateMusicBPM();
        //GetMap();

        if (_mapName == "" || _musicName == "" || _musicBPM <= 0) return;

        // ResetInputField();

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

    private void SaveJson()
    {
        MapConstructData mapConstructData = new MapConstructData();
        mapConstructData = _currentMCD;

        string json = JsonUtility.ToJson(mapConstructData);
        File.WriteAllText($"{Application.streamingAssetsPath}/{_folderDestination}/{_mapName}.txt", json);
        RefreshEditorProjectWindow();
    }
}