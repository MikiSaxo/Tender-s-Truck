using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Serialization;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public int SignNbByBPM { get; set; }

    [Header("Controls")]
    //[SerializeField] private float _zoomIn = .05f;
    //[Range(1.1f, 2f)] [SerializeField] private float _zoomOut = 1.2f;
    [SerializeField]
    private float _dragSpeed;

    [Header("Board")] [SerializeField] private int _numberToSpawn;
    [SerializeField] private GameObject _boardEditorPrefab;

    private List<GameObject> _boardsPanel = new List<GameObject>();
    private int _counter;
    private int _currentVisibility;
    private float _distanceToEnd;

    // private const float StartDistance = 1.33f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SignNbByBPM = 4;
        _currentVisibility = 4;

        for (int i = 0; i < _numberToSpawn; i++)
        {
            AddMultipleBoard();
        }
    }

    public void AddMultipleBoard()
    {
        for (int j = 0; j < SignNbByBPM; j++)
        {
            // Instantiate the board
            GameObject go = Instantiate(_boardEditorPrefab, transform);
            // Init the numbers
            go.GetComponent<BoardPanel>().Init(_counter, j % SignNbByBPM);
            // Get position of spawner and move it  
            SetPositionBoards(go, _counter);
            // Add to list
            _boardsPanel.Add(go);
            // Add to the counter
            _counter++;
        }
        
        ChangeVisibility(_currentVisibility);
    }

    public void RemoveMultipleBoard()
    {
        for (int j = 0; j < SignNbByBPM; j++)
        {
            // Call this function to remove on the save
            _boardsPanel[^1].GetComponent<BoardPanel>().OnDestroyElement();
            // Destroy last at last index
            Destroy(_boardsPanel[^1]);
            // Remove this destroyed object in the list
            _boardsPanel.RemoveAt(_boardsPanel.Count - 1);
            // Subtract to _counter
            _counter--;
        }
        
        _distanceToEnd = _counter * _currentVisibility;
    }

    private void SetPositionBoards(GameObject board, int index)
    {
        var position = transform.position;
        board.transform.position = new Vector3(position.x, position.y, position.z + (index * _currentVisibility));
    }

    public void ChangeVisibility(int number)
    {
        _currentVisibility = number;
        if (number == 1)
        {
            for (int i = 0; i < _boardsPanel.Count; i++)
            {
                SetPositionBoards(_boardsPanel[i], i);
                _boardsPanel[i].SetActive(i % 4 == 0);
            }
        }

        else if (number == 2)
        {
            for (int i = 0; i < _boardsPanel.Count; i++)
            {
                SetPositionBoards(_boardsPanel[i], i);
                _boardsPanel[i].SetActive(i % 2 == 0);
            }
        }

        else if (number == 4)
        {
            for (int i = 0; i < _boardsPanel.Count; i++)
            {
                SetPositionBoards(_boardsPanel[i], i);
                _boardsPanel[i].SetActive(true);
            }
        }

        _distanceToEnd = _counter * _currentVisibility;

        // ResetPos();
    }

    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            float mouseYInput = Input.GetAxis("Mouse Y");
            transform.Translate(Vector3.forward * mouseYInput * _dragSpeed);
        }
    }

    public void MoveBoards(float percent)
    {
        var pos = transform.position;
        // transform.position = new Vector3(pos.x, pos.y, -index);

        transform.position = new Vector3(pos.x, pos.y, -_distanceToEnd * percent / 100);
    }

    private void Zoom()
    {
        // float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        //
        // if (zoomInput == 0)
        //     return;
        //
        // foreach (var obj in _boardsPanel)
        // {
        //     var position = obj.transform.position;
        //
        //     if (zoomInput < 0)
        //     {
        //         var newZ = Vector3.Lerp(position, transform.position, _zoomIn);
        //         position = new Vector3(position.x, position.y, newZ.z);
        //         obj.transform.position = position;
        //     }
        //     else
        //     {
        //         var newZ = Vector3.Lerp(position, transform.position, _zoomIn);
        //         position = new Vector3(position.x, position.y, newZ.z * _zoomOut);
        //         obj.transform.position = position;
        //     }
        // }
    }

    public void ResetPos()
    {
        transform.position = new Vector3(0, transform.position.y, 0);

        for (int i = 0; i < _boardsPanel.Count; i++)
        {
            var position = transform.position;
            SetPositionBoards(_boardsPanel[i], i);
            // _boardsPanel[i].transform.position = new Vector3(position.x, position.y, position.z + (i * 5f));
        }
        
        _distanceToEnd = _counter * _currentVisibility;
    }

    public void ResetMap()
    {
        foreach (var obj in _boardsPanel)
        {
            obj.GetComponent<BoardPanel>().OnDestroyElement();
            Destroy(obj);
        }

        _boardsPanel.Clear();
        _counter = 0;

        AddMultipleBoard();
    }

    public List<GameObject> GetObjectList()
    {
        return _boardsPanel;
    }
}