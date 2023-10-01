using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    
    [Header("Controls")] [SerializeField] private float _zoomIn = .05f;
    [Range(1.1f, 2f)] [SerializeField] private float _zoomOut = 1.2f;
    [SerializeField] private float _dragSpeed;

    [Header("Board")] [SerializeField] private int _numberToSpawn;
    [SerializeField] private GameObject _boardEditorPrefab;

    private List<GameObject> _panelElements = new List<GameObject>();
    private int _counter;
    private const float StartDistance = 5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < _numberToSpawn; i++)
        {
            AddFourBoard();
        }
    }

    public void AddFourBoard()
    {
        for (int j = 0; j < 4; j++)
        {
            // Instantiate the board
            GameObject go = Instantiate(_boardEditorPrefab, transform);
            // Init the numbers
            go.GetComponent<BoardEditor>().Init(_counter + 1, j % 4);
            // Get position of spawner and move it  
            var position = transform.position;
            go.transform.position = new Vector3(position.x, position.y, position.z + (_counter * StartDistance));
            // Add to list
            _panelElements.Add(go);
            // Add to the counter
            _counter++;
        }
    }

    public void RemoveFourBoard()
    {
        for (int j = 0; j < 4; j++)
        {
            // Call this function to remove on the save
            _panelElements[^1].GetComponent<BoardEditor>().OnDestroyElement();
            // Destroy last at last index
            Destroy(_panelElements[^1]);
            // Remove this destroyed object in the list
            _panelElements.RemoveAt(_panelElements.Count - 1);
            // Subtract to _counter
            _counter--;
        }
    }

    public void ResetMap()
    {
        foreach (var obj in _panelElements)
        {
            obj.GetComponent<BoardEditor>().OnDestroyElement();
            Destroy(obj);
        }
        _panelElements.Clear();
        _counter = 0;
        
        AddFourBoard();
    }

    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            float mouseYInput = Input.GetAxis("Mouse Y");
            transform.Translate(Vector3.forward * mouseYInput * _dragSpeed);
        }

        float zoomInput = Input.GetAxis("Mouse ScrollWheel");

        if (zoomInput == 0)
            return;

        foreach (var obj in _panelElements)
        {
            var position = obj.transform.position;

            if (zoomInput < 0)
            {
                var newZ = Vector3.Lerp(position, transform.position, _zoomIn);
                position = new Vector3(position.x, position.y, newZ.z);
                obj.transform.position = position;
            }
            else
            {
                var newZ = Vector3.Lerp(position, transform.position, _zoomIn);
                position = new Vector3(position.x, position.y, newZ.z * _zoomOut);
                obj.transform.position = position;
            }
        }
    }

    public void ResetPos()
    {
        transform.position = new Vector3(0, transform.position.y, 0);

        for (int i = 0; i < _panelElements.Count; i++)
        {
            var position = transform.position;
            _panelElements[i].transform.position = new Vector3(position.x, position.y, position.z + (i * 5f));
        }
    }

    public List<GameObject> GetObjectList()
    {
        return _panelElements;
    }
}