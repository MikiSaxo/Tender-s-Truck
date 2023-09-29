using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ObjectZoom : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private float _zoomIn = .05f;
    [Range(1.1f, 2f)] [SerializeField] private float _zoomOut = 1.2f;
    [SerializeField] private float _dragSpeed;
    
    [Header("Board")]
    [SerializeField] private int _numberToSpawn;
    [SerializeField] private GameObject _boardEditorPrefab;
    
    private List<GameObject> _objectsToMove = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _numberToSpawn; i++)
        {
            GameObject go = Instantiate(_boardEditorPrefab, transform);
            go.GetComponent<BoardEditor>().Init(i+1, i%4);
            var position = transform.position;
            go.transform.position = new Vector3(position.x, position.y, position.z + (i * 5f));
            _objectsToMove.Add(go);
        }   
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

        foreach (var obj in _objectsToMove)
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
}